
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Objects.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace DataAccess.DocumentDb
{
    public class DocumentDbRepository<T> where T : ModelBase
    {

        private readonly DocumentClient _client;
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly RequestOptions _requestOptions;
        private readonly PartitionKey _partitionKey;


        public DocumentDbRepository(DocumentClient client, string databaseName, string collectionName)
        {
            _client = client;
            _databaseName = databaseName;
            _collectionName = collectionName;
            //_requestOptions.PartitionKey = _partitionKey;
        }


        protected internal IQueryable<T> Query(int maxItemCount = -1)
        {
            var typename = typeof(T).Name;

            var queryOptions = new FeedOptions
            {
                EnableCrossPartitionQuery = true,
                MaxItemCount = maxItemCount
            };

            var query = _client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), queryOptions);
            return query;
        }


        protected internal async Task<T> Save(T itemToSave)
        {
            try
            {
                itemToSave.LastUpdatedDate = DateTime.Now;
                await _client.ReadDocumentAsync(DocumentUri(itemToSave.Id), _requestOptions);
                await _client.ReplaceDocumentAsync(DocumentUri(itemToSave.Id), itemToSave, _requestOptions);

            }
            catch (DocumentClientException de)
            {

                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentAsync(CollectionUri(), itemToSave, _requestOptions);

                }
                else
                {
                    throw;
                }
            }
            return itemToSave;

        }

        protected internal async Task Delete(T itemToDelete)
        {
            try
            {
                await _client.DeleteDocumentAsync(DocumentUri(itemToDelete.Id), _requestOptions);
            }
            catch (DocumentClientException documentClientException)
            {
                if (documentClientException.StatusCode == HttpStatusCode.NotFound)
                {
                    return;
                }
                throw;
            }

        }


        private Uri CollectionUri()
        {
            return UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName);
        }


        private Uri DocumentUri(string id)
        {
            return UriFactory.CreateDocumentUri(_databaseName, _collectionName, id);
        }




    }
}
