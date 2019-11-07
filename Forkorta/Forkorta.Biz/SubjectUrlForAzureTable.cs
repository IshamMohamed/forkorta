using Forkorta.Biz.DataConnector;
using Forkorta.Biz.Entities;
using System.Threading.Tasks;

namespace Forkorta.Biz
{
    /// <summary>
    /// Derived class from the SubjectUrl for Azure Table connectivity
    /// </summary>
    public sealed class SubjectUrlForAzureTable : SubjectUrl
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">Url to short or deshort</param>
        /// <param name="connectionString">Azure Table connection string</param>
        public SubjectUrlForAzureTable(string url, string connectionString)
        {
            this.Url = url;
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// To short a given URL and save it to the Azure Table
        /// </summary>
        /// <returns>Shorten URL</returns>
        public async Task<string> ShortenUrlAndSaveToAzureTable()
        {
            // 8 character long short url is generated
            string shortUrl = SubjectUrlUtil.GetShortUrl(8);
            var dataTable = await CommonOperations.ConfigureTableAsync(this.ConnectionString);
            var ops = await DataUtils.InsertOrMergeEntityAsync(dataTable, new UrlEntity { PartitionKey = shortUrl, RowKey = this.Url });

            return ops.PartitionKey;
        }

        /// <summary>
        /// Read the Azure Table and get the short URL for a given long URL
        /// </summary>
        /// <returns>Deshorten Long URL</returns>
        public async Task<string> DeshortenUrlFromAzureTable()
        {
            var dataTable = await CommonOperations.ConfigureTableAsync(this.ConnectionString);
            var ops = DataUtils.RetrieveEntityUsingPartitionKey(dataTable, this.Url);

            return ops.RowKey;
        }

        
    }
}
