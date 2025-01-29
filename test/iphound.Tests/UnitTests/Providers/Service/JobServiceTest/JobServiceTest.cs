using iphound.API.Data.Repositories.Interfaces;
using iphound.API.Models.Entities;
using iphound.API.Providers.Service.CacheService;
using iphound.API.Providers.Service.DatabaseService;
using iphound.API.Providers.Service.Ip2cService;
using iphound.API.Providers.Service.JobService;
using iphound.API.Utils;
using iphound.Tests.Builders;
using iphound.Tests.Utils;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;

namespace iphound.Tests.UnitTests.Providers.Service.JobServiceTest
{
    public class JobServiceTest
    {
        private readonly Mock<IIpAddressRepository> _ipAddressRepositoryMock;
        private readonly Mock<IDatabaseService> _databaseServiceMock;
        private readonly Mock<IIp2cService> _ip2cServiceMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly Mock<ILogger<UpdateDatabase>> _loggerMock;

        public JobServiceTest()
        {
            _ipAddressRepositoryMock = new Mock<IIpAddressRepository>();
            _databaseServiceMock = new Mock<IDatabaseService>();
            _ip2cServiceMock = new Mock<IIp2cService>();
            _cacheServiceMock = new Mock<ICacheService>();
            _loggerMock = new Mock<ILogger<UpdateDatabase>>();
        }

        [Fact]
        public async Task Should_Fetch_And_Update()
        {
            var list = IpAddressBuilder.Build();

            var obj = list.FirstOrDefault(x => x.Id == 2);
            var obj2 = JsonConvert.DeserializeObject<IpAddress>(JsonConvert.SerializeObject(obj));

            obj2.Country.ThreeLetterCode = Util.RandomLetters(3);
            obj2.Country.TwoLetterCode = Util.RandomLetters(2);
            obj2.Country.Name = Util.DiferentCountry(obj2.Country.Name);
           

            _ipAddressRepositoryMock
                .Setup(x => x.GetIpAddressListWithCountryInfo(100,1))
                .ReturnsAsync(list);

            _ip2cServiceMock
                .Setup(x => x.FetchIpInfo(obj.Ip))
                .ReturnsAsync(obj2.MapFromEntity());

            _ip2cServiceMock
                .Setup(x => x.FetchIpInfo(It.Is<string>(x => x != obj.Ip)))
                .ReturnsAsync(new API.Models.HttpModels.Responses.IpInfoResponse { Success = false });

            var service = CreateService();

            await service.ExecuteAsync();

            _databaseServiceMock
                .Verify(x => x.UpdateIpInfoAsync(It.Is<IpAddress>(x => x.Ip == obj.Ip)), Times.Once);

            _cacheServiceMock
                .Verify(x => x.RemoveAsync(obj!.Ip), Times.Once);
        }

        [Fact]
        public async Task Should_Fetch_And_Not_Update()
        {
            var list = IpAddressBuilder.Build();
            var obj = list.FirstOrDefault(x => x.Id == 2);


            _ipAddressRepositoryMock
                .Setup(x => x.GetIpAddressListWithCountryInfo(100, 1))
                .ReturnsAsync(list);

            _ip2cServiceMock
                .Setup(x => x.FetchIpInfo(obj.Ip))
                .ReturnsAsync(obj.MapFromEntity());

            _ip2cServiceMock
                .Setup(x => x.FetchIpInfo(It.Is<string>(x => x != obj.Ip)))
                .ReturnsAsync(new API.Models.HttpModels.Responses.IpInfoResponse { Success = false });

            var service = CreateService();

            await service.ExecuteAsync();

            _databaseServiceMock
                .Verify(x => x.UpdateIpInfoAsync(It.IsAny<IpAddress>()), Times.Never);

            _cacheServiceMock
                .Verify(x => x.RemoveAsync(obj!.Ip), Times.Never);

        }

        public UpdateDatabase CreateService() => new(_ipAddressRepositoryMock.Object, _ip2cServiceMock.Object, _cacheServiceMock.Object, _loggerMock.Object, _databaseServiceMock.Object);
    }
}
