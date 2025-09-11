using Appeanza.ExaminationManagementSystem.Application.Abstraction.Services.Auth;

namespace Appeanza.ExaminationManagementSystem.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IAuthService AuthService{ get; }
    }
}
