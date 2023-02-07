using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace GreenFood.features
{
    public static class ConfigSettings
    {
        static public void ConfigureLog(ConfigurationBuilder config)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuraton = config
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(ConfigureELS(configuraton, env))
                .CreateLogger();
        }

        static private ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuraton, string env)
        {
            return new ElasticsearchSinkOptions(new Uri(configuraton["ELKConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
