using Bogus;
using iphound.API.Models.HttpModels.Responses;

namespace iphound.Tests.Builders
{
    public class IpInfoResponseBuilder
    {
        public static IpInfoResponse Build(string ip)
        {
            Faker faker = new Faker();
            return new IpInfoResponse
            {
                IpAddress = ip,
                CountryName = faker.Address.Country(),
                TwoLetterCode = "XX",
                ThreeLetterCode = "XXX",
                Success = true
            };
        }
    }
}
