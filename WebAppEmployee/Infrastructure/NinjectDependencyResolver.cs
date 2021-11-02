using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppEmployee.Data.Interfaces.Repository;
using WebAppEmployee.Data.Interfaces.Service;
using WebAppEmployee.Data.Repositories;
using WebAppEmployee.Domain.Services;

namespace WebAppEmployee.Infrastructure
{
    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private static IKernel Kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            Kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            //Base
            //Kernel.Bind<IMapper>().ToMethod(_ => AutoMapperResolver.InitMapper()).InSingletonScope();

            RegisterRepositories();
            RegisterServices();
        }

        private void RegisterRepositories()
        {
            Kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Kernel.Bind<IPositionRepository>().To<PositionRepository>();

        }

        private void RegisterServices()
        {
            Kernel.Bind<IEmployeeService>().To<EmployeeService>();
            Kernel.Bind<IPositionService>().To<PositionService>();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}