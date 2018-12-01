using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Core;
using System;
using Nodinite.Serilog.ApiSink;
using Nodinite.Serilog.ApiSink.models;

namespace Nodinite.Serilog.Sink.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadSettingsFromAppSettingsTest()
        {
            // todo: implement moq
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //Logger log = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            //log.Information("Hello World");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InitiateLogger_MissingLogAgentValue()
        {
            var nodiniteApiUrl = "{Your Nodinite API Url}";
            var settings = new NodiniteLogEventSettings()
            {
                EndPointDirection = 0,
                EndPointTypeId = 0,
                EndPointUri = "Nodinite.Serilog.Sink.Tests.Serilog",
                EndPointName = "Nodinite.Serilog.Sink.Tests",
                OriginalMessageTypeName = "Serilog.LogEvent",
                ProcessingUser = "NODINITE",
                ProcessName = "Nodinite.Serilog.Sink.Tests",
                ProcessingMachineName = "NODINITE-DEV",
                ProcessingModuleName = "DOTNETCORE.TESTS",
                ProcessingModuleType = "DOTNETCORE.TESTPROJECT"
            };

            Logger log = new LoggerConfiguration()
                .WriteTo.NodiniteApiSink(nodiniteApiUrl, settings)
                .CreateLogger();
        }

        [TestMethod]
        public void LogContextProperties()
        {
            var nodiniteApiUrl = "{Your Nodinite API Url}";
            var settings = new NodiniteLogEventSettings()
            {
                LogAgentValueId = 503,
                EndPointDirection = 0,
                EndPointTypeId = 0,
                EndPointUri = "Nodinite.Serilog.Sink.Tests.Serilog",
                EndPointName = "Nodinite.Serilog.Sink.Tests",
                OriginalMessageTypeName = "Serilog.LogEvent",
                ProcessingUser = "NODINITE",
                ProcessName = "Nodinite.Serilog.Sink.Tests",
                ProcessingMachineName = "NODINITE-DEV",
                ProcessingModuleName = "DOTNETCORE.TESTS",
                ProcessingModuleType = "DOTNETCORE.TESTPROJECT"
            };

            ILogger log = new LoggerConfiguration()
                .WriteTo.NodiniteApiSink(nodiniteApiUrl, settings)
                .CreateLogger()
                .ForContext("CorrelationId", Guid.NewGuid())
                .ForContext("CustomerId", 12);

            log.Information($"Customer '12' imported");
        }
    }
}
