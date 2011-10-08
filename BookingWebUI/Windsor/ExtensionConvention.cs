using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.Core;

namespace Ploeh.Samples.Booking.WebUI.Windsor
{
    public class ExtensionConvention : ISubDependencyResolver
    {
        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType == typeof(string)
                && dependency.DependencyKey == "extension";
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return "txt";
        }
    }
}