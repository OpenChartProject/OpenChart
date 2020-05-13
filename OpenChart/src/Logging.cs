using Serilog.Core;
using Serilog.Events;

namespace OpenChart
{
    public class ShortLevelEnricher : ILogEventEnricher
    {
        public const string ShortLevelPropertyName = "ShortLevel";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var prop = propertyFactory.CreateProperty(
                ShortLevelPropertyName,
                GetShortLevel(logEvent.Level)
            );

            logEvent.AddOrUpdateProperty(prop);
        }

        public string GetShortLevel(LogEventLevel level)
        {
            if (level == LogEventLevel.Debug)
                return "DEBUG";
            else if (level == LogEventLevel.Error)
                return "ERROR";
            else if (level == LogEventLevel.Fatal)
                return "FATAL";
            else if (level == LogEventLevel.Information)
                return "INFO ";
            else if (level == LogEventLevel.Verbose)
                return "VERB ";
            else if (level == LogEventLevel.Warning)
                return "WARN ";

            return "";
        }
    }
}
