using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;

using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Core.Behaviors;
using RequestDispatcher.Core.Options;
using RequestDispatcher.Core.Processing.Messages;
using RequestDispatcher.Core.Processing.Requests;
using RequestDispatcher.Core.Processing.Sreams;
using RequestDispatcher.Core.RequestMapping;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestDispatcher.Core;

public static class DI
{
    public static IServiceCollection AddDispatcher(this IServiceCollection services, Action<RequestDispatcherOptions> options = default)
    {
        //services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        //services.TryAddSingleton(serviceProvider =>
        //{
        //    var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
        //    var policy = new DefaultPooledObjectPolicy<InvokeParametersBuffer>();
        //    return provider.Create(policy);
        //});
        services.AddSingleton<IDispatcher, Dispatcher>();
        services.AddSingleton(typeof(IRequestHandlerInvoker<,>),typeof(RequestHandlerInvoker<,>));
        services.AddSingleton(typeof(IMessageHandlerInvoker<,>), typeof(MessageHandlerInvoker<,>));
        services.AddSingleton(typeof(IStreamRequestHandlerInvoker<,>), typeof(StreamRequestHandlerInvoker<,>));

        services.AddSingleton<IRequestHanlderMapping, DefaultRequestHandlerMapping>();
        services.AddSingleton<IMessageHandlerMapping, DefaultMessageHandlerMapping>();
        services.AddSingleton<IStreamRequestHanlderMapping, DefaultStreamRequestHandlerMapping>();
        services.AddScoped(typeof(IRequestBasePipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IRequestBasePipelineBehavior<,>), typeof(MetricsBehavior<,>));
        services.AddScoped(typeof(IStreamRequestPipelineBehavior<,>), typeof(LoggingStreamBehavior<,>));
        return services;
    }
}
