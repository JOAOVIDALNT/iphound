using iphound.API.Data.Repositories.Interfaces;
using iphound.API.Models.Entities;
using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.DatabaseService;
using iphound.API.Utils;
using iphound.Tests.Builders;
using Moq;

namespace iphound.Tests.UnitTests.Providers.Service.DatabaseServiceTest
{
    public class DatabaseServiceTest
    {
        private readonly Mock<IIpAddressRepository> _ipAddressRepository;
        private readonly Mock<ICountryRepository> _countryRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public DatabaseServiceTest()
        {
            _ipAddressRepository = new Mock<IIpAddressRepository>();
            _countryRepository = new Mock<ICountryRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task Should_Return_IpInfo()
        {
            var ip = IpRequestBuilder.Build();
            var ipResponse = IpInfoResponseBuilder.Build(ip);

            _ipAddressRepository
                .Setup(x => x.GetIpAddressWithCountryInfo(It.Is<string>(x => x == ip)))
                .ReturnsAsync(ipResponse.MapToEntity());

            var service = CreateService();

            var result = await service.GetIpInfoAsync(ip);

            Assert.NotNull(result);
            Assert.Equal(result.IpAddress, ip);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Should_Return_Null_IpInfo()
        {
            var ip = IpRequestBuilder.Build();

            _ipAddressRepository
                .Setup(x => x.GetIpAddressWithCountryInfo(It.Is<string>(x => x == ip)))
                .ReturnsAsync((IpAddress?)null);

            var service = CreateService();

            var result = await service.GetIpInfoAsync(ip);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Save_IpInfo()
        {
            var ip = IpRequestBuilder.Build();
            var ipResponse = IpInfoResponseBuilder.Build(ip);

            var service = CreateService();

            await service.SaveIpInfoAsync(ipResponse);

            _ipAddressRepository
                .Verify(x => x.CreateAsync(It.Is<IpAddress>(x => x.Ip == ip)), Times.Once);

            _unitOfWork
                .Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task Should_Update_IpInfo()
        {
            var ip = IpRequestBuilder.Build();
            var ipResponse = IpInfoResponseBuilder.Build(ip);

            var service = CreateService();

            await service.UpdateIpInfoAsync(ipResponse.MapToEntity());

            _ipAddressRepository
                .Verify(x => x.Update(It.Is<IpAddress>(x => x.Ip == ip)), Times.Once);

            _unitOfWork
                .Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public void Should_Return_IpInfoReport()
        {
            var service = CreateService();

            _countryRepository
                .Setup(x => x.GetCountryReport()).Returns(new List<CountryReportResponse>());

            var list = service.GetCountryReport();

            Assert.NotNull(list);
        }

        private DatabaseService CreateService() => new(_ipAddressRepository.Object, _countryRepository.Object, _unitOfWork.Object);
    }
}
