using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace DataAccess.DocumentDb
{
    public abstract class AppRuntimeSettingsProvider
    {
        public string DocumentDbEndpointUrl { get; set; }
        public string DocumentDbPrimaryKey { get; set; }
        public string DocumentDbDatabaseName { get; set; }
        public string DocumentDbCollectionName { get; set; }

        public void Initialise(IConfiguration configuration)
        {
            InitialiseSettings(configuration);
            Verify();
        }

        protected abstract void InitialiseSettings(IConfiguration configuration);

        public void Verify()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Public)
                .Select(property => property.GetValue(this) as string).ToList();
            if (properties.Any(string.IsNullOrWhiteSpace))
            {
                throw new ApplicationException(
                    $"AppRuntimeSettingsProvider --> {this.GetType().Name}: Failed to verify runtime Settings, Some of the settings are missing, ");
            }
        }
    }
}