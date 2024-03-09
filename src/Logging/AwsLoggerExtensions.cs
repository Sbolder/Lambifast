using AWS.Lambda.Powertools.Logging;
using AWS.Lambda.Powertools.Logging.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace Lambifast.Logging;

public static class AwsLoggerExtensions
{
    public static ILoggingBuilder AddAwsLogger(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
        LoggerProviderOptions.RegisterProviderOptions<LoggerConfiguration, LoggerProvider>(builder.Services);
        return builder;
    }

    public static ILoggingBuilder AddAwsLogger(this ILoggingBuilder builder,
        Action<LoggerConfiguration> configure)
    {
        builder.AddAwsLogger();
        builder.Services.Configure(configure);
        return builder;
    }
}