//using DF.Infrastructure;
//using DF.DomainModel.Abstract;
//using DF.DomainModel.Concrete;
//using DF.Repositories;


//[assembly: WebActivator.PreApplicationStartMethod(typeof(DF.UI.App_Start.NinjectMVC3), "Start")]
//[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DF.UI.App_Start.NinjectMVC3), "Stop")]

//namespace DF.UI.App_Start
//{
//    using System.Reflection;
//    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//    using Ninject;
//    using Ninject.Web.Mvc;

//    public static class NinjectMVC3 
//    {
//        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

//        /// <summary>
//        /// Starts the application
//        /// </summary>
//        public static void Start() 
//        {
//            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
//            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
//            bootstrapper.Initialize(CreateKernel);
//        }
        
//        /// <summary>
//        /// Stops the application.
//        /// </summary>
//        public static void Stop()
//        {
//            bootstrapper.ShutDown();
//        }
        
//        /// <summary>
//        /// Creates the kernel that will manage your application.
//        /// </summary>
//        /// <returns>The created kernel.</returns>
//        private static IKernel CreateKernel()
//        {
//            var kernel = new StandardKernel();
//            RegisterServices(kernel);
//            return kernel;
//        }

//        /// <summary>
//        /// Load your modules or register your services here!
//        /// </summary>
//        /// <param name="kernel">The kernel.</param>
//        private static void RegisterServices(IKernel kernel)
//        {
//            kernel.Bind(typeof(System.Data.Objects.ObjectContext)).To(typeof(DAL.DFContext)).WithConstructorArgument("connectionString", DF.Core.Components.AppSettings.CONNECTION_STRING);
//            kernel.Bind<IUser>().To<DAL.User>();
//            kernel.Bind<IRepository<IUser>>().To<UserRepository>();
//        }        
//    }
//}
