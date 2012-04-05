using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ninject;

using DF.Core;
using DF.Domain.Abstract;
using DF.Domain.Concrete;
using DF.Repositories;

namespace DF.Controllers
{
    public class DependencyControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public DependencyControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //context & UoW
            ninjectKernel.Bind(typeof(System.Data.Objects.ObjectContext)).To(typeof(DFContext)).InRequestScope().WithConstructorArgument("connectionString", DF.Core.Components.AppSettings.CONNECTION_STRING);           
           ninjectKernel.Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument("connectionString", DF.Core.Components.AppSettings.CONNECTION_STRING);

           //user
           ninjectKernel.Bind<IUser>().To<DF.Domain.Concrete.User>();
           ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
           ninjectKernel.Bind<IUserManager>().To<UserManager>();

           //Dealer
           ninjectKernel.Bind<IDeal>().To<DF.Domain.Concrete.Deal>();
           ninjectKernel.Bind<IDealRepository>().To<DealRepository>();
           ninjectKernel.Bind<IDealManager>().To<DealManager>();

            ////region
            //ninjectKernel.Bind<IRegion>().To<DF.Domain.Concrete.Region>();
            //ninjectKernel.Bind<IRegionRepository>().To<RegionRepository>();
            //ninjectKernel.Bind<IRegionManager>().To<RegionManager>();

            ////regionlocation
            //ninjectKernel.Bind<IRegionLocation>().To<DF.Domain.Concrete.RegionLocation>();
            //ninjectKernel.Bind<IRegionLocationRepository>().To<RegionLocationRepository>();
            //ninjectKernel.Bind<IRegionLocationManager>().To<RegionLocationManager>();

            ////regionLocationtype
            //ninjectKernel.Bind<IRegionLocationType>().To<DF.Domain.Concrete.RegionLocationType>();
            //ninjectKernel.Bind<IRegionLocationTypeRepository>().To<RegionLocationTypeRepository>();
            //ninjectKernel.Bind<IRegionLocationTypeManager>().To<RegionLocationTypeManager>();

           //menu
           ninjectKernel.Bind<IMenu>().To<Menu>();
           ninjectKernel.Bind<IMenuRepository>().To<MenuRepository>();
           ninjectKernel.Bind<IMenuManager>().To<MenuManager>();

        }
    }
    
}
