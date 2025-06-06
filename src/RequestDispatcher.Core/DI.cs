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
        services.AddSingleton<IRequestDispatcher, RequestDispatcher>();
        services.AddSingleton(typeof(IRequestHandlerInvoker<,>), typeof(RequestHandlerInvoker<,>));
        services.AddSingleton(typeof(IMessageHandlerInvoker<,>), typeof(MessageHandlerInvoker<,>));
        services.AddSingleton(typeof(IStreamRequestHandlerInvoker<,>), typeof(StreamRequestHandlerInvoker<,>));

        //services.AddSingleton(typeof(IRequestBasePipelineBehavior<,>), typeof(LoggingBehavior<,>));
        //services.AddSingleton(typeof(IRequestBasePipelineBehavior<,>), typeof(MetricsBehavior<,>));
        //services.AddSingleton(typeof(IStreamRequestPipelineBehavior<,>), typeof(LoggingStreamBehavior<,>));

        services.AddScoped<IRequestDispatcher, RequestDispatcher>();
        services.AddScoped(typeof(IRequestHandlerInvoker<,>), typeof(RequestHandlerInvoker<,>));
        services.AddScoped(typeof(IMessageHandlerInvoker<,>), typeof(MessageHandlerInvoker<,>));
        services.AddScoped(typeof(IStreamRequestHandlerInvoker<,>), typeof(StreamRequestHandlerInvoker<,>));

        //services.AddScoped(typeof(IRequestBasePipelineBehavior<,>), typeof(LoggingBehavior<,>));
        //services.AddScoped(typeof(IRequestBasePipelineBehavior<,>), typeof(MetricsBehavior<,>));
        //services.AddScoped(typeof(IStreamRequestPipelineBehavior<,>), typeof(LoggingStreamBehavior<,>));

        services.AddSingleton<IMessageHandlerMapping, DefaultMessageHandlerMapping>();
        services.AddSingleton<IRequestHanlderMapping, DefaultRequestHandlerMapping>();
        services.AddSingleton<IStreamRequestHanlderMapping, DefaultStreamRequestHandlerMapping>();
        
        return services;
    }
}
