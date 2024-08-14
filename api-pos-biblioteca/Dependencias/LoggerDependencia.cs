using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace api_pos_biblioteca.Dependencias
{
    public static class LoggerDependencia
    {
        public static ILoggingBuilder AgregarLogging(this ILoggingBuilder logging)
        {
            var loggerConfi = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("./Recursos/serilog-config.json").Build())
                .Enrich.FromLogContext().CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog(loggerConfi);

            return logging;
        }
    }
}
