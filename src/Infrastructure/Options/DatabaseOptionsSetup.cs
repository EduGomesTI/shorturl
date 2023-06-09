using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        #region Properties

        private readonly IConfiguration _configuration;
        private const string ConfigurationSectionName = "DatabaseOptions";

        #endregion Properties

        #region Constructors

        public DatabaseOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion Constructors

        #region Methods

        public void Configure(DatabaseOptions options)
        {
            var connectionString = _configuration.GetConnectionString("Database");

            options.ConnectionString = connectionString!;

            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }

        #endregion Methods
    }
}