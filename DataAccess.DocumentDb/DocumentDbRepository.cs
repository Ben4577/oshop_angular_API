
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Objects.Models;
using Microsoft.Azure.Documents.Client;

namespace DataAccess.DocumentDb
{
    public class DocumentDbRepository<T> where T : ModelBase
    {

        private readonly DocumentClient _client;
        private readonly string _databaseName;
        private readonly string _collectionName;


        public DocumentDbRepository(DocumentClient client, string databaseName, string collectionName)
        {
            _client = client;
            _databaseName = databaseName;
            _collectionName = collectionName;
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
               // .Where(model => model.TypeName == typename);

            return query;
        }




    }
}
