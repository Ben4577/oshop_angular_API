
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Objects;
using Domain.Objects.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DataAccess.DocumentDb
{
    public class DocumentDbRepository<T> where T : ModelBase
    {

        private readonly DocumentClient _client;
        private readonly string _databaseName;
        private readonly string _collectionName;
        private RequestOptions _requestOptions;
        private PartitionKey _partitionKey;

        public DocumentDbRepository(DocumentClient client, string databaseName, string collectionName)
        {
            _client = client;
            _databaseName = databaseName;
            _collectionName = collectionName;
            _partitionKey = new PartitionKey(typeof(T).Name);
            _requestOptions = new RequestOptions();
            _requestOptions.PartitionKey = _partitionKey;
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
                    UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName), queryOptions)
                .Where(model => model.TypeName == typename);
            return query;

        }

        protected internal async Task<IEnumerable<T>> QueryAsync(string token, int maxItemCount = 50)
        {
            var typename = typeof(T).Name;
            var queryOptions = new FeedOptions
            {
                MaxItemCount = maxItemCount,
                RequestContinuation = token
            };

            var query =
                _client.CreateDocumentQuery<T>(CollectionUri(), queryOptions)
                    .Where(model => model.TypeName == typename)
                    .AsDocumentQuery();

            var results = await query.ExecuteNextAsync<T>();

            return results;
        }

        protected internal async Task<IEnumerable<T>> QueryAsync()
        {
            var typename = typeof(T).Name;
            var query =
                _client.CreateDocumentQuery<T>(CollectionUri())
                    .Where(model => model.TypeName == typename)
                    .AsDocumentQuery();

            var results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        protected internal IQueryable<T> CreateQuery(string token, int pageSize = 50)
        {
            var typename = typeof(T).Name;
            var queryOptions = new FeedOptions
            {
                MaxItemCount = pageSize
            };

            if (token.IsNotNullOrWhiteSpace()) queryOptions.RequestContinuation = token;

            var query = _client.CreateDocumentQuery<T>(CollectionUri(), queryOptions)
                .Where(model => model.TypeName == typename);

            return query;
        }

        protected internal async Task<Dictionary<string, IEnumerable<T>>> ExecutePagedQuery(IDocumentQuery<T> query)
        {
            var results = new Dictionary<string, IEnumerable<T>>();
            try
            {
                var result = await query.ExecuteNextAsync<T>();
                var token = result.ResponseContinuation;
                var items = result.ToList();
                results.Add(token, items.AsEnumerable());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return results;
        }

        protected internal async Task<string> Count(string condition = "")
        {
            var typename = typeof(T).Name;
            var counts = _client.CreateDocumentQuery(
                UriFactory.CreateDocumentCollectionUri(_databaseName, _collectionName),
                $"SELECT COUNT(c.id) AS Count FROM c WHERE c.TypeName=\"{typename}\" {(condition.IsNotNullOrWhiteSpace() ? condition : string.Empty)}").ToList();
            if (!counts.Any()) return await Task.FromResult("0");

            var first = counts.First();
            return await Task.FromResult(first.Count.ToString());
        }

        protected internal async Task<IEnumerable<T>> QueryAsync(string token, Expression<Func<T, bool>> predicate, int maxItemCount = 50)
        {
            var typename = typeof(T).Name;
            var queryOptions = new FeedOptions
            {
                MaxItemCount = maxItemCount,
                RequestContinuation = token
            };

            var query =
                _client.CreateDocumentQuery<T>(CollectionUri(), queryOptions)
                    .Where(model => model.TypeName == typename)
                    .Where(predicate)
                    .OrderByDescending(predicate)
                    .AsDocumentQuery();

            var results = await query.ExecuteNextAsync<T>();

            return results;
        }

        protected internal async Task<T> Save(T itemToSave)
        {
            try
            {
               // itemToSave.LastUpdatedDate = DateTime.Now;
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
