using System.Text;
using Amazon.XRay.Recorder.Core;
using AWS.Lambda.Powertools.Tracing;
using Lambifast.Extensions;
using Lambifast.Settings;
using Microsoft.Extensions.Options;

namespace Lambifast.Middlewares;

public class RequestBodyLoggingMiddleware
{
    private RequestDelegate Next { get; }
    private ObservabilitySettings ObservabilitySettings { get; }

    public RequestBodyLoggingMiddleware(
        RequestDelegate next,
        IOptions<ObservabilitySettings> observabilityOptions)
    {
        Next = next;
        ObservabilitySettings = observabilityOptions.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        AWSXRayRecorder.Instance.BeginSubsegment("Request Call");
        if (ObservabilitySettings.TrackRequest)
        {
            var method = context.Request.Method;

            // Ensure the request body can be read multiple times
            context.Request.EnableBuffering();

            // Only if we are dealing with POST or PUT, GET and others shouldn't have a body
            if (context.Request.Body.CanRead && (method == HttpMethods.Post || method == HttpMethods.Put))
            {
                // Leave stream open so next middleware can read it
                using var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: 512, leaveOpen: true);

                var requestBody = await reader.ReadToEndAsync();

                // Reset stream position, so next middleware can read it
                context.Request.Body.Position = 0;

                // Write request body to App Insights
                Tracing.AddAnnotation("requestBody", requestBody);
                Tracing.AddAnnotation("requestHeaders", context.Request.Headers.SerializeJson(true));
            }
        }

        // Call next middleware in the pipeline
        await Next(context);
    }
}