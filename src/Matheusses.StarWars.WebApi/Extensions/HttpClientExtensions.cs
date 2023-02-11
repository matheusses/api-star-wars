using Polly;
using Refit;
using Serilog;


namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class HttpClientExtensions
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(3, onRetry: (message, retryCount) =>
                {
                    string msg = $"Retentativa: {retryCount}";
                    Console.Out.WriteLineAsync(msg);
                    Log.Information(msg);
                });
        }
    }
}