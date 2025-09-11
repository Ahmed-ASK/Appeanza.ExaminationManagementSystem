using Appeanza.ExaminationManagementSystem.Application.Abstraction.Services;
using Appeanza.ExaminationManagementSystem.Application.Abstraction.Services.Auth;
using Appeanza.ExaminationManagementSystem.Application.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeanza.ExaminationManagementSystem.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authServices;

        public ServiceManager(Func<IAuthService> authServiceFactory)
        {
            _authServices = new Lazy<IAuthService>(authServiceFactory , LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IAuthService AuthService => _authServices.Value;
    
        
    }
}
