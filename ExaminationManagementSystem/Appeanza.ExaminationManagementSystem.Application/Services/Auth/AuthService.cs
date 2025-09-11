using Appeanza.ExaminationManagementSystem.Application.Abstraction.Models.Auth;
using Appeanza.ExaminationManagementSystem.Application.Abstraction.Services.Auth;
using Appeanza.ExaminationManagementSystem.Application.Exceptions;
using Appeanza.ExaminationManagementSystem.Domain.Emtities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Appeanza.ExaminationManagementSystem.Application.Services.Auth
{
    public class AuthService 
        (
            //IMapper mapper, // Maybe Will Be Use In The Future
            IOptions<JwtSettings> _jwtSettings,
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager
        ): IAuthService
    {
        private readonly JwtSettings _jwtSettings = _jwtSettings.Value;

        public IHttpContextAccessor? HttpContextAccessor { get; set; }

        public async Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email!);
            var userRole = await _userManager.GetRolesAsync(user!);
            if (user is null) throw new BadRequestException("user was not found");

            var userDto = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Role = userRole.FirstOrDefault()!,
                Token = await GenerateTokenAsync(user),
            };

            return userDto;
        }
        public async Task<bool> EmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
        public async Task<bool> UserNameExists(string username)
        {
            return await _userManager.FindByNameAsync(username) is not null;
        }
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user is null) throw new UnAuthorizedException("Invalid Login.");
            var userRoles = _userManager.GetRolesAsync(user);
            
            var result = await _signInManager.CheckPasswordSignInAsync(user , model.Password , lockoutOnFailure: true);
            
            if (result.IsNotAllowed) throw new UnAuthorizedException("Account Not Confirmed Yet.");
            
            if (result.IsLockedOut) throw new UnAuthorizedException("Account is locked.");
        
            if(!result.Succeeded) throw new UnAuthorizedException($"Invalid login.");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Role = userRoles.Result.FirstOrDefault() ?? "not Setted",
                Token = await GenerateTokenAsync(user)
            };
            var activateRefreshToken = user.RefreshToken.FirstOrDefault(token => token.IsActive);
            if (activateRefreshToken is not null)
            {
                response.RefreshToken = activateRefreshToken.Token;
                response.RefreshTokenExpiresOn = activateRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GetRefreshToken();
                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiresOn = refreshToken.ExpiresOn;
                user.RefreshToken.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            SetRefreshTokenInCookies(response.RefreshToken, response.RefreshTokenExpiresOn);

            return response;
        }
        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
          
            if (EmailExists(model.Email).Result) throw new BadRequestException("Email Already Exists");

            if (UserNameExists(model.UserName).Result) throw new BadRequestException("Username Already Taken");

            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
                IsDeleted = false
            };

            var result = await _userManager.CreateAsync(user , model.Password);

            if (!result.Succeeded) throw new ValidationException("Something went wrong during registeration")
            {
                Errors = result.Errors.Select(E => E.Description)
            };

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = model.DisplayName,
                Email = model.Email,
                Role = model.RoleName,
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }
        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var rolesAsClaims = new List<Claim>();

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                rolesAsClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                var identityRole = await _userManager.FindByNameAsync(role);
                if (identityRole is not null)
                {
                    var ClaimsForRole = new List<Claim>();
                    var claimsForRole = await _userManager.GetClaimsAsync(identityRole);
                    roleClaims.AddRange(claimsForRole);
                }
            }

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email , user.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName , user.DisplayName)
            }
            .Union(userClaims)
            .Union(rolesAsClaims)
            .Union(roleClaims);

            var symemetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symemetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenObj = new JwtSecurityToken
                (
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }
        private RefreshToken GetRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
            };
        }
        private void SetRefreshTokenInCookies(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };
            HttpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

    }
}
