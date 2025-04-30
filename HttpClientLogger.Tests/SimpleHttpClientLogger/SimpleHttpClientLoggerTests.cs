using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;

namespace HttpClientLogger.Tests.SimpleHttpClientLogger
{
    [TestFixture]
    public class SimpleHttpClientLoggerTests
    {
        private Mock<ILogger<HttpClientLogger.SimpleHttpClientLogger.SimpleHttpClientLogger>> _loggerMock;
        private HttpClientLogger.SimpleHttpClientLogger.SimpleHttpClientLogger _sut;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HttpClientLogger.SimpleHttpClientLogger.SimpleHttpClientLogger>>();
            _sut = new HttpClientLogger.SimpleHttpClientLogger.SimpleHttpClientLogger(_loggerMock.Object);
        }
        [Test]
        public void LogRequestStart_ShouldReturnNull()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com");

            // Act
            var result = _sut.LogRequestStart(request);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void LogRequestStop_ShouldLogSuccessfulRequest()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.example.com/users");
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            var elapsed = TimeSpan.FromMilliseconds(125.5);

            // Act
            _sut.LogRequestStop(null, request, response, elapsed);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Processed HTTP request POST https://api.example.com/users status - 201 after 125.5ms", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Test]
        public void LogRequestFailed_ShouldLogFailedRequest()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/orders");
            var exception = new HttpRequestException("Connection failed");
            var elapsed = TimeSpan.FromMilliseconds(250.75);

            // Act
            _sut.LogRequestFailed(null, request, null, exception, elapsed);

            // Assert
            _loggerMock.Verify(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) =>
                    v.ToString().Contains("Processed HTTP request GET https://api.example.com/orders status - failed after 250.8ms")),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Test]
        public void LogRequestStop_ShouldHandleNullResponse()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, "https://api.example.com/data/123");
            var elapsed = TimeSpan.FromMilliseconds(50);

            // Act
            _sut.LogRequestStop(null, request, null, elapsed);

            // Assert
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Processed HTTP request DELETE https://api.example.com/data/123 status - 0 after 50.0ms", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}
