using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public static class Utils
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
    }
}
