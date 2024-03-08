namespace MepWeb.Service
{
    public class GetConfig
    {
        private readonly IConfiguration _configuration;

        public GetConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ScwPlatformURL
        {
            get
            {
                return _configuration[nameof(ScwPlatformURL)];
            }
        }

        public string TableGeneratorPath
        {
            get
            {
                return _configuration[nameof(TableGeneratorPath)];
            }
        }
    }
}
