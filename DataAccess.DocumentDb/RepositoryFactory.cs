using System;
using System.Net;
using System.Threading.Tasks;
using DataAccess.RepositoryFactory;
using DataAccess.RepositoryFarm;
using Domain.Objects.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DataAccess.DocumentDb
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DocumentClient _client;
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly PartitionKey _partitionKey;

        public RepositoryFactory(AppRuntimeSettingsProvider settings)
        {
            var connectionPolicy = new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            };

            var _settings = settings;
            var endpointUrl = _settings.DocumentDbEndpointUrl;
            var primaryKey = _settings.DocumentDbPrimaryKey;
            _databaseName = _settings.DocumentDbDatabaseName;
            _collectionName = _settings.DocumentDbCollectionName;


            _client = new DocumentClient(new Uri(endpointUrl), primaryKey, connectionPolicy);

            OshopRepository = new OshopRepository(new DocumentDbRepository<Product>(_client, _databaseName, _collectionName));
        }

        public IOshopRepository OshopRepository { get; }

        public async Task CreateDatabaseIfNotExists()
        {
            try
            {
                await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_databaseName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDatabaseAsync(new Database { Id = _databaseName });
                    await CreateDocumentCollectionIfNotExists();
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                await _client.OpenAsync();
            }
        }

        private async Task CreateDocumentCollectionIfNotExists()
        {
            try
            {
                await _client.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    var collectionInfo = new DocumentCollection
                    {
                        Id = _collectionName,
                        IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 })
                    };

                    await _client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(_databaseName),
                        collectionInfo,
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

