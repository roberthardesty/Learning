using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using BusinessObjectExamples;
using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DaemonExample
{
    public class DIExamples
    {
        private static IServiceLocator SetupServiceLocator()
        {
            var builder = new ContainerBuilder();

            // registering some types as singleton
            var coreAssembly = Assembly.GetAssembly(typeof(BusinessLogicItem));
            // this grabs all types that implement the interface IRequireSingleton
            var singletonTypes = coreAssembly.GetTypes()
                .Where(t => t.IsAssignableTo<IRequireSingleton>())
                .ToArray();
            // this grabs everything else
            var nonSingletonTypes = coreAssembly.GetTypes()
                .Except(singletonTypes)
                .ToArray();
            // register the IRequireSingleton's
            builder.RegisterTypes(singletonTypes)
                   .SingleInstance();
            // register the others
            builder.RegisterTypes(nonSingletonTypes);

            // registering all types in an assembly as their implemented interfaces
            var serviceAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(serviceAssembly)
                   .AsImplementedInterfaces();

            var serviceCollection = new ServiceCollection();

            builder.Populate(serviceCollection);

            var container = builder.Build();

            var serviceLocator = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            return serviceLocator;
        }
    }
}
