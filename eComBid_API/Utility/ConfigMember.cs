using System.Configuration;

namespace eComBid_API.Utility
{
    public static class ConfigMember
    {
        internal static string baseURL = ConfigurationManager.AppSettings["BaseURL"];
        internal static string ImageURL = ConfigurationManager.AppSettings["ImageURL"];
        internal static string ImageFolder = ConfigurationManager.AppSettings["ImagesFolder"];
    }
}
