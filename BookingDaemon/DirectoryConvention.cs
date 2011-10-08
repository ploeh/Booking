using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.Core;
using System.IO;

namespace Ploeh.Samples.Booking.Daemon
{
    public class DirectoryConvention : ISubDependencyResolver
    {
        private readonly IKernel kernel;

        public DirectoryConvention(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType == typeof(DirectoryInfo);
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return this.kernel.Resolve(dependency.DependencyKey, typeof(DirectoryInfo));
        }
    }
}
