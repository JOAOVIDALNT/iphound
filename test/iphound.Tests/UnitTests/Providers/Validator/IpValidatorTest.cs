using Bogus;
using iphound.API.Exceptions;
using iphound.API.Providers.Validator;
using iphound.Tests.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iphound.Tests.UnitTests.Providers.Validator
{
    public class IpValidatorTest
    {
        [Fact]
        public void Should_Succeed()
        {
            var ip = IpRequestBuilder.Build();

            ip.ValidateIp();
        }

        [Fact]
        public void Should_Throw_Invalid_Ip()
        {
            Faker faker = new Faker();
            var ip = faker.Person.FirstName;

            Assert.Throws<InvalidIpException>(() => ip.ValidateIp());
        }
    }
}
