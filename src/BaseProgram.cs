using System.Text.Json.Serialization;
using AWS.Lambda.Powertools.Tracing;
using Lambifast.Filters;
using Lambifast.Logging;
using Lambifast.Middlewares;
using Lambifast.Services;
using Lambifast.Services.Impl;
using Lambifast.Settings;
using Lambifast.Validations;

namespace Lambifast;

public abstract class BaseProgram<T>
    where T : BaseProgram<T>
{
    protected Task RunAsync(String[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services, builder.Configuration);
        ConfigureLogging(builder.Logging);
        var app = builder.Build();
        Configure(app);
        // Register Tracing for all AWS Services
        Tracing.RegisterForAllServices();
        return app.RunAsync();
    }

    private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        #region Aws Lambda Hosting
        
        services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
        
        #endregion
        
        #region Controllers

        services
            .AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ValidationFilter>();

                // Remove Validator
                options.ModelValidatorProviders.Clear();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        #endregion
        
        #region Swagger
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.UseInlineDefinitionsForEnums();
        });

        #endregion
        
        #region Http Client

        services.AddHttpClient();

        #endregion
        
        #region AutoMapper

        services.AddAutoMapper(new[] { typeof(T) });

        #endregion
        
        #region Validations

        services.RegisterValidatorsFromAssemblyContaining<T>();

        #endregion
        
        #region Services

        services.AddSingleton<IErrorResponseService, ErrorResponseService>();

        #endregion
        
        #region Options

        services.Configure<ObservabilitySettings>(configuration.GetSection("Observability"));

        #endregion

        InnerConfigureServices(services, configuration);
    }

    protected abstract void InnerConfigureServices(IServiceCollection services, IConfiguration configuration);

    private void ConfigureLogging(ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.AddAwsLogger();
    }
    
    private void Configure(WebApplication app)
    {
        #region Swagger

        app.UseSwagger();
        app.UseSwaggerUI();

        #endregion
        
        #region Routing + Authorization
        
        app.UseRouting();
        app.UseAuthorization();
        
        #endregion

        #region Middlewares

        app.UseMiddleware<RequestBodyLoggingMiddleware>();
        app.UseMiddleware<ResponseBodyLoggingMiddleware>();

        #endregion
        
        app.MapControllers();
    }
}