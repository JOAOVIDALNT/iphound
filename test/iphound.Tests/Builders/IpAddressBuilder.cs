using Bogus;
using iphound.API.Models.Entities;
using iphound.Tests.Utils;

namespace iphound.Tests.Builders
{
    public class IpAddressBuilder
    {
        public static List<IpAddress> Build()
        {
            List<IpAddress> ips = new List<IpAddress>();
            Faker faker = new Faker();
            for (int i = 1; i <= 10; i++)
            {
                ips.Add(new IpAddress
                {
                    Id = i,
                    Ip = faker.Internet.Ip(),
                    Country = new Country
                    {
                        Id = i,
                        Name = faker.Address.Country(),
                        TwoLetterCode = Util.RandomLetters(2),
                        ThreeLetterCode = Util.RandomLetters(3),
                    },
                    CountryId = i
                });
            }

            return ips;
        }
    }


}
