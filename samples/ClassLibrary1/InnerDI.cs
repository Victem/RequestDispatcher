using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    internal static class InnerDi
    {
        internal static IServiceCollection AddInnerTestLibrary(this IServiceCollection services)
        {
            services.AddTransient<Test>();
            return services;
        }
    }
}
