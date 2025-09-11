using Appeanza.ExaminationManagementSystem.Application.Abstraction.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Application.Abstraction.Services.Auth
{
    public interface IAuthService
    {

        Task<UserDto> LoginAsync(LoginDto model);
        Task<UserDto> RegisterAsync(RegisterDto model);
        Task<bool> EmailExists(string email);
        Task<bool> UserNameExists(string username);
        Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);


    }
}
