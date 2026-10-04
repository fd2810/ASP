using Ninject;
using System.Web.Mvc;
using DependencyInjectionDemo.Services;

namespace DependencyInjectionDemo.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterDependencies()
        {
            var kernel = new StandardKernel();

            // Tell Ninject: when someone asks for IStudentService, give StudentService
            kernel.Bind<IStudentService>().To<StudentService>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(System.Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(System.Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}
