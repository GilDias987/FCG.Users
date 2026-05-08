using Serilog;

namespace FCG.Users.WebAPI.Configurations
{
    public static class LoggingConfig
    {
        public static void AddLogging(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(builder.Configuration)
                     .Enrich.FromLogContext()
                     .WriteTo.Console()
                     .WriteTo.Debug()
                     .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
