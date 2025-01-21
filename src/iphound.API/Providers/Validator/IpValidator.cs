using iphound.API.Exceptions;
using System.Net;

namespace iphound.API.Providers.Validator
{
    public static class IpValidator
    {
        public static void ValidateIp(this string ip)
        {
            if (!ip.IsValidIp())
                throw new InvalidIpException();
        }

        private static bool IsValidIp(this string input)
        {
            if (IPAddress.TryParse(input, out _))
            {
                return true;
            }
            return false;
        }
    }
}
