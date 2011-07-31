using System;
using System.Linq.Expressions;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;
using Health.Core.Ninject;
using Ninject;

namespace Health.Core
{
    public class CoreKernel : ICoreKernel
    {
        protected IKernel DIKernel { get; set; }

        public CoreKernel(IKernel di_kernel)
        {
            DIKernel = di_kernel;
            RoleRepo = DIKernel.Get<IRoleRepository<IRole>>(this);
            UserRepo = DIKernel.Get<IUserRepository<IUser>>(this);
            CandRepo = DIKernel.Get<ICandidateRepository<ICandidate>>(this);
            RegServ = DIKernel.Get<IRegistrationService<ICandidate>>(this);
            AuthServ = di_kernel.Get<IAuthorizationService<IUserCredential>>(this);
        }

        public IRoleRepository<IRole> RoleRepo { get; private set; }

        public IUserRepository<IUser> UserRepo { get; private set; }

        public ICandidateRepository<ICandidate> CandRepo { get; private set; }

        public IAuthorizationService<IUserCredential> AuthServ { get; private set; }

        public IRegistrationService<ICandidate> RegServ { get; private set; }

    }
}