using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Interfaces;
using BLL.Services;
using ORM;
using DAL.Concrete;
using DAL.Interfaces.Interfaces;
using Ninject.Web.Common;
using Ninject;
using DAL;
using System.Data.Entity;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            kernel.Bind<IPhotoService>().To<PhotoService>();

            kernel.Bind<ITagRepository>().To<TagRepository>();
            kernel.Bind<ITagService>().To<TagService>();

            kernel.Bind<IUserInformationRepository>().To<UserInformationRepository>();
            kernel.Bind<IUserInformationService>().To<UserInformationService>();

        }
    }
}
