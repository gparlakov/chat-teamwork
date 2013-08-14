using Chat.Data;
using Chat.Models;
using Chat.Repositories;
using Chat.Repository;
using Chat.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http.Dependencies;

namespace Chat.Services.Resolvers
{
    public class DependencyResolver: IDependencyResolver
    {
        private static DbContext chatContext = new ChatContext();

        private static IRepository<User> repository = new UserRepository(chatContext);


        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(UsersController))
            {
                return new UsersController(repository);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
        }
    }
}