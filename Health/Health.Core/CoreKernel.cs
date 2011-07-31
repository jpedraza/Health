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
        private IRoleRepository<IRole> _roleRepo;
        private IUserRepository<IUser> _userRepo;
        private ICandidateRepository<ICandidate> _candRepo;
        private IAuthorizationService<IUserCredential> _authServ;
        private IRegistrationService<ICandidate> _regServ;
        protected IKernel DIKernel { get; set; }

        public CoreKernel(IKernel di_kernel)
        {
            DIKernel = di_kernel;
        }

        public IRoleRepository<IRole> RoleRepo
        {
            get { return _roleRepo ?? (_roleRepo = DIKernel.Get<IRoleRepository<IRole>>(this)); }
        }

        public IUserRepository<IUser> UserRepo
        {
            get { return _userRepo ?? (_userRepo = DIKernel.Get<IUserRepository<IUser>>(this)); }
        }

        public ICandidateRepository<ICandidate> CandRepo
        {
            get { return _candRepo ?? (_candRepo = DIKernel.Get<ICandidateRepository<ICandidate>>(this)); }
        }

        public IAuthorizationService<IUserCredential> AuthServ
        {
            get { return _authServ ?? (_authServ = DIKernel.Get<IAuthorizationService<IUserCredential>>(this)); }
        }

        public IRegistrationService<ICandidate> RegServ
        {
            get { return _regServ ?? (_regServ = DIKernel.Get<IRegistrationService<ICandidate>>(this)); }
        }
    }
}