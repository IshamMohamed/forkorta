using Forkorta.Biz.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Forkorta.Biz.DataConnector
{
    public class DataUtils
    {
        /// <summary>
        /// Insert or Merge an entity to the Azure Table Storage. Both Partition key and Row key must be provided.
        /// Partition Key is the Shorten URL and Row Key is the long/orignial URL
        /// </summary>
        /// <param name="table">Azure Table Name</param>
        /// <param name="entity">Entity object</param>
        /// <returns></returns>
        public static async Task<UrlEntity> InsertOrMergeEntityAsync(CloudTable table, UrlEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            try
            {
                // Encode long URL
                entity.RowKey = WebUtility.UrlEncode(entity.RowKey);

                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                UrlEntity shortenUrlEntity = result.Result as UrlEntity;

                return shortenUrlEntity;
            }
            catch (StorageException e)
            {
                throw new StorageException(e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Get the entity from Azure Table storage for a given partition key. Patition key must be provided
        /// </summary>
        /// <param name="table"></param>
        /// <param name="partitionKey"></param>
        /// <returns></returns>
        public static UrlEntity RetrieveEntityUsingPartitionKey(CloudTable table, string partitionKey)
        {
            try
            {
                // Table query
                TableQuery<UrlEntity> query = new TableQuery<UrlEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

                // Execute the operation
                var results = table.ExecuteQuery(query);

                if (results.Any())
                {
                    var result = results.FirstOrDefault();
                    result.RowKey = WebUtility.UrlDecode(result.RowKey);
                    return result;
                }

                else return null;
            }
            catch (StorageException e)
            {
                throw new StorageException(e.Message, e.InnerException);
            }
        }
    }
}
