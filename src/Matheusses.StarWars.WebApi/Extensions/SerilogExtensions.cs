using Serilog;

public static class SerilogExtensions
    {
        public static void AddSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string applicationName)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("ApplicationName", $"API Star Wars - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}")
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .MinimumLevel.Debug()
                .WriteTo.File("logs.txt")
                .CreateLogger();
        }
    }