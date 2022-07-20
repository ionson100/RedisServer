using Microsoft.Extensions.Configuration;

namespace RedisTr
{
    public class MPgSecret2
    {
        static readonly MPgSecret2 _secret = new MPgSecret2();
        public static void Active(IConfiguration configuration)
        {
            // _secret.RabName=configuration.GetValue<string>("RabName");
            // _secret.RabPwd = configuration.GetValue<string>("RabPwd");
            // _secret.PgName = configuration.GetValue<string>("PgName");
            // _secret.PgPwd = configuration.GetValue<string>("PgPwd");
            // _secret.Token = configuration.GetValue<string>("Token");
        }

        public static MPgSecret2 Secret => _secret;

        public string RabName { get; private set; } = "ion100";
        public string RabPwd { get; private set; } = "312312";
        public string PgName { get; private set; } = "postgres";
        public string PgPwd { get; private set; } = "ion100312873";
        public string Token { get; private set; } = "f92780be-b1f5-4740-addf-cc6f95888b33";
    }
}
