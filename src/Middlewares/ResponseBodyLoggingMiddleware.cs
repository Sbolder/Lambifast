using Amazon.XRay.Recorder.Core;
using AWS.Lambda.Powertools.Tracing;
using Lambifast.Extensions;
using Lambifast.Settings;
using Microsoft.Extensions.Options;

namespace Lambifast.Middlewares;

public class ResponseBodyLoggingMiddleware
{
    private RequestDelegate Next { get; }
    private ObservabilitySettings ObservabilitySettings { get; }
    private ILogger<ResponseBodyLoggingMiddleware> Logger { get; }

    public ResponseBodyLoggingMiddleware(
        RequestDelegate next,
        IOptions<ObservabilitySettings> observabilityOptions,
        ILogger<ResponseBodyLoggingMiddleware> logger)
    {
        Next = next;
        ObservabilitySettings = observabilityOptions.Value;
        Logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (ObservabilitySettings.TrackResponse)
        {
            var originalBodyStream = context.Response.Body;

            try
            {
                // Swap out stream with one that is buffered and suports seeking
                using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                // hand over to the next middleware and wait for the call to return
                await Next(context);

                // Read response body from memory stream
                memoryStream.Position = 0;
                var reader = new StreamReader(memoryStream);
                var responseBody = await reader.ReadToEndAsync();

                // Copy body back to so its available to the user agent
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBodyStream);

                // Write response body to App Insights
                Logger.LogInformation("Response Body: {}", responseBody);
                Tracing.AddAnnotation("responseBody", responseBody);
                var headers = context.Response.Headers.SerializeJson(true);
                Logger.LogInformation("Response Headers: {}", headers);
                Tracing.AddAnnotation("ResponseHeaders", headers);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
        else
        {
            await Next(context);
        }

        AWSXRayRecorder.Instance.EndSubsegment();
    }
}