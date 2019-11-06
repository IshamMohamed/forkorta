using Forkorta.Biz.DataConnector;
using Forkorta.Biz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Forkorta.Biz
{
    public sealed class SubjectUrlForAzureTable : SubjectUrl
    {
        public string ConnectionString { get; set; }

        public SubjectUrlForAzureTable(string url, string connectionString)
        {
            this.Url = url;
            this.ConnectionString = connectionString;
        }

        public async Task<string> ShortenUrlAndSaveToAzureTable()
        {
            // 8 character long short url is generated
            string shortUrl = SubjectUrlUtil.GetShortUrl(8);
            var dataTable = await CommonOperations.ConfigureTableAsync(this.ConnectionString);
            var ops = await DataUtils.InsertOrMergeEntityAsync(dataTable, new Entities.UrlEntity { PartitionKey = shortUrl, RowKey = this.Url });

            return ops.PartitionKey;
        }

        public async Task<string> DeshortenUrlFromAzureTable()
        {
            var dataTable = await CommonOperations.ConfigureTableAsync(this.ConnectionString);
            var ops = DataUtils.RetrieveEntityUsingPartitionKey(dataTable, this.Url);

            return ops.RowKey;
        }

        
    }
}
