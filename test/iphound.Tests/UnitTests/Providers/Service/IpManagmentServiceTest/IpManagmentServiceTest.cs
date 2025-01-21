using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.CacheService;
using iphound.API.Providers.Service.DatabaseService;
using iphound.API.Providers.Service.Ip2cService;
using iphound.API.Providers.Service.IpManagmentService;
using iphound.Tests.Builders;
using Moq;

namespace iphound.Tests.UnitTests.Providers.Service.IpManagmentServiceTest
{
    public class IpManagmentServiceTest
    {
        private readonly Mock<IDatabaseService> _dbService;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<IIp2cService> _ip2cService;

        public IpManagmentServiceTest()
        {
            _dbService = new Mock<IDatabaseService>();
            _cacheService = new Mock<ICacheService>();
            _ip2cService = new Mock<IIp2cService>();
        }

        [Fact]
        public async Task Should_Fetch_Ip2c()
        {
            var ip = IpRequestBuilder.Build();
            var ipInfoResponse = IpInfoResponseBuilder.Build(ip);

            _cacheService
                .Setup(x => x.GetAsync(It.Is<string>(x => x == ip)))
                .ReturnsAsync((string?)null);

            _dbService
                .Setup(x => x.GetIpInfoAsync(It.Is<string>(x => x == ip)))
                .ReturnsAsync((IpInfoResponse?)null);

            _ip2cService
                .Setup(x => x.FetchIpInfo(It.Is<string>(x => x == ip)))
                .ReturnsAsync(ipInfoResponse);

            var service = CreateService();

            var result = await service.FetchDataAsync(ip);

            Assert.NotNull(result);
            Assert.Equal(result.IpAddress, ip);

            _ip2cService.Verify(x => x.FetchIpInfo(It.Is<string>(x => x == ip)), Times.Once);
            _cacheService.Verify(x => x.SetAsync(It.Is<string>(x => x == ip), It.IsAny<IpInfoResponse>(), null), Times.Once);
            _dbService.Verify(x => x.SaveIpInfoAsync(It.Is<IpInfoResponse>(x => x.IpAddress == ip)), Times.Once);

        }



        private IpManagmentService CreateService() => new IpManagmentService(_cacheService.Object, _dbService.Object, _ip2cService.Object);
    }
}
