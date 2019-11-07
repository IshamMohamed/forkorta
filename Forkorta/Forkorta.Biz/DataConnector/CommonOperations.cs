using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace Forkorta.Biz.DataConnector
{
    public static class CommonOperations
    {
        /// <summary>
        /// Connect to the Azure Storage
        /// </summary>
        /// <param name="storageConnectionString">Connection String for Azure Stroage</param>
        /// <returns></returns>
        private static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application."); ;
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
            }

            return storageAccount;
        }

        /// <summary>
        /// Connect to Azure Table.
        /// </summary>
        /// <param name="storageConnectionString">Connection String for Azure Stroage</param>
        /// <returns></returns>
        public static async Task<CloudTable> ConfigureTableAsync(string storageConnectionString)
        {
            string tableName = "datatable";

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}
