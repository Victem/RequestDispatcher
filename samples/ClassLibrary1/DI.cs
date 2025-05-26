using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static class DI
    {
        public static IServiceCollection AddTestLibrary( this IServiceCollection services)
        {
            return services.AddInnerTestLibrary();
        }
    }
}
