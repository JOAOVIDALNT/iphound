using Bogus;

namespace iphound.Tests.Builders
{
    public class IpRequestBuilder
    {
        public static string Build()
        {
            var faker = new Faker();

            return faker.Internet.Ip();
        }
    }
}
