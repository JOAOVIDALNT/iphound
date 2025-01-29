using Azure;
using iphound.API.Providers.Service.Ip2cService;
using iphound.Tests.Builders;
using Moq;
using Moq.Protected;
using System.Net;

namespace iphound.Tests.UnitTests.Providers.Service.Ip2cServiceTest
{
    public class Ip2cServiceTest
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;

        public Ip2cServiceTest()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict); //MockBehavior permite apenas chamadas de métodos configurados explicitamente
        }

        [Fact]
        public async Task Should_Fetch_Succeed()
        {
            var ip = IpRequestBuilder.Build();
            var IpResponse = IpInfoResponseBuilder.Build(ip);

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"1;{IpResponse.TwoLetterCode};{IpResponse.ThreeLetterCode};{IpResponse.CountryName}")
            };
            
            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(httpResponse);

            var service = CreateService();
             
            var result = await service.FetchIpInfo(ip);

            Assert.NotNull(result);
            Assert.Equal(result.IpAddress, ip);
        }

        [Fact]
        public async Task Should_Fetch_Fail()
        {
            var ip = IpRequestBuilder.Build();

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent($"0;;;WRONG INPUT")
            };

            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(httpResponse);

            var service = CreateService();

            var result = await service.FetchIpInfo(ip);

            Assert.NotNull(result);
            Assert.False(result.Success);
        }

        private Ip2cService CreateService()
        {
            var httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://ip2c.org")
            };

            return new Ip2cService(httpClient);
        }

    }
}
