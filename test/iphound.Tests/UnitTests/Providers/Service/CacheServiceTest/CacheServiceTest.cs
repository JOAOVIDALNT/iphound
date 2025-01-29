using iphound.API.Models.HttpModels.Responses;
using iphound.API.Providers.Service.CacheService;
using iphound.Tests.Builders;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Newtonsoft.Json;
using System.Text;

namespace iphound.Tests.UnitTests.Providers.Service.CacheServiceTest
{
    public class CacheServiceTest
    {
        private readonly Mock<IDistributedCache> _cache;

        public CacheServiceTest()
        {
            _cache = new Mock<IDistributedCache>();
        }

        [Fact]
        public async Task Should_Return_Cached_Data()
        {
            var key = IpRequestBuilder.Build();
            var ipResponse = IpInfoResponseBuilder.Build(key);

            var returnExpected = JsonConvert.SerializeObject(ipResponse);
            var encodedValue = Encoding.UTF8.GetBytes(returnExpected);

            _cache
                .Setup(x => x.GetAsync(It.Is<string>(x => x == key), It.IsAny<CancellationToken>()))
                .ReturnsAsync(encodedValue);

            var service = CreateService();

            var result = await service.GetAsync(key);

            Assert.NotNull(result);
            Assert.Equal(result, returnExpected);
        }

        [Fact]
        public async Task Should_Return_Null_Data()
        {
            var key = IpRequestBuilder.Build();

            _cache
                .Setup(x => x.GetAsync(It.Is<string>(x => x == key), It.IsAny<CancellationToken>())) // Métodos de extensão do IDistrubutedCache não são válidos pra setup ou verify
                .ReturnsAsync((byte[]?)null);   

            var service = CreateService();

            var result = await service.GetAsync(key);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Save_Data()
        {
            var key = IpRequestBuilder.Build();
            var ipResponse = IpInfoResponseBuilder.Build(key);

            var mockCacheService = new Mock<ICacheService>(); // Cache da própria instância pois não estava conseguindo verificar ou configurar qualquer método de IDistribuitedCache

            var cacheService = mockCacheService.Object;

            await cacheService.SetAsync(key, ipResponse);

            mockCacheService
                .Verify(x => x.SetAsync(key, ipResponse, null), Times.Once());
        }

        public CacheService CreateService() => new(_cache.Object);
    }
}
