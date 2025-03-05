using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Property_And_Supply_Management_API
{
    public class EmailSenderInformation
    {
        private readonly IConfiguration _config;

        public EmailSenderInformation(IConfiguration configuration)
        {
            _config = configuration;

        }

        public int PORT =>  _config.GetValue<int>("EMAIL_CONFIGURATION:PORT");
        public string EMAIL => _config.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
        public string PASSWORD => _config.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
        public string HOST => _config.GetValue<string>("EMAIL_CONFIGURATION:HOST");


    }
}
