namespace FCG.Users.WebAPI.Configurations
{
    public static class LoggingConfig
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // 1. Adiciona o SDK do Application Insights (Telemetria de requisições, etc.)
            builder.Services.AddApplicationInsightsTelemetry();

            // 2. Configura especificamente o Logging para o Application Insights
            builder.Logging.AddApplicationInsights(
                configureTelemetryConfiguration: (config) =>
                    config.ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"],
                configureApplicationInsightsLoggerOptions: (options) => 
                {
                    options.IncludeScopes = true;
                    options.TrackExceptionsAsExceptionTelemetry = false;
                }
            );

            builder.Logging.SetMinimumLevel(LogLevel.Information); // Set a minimum log level
        }
    }
}
