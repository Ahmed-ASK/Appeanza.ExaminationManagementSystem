using Microsoft.EntityFrameworkCore;
namespace Appeanza.ExaminationManagementSystem.Domain.Emtities.Identity
{
    [Owned]
    public class RefreshToken
    {
        public required string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime RevokedOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsActive { get; set; } = true;
    }
}
