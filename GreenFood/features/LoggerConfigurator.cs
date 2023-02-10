﻿using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace GreenFood.Web.features
{
    public static class LoggerConfigurator
    {
        static public void ConfigureLog(IConfigurationRoot configuration)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .WriteTo.Debug()
                        .WriteTo.Elasticsearch(ConfigureELS(configuration, env!))
                        .CreateLogger();
        }

        static private ElasticsearchSinkOptions ConfigureELS(
            IConfigurationRoot configuration,
            string env)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ELKConfiguration:Uri"]!))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower()}-{env.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
