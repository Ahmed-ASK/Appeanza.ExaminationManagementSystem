using Microsoft.AspNetCore.Identity;

namespace Appeanza.ExaminationManagementSystem.Domain.Emtities.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public required string DisplayName { get; set; }
        public ICollection<RefreshToken> RefreshToken { get; set; } = new HashSet<RefreshToken>();
        public required bool IsDeleted { get; set; }

    }
}
