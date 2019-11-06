using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forkorta.Biz.Entities
{
    public class UrlEntity : TableEntity
    {
        public UrlEntity()
        {

        }

        public UrlEntity(string longUrl, string shortUrl)
        {
            this.PartitionKey = shortUrl;
            this.RowKey = longUrl;
        }
    }
}
