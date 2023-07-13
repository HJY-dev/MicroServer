using MicroServer.Common.Configuration;
using MicroServer.Common.Helper;
using System.IO;

namespace MicroServer.Common.ServiceExtensions
{
    public class AppSecretConfig
    {
        private static string Audience_Secret = ConfigurationManager.Appsettings.GetSection("Audience:Secret").Value.ObjToString();
        private static string Audience_Secret_File = ConfigurationManager.Appsettings.GetSection("Audience:SecretFile").Value.ObjToString();

        public static string Audience_Secret_String => InitAudience_Secret();


        private static string InitAudience_Secret()
        {
            var securityString = DifDBConnOfSecurity(Audience_Secret_File);
            if (!string.IsNullOrEmpty(Audience_Secret_File) && !string.IsNullOrEmpty(securityString))
            {
                return securityString;
            }
            else
            {
                return Audience_Secret;
            }

        }

        private static string DifDBConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (System.Exception) { }
            }

            return "";
        }

    }
}
