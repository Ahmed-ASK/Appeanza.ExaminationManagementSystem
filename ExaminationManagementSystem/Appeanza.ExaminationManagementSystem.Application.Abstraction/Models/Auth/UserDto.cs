using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Application.Abstraction.Models.Auth
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string Role{ get; set; }
        public required string Token { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresOn { get; set; }
    }
}
