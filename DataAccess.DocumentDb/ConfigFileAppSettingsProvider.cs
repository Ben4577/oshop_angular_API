using Microsoft.Extensions.Configuration;

namespace DataAccess.DocumentDb
{
    public sealed class ConfigFileAppSettingsProvider : AppRuntimeSettingsProvider
    {
        protected override void InitialiseSettings(IConfiguration configuration)
        {
            DocumentDbEndpointUrl = configuration.GetSection("DocumentDb:DocumentDbEndpointUrl").Value;
            DocumentDbPrimaryKey = configuration.GetSection("DocumentDb:DocumentDbPrimaryKey").Value;
            DocumentDbDatabaseName = configuration.GetSection("DocumentDb:DocumentDbDatabaseName").Value;
            DocumentDbCollectionName = configuration.GetSection("DocumentDb:DocumentDbCollectionName").Value;
        }
    }
}