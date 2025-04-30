using HttpClientLogger.SimpleHttpClientLogger;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace HttpClientLogger.Tests.SimpleHttpClientLogger
{
    [TestFixture]
    public class ServiceCollectionExtensionsTests
    {
        [Test]
        public void AddSimpleHttpClientLogger_ShouldRegisterLogger()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddLogging();

            // Act
            services.AddHttpClient("TestClient").ReplaceLogger();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<HttpClientLogger.SimpleHttpClientLogger.SimpleHttpClientLogger>();
            Assert.That(logger, Is.Not.Null);
        }
    }
}
