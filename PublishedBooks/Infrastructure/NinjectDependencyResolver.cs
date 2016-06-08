using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using PublishedBooksDAL.Repositories;
using PublishedBooks.Infrastructure.Security;

namespace PublishedBooks.Infrastructure {

    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings() {    
            kernel.Bind(typeof(IRepository<>)).To(typeof(MongoDbRepository<>)).WhenInjectedInto(typeof(LogRepository<>));
            kernel.Bind(typeof(IRepository<>)).To(typeof(LogRepository<>));
            kernel.Bind<IAuthProvider>().To<MongoAuthProvider>();
        }
    }
}
