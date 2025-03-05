using PAS_API.Interface;

namespace PAS_API
{
    public class EmailSenderInformation
    {
        private readonly IConfiguration _configuration;

        public EmailSenderInformation(IConfiguration configuration)
        {
            _configuration = configuration;
            email = _configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            password = _configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            host = _configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            port = _configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");
        }

        public string email { get; set; }
        public int port { get; set; }
        public string host { get; set; }
        public string password { get; set; } 
    }
}
