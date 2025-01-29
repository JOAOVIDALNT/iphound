using Bogus;

namespace iphound.Tests.Utils
{
    public class Util
    {
        public static string RandomLetters(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }

        public static string DiferentCountry(string country)
        {
            Faker faker = new();

            while (true)
            {
                var newCountry = faker.Address.Country();

                if (!newCountry.Equals(country))
                {
                    return newCountry;
                }
            }
        }
    }
}
