using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.ConsoleApp;

public static class DI
{
    public static TService HandlerFactory<TService>(this IServiceProvider provider, Type type)
    {
        return (TService)provider.GetRequiredService(type);
    }
}
