using Forkorta.Biz.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Forkorta.Biz.DataConnector
{
    public class DataUtils
    {
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
