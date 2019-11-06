using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forkorta.Biz.Commands
{
    public class ShortUrlAzureTableCommand : ICommand
    {
        private readonly SubjectUrlForAzureTable url;
        private readonly UrlAction urlAction;
        public ShortUrlAzureTableCommand(SubjectUrlForAzureTable url, UrlAction urlAction)
        {
            this.url = url;
            this.urlAction = urlAction;
        }

        public async Task<string> ExecuteAction()
        {
            if (urlAction == UrlAction.Short)
            {
                return await url.ShortenUrlAndSaveToAzureTable();
            }

            else
            {
                return await url.DeshortenUrlFromAzureTable();
            }
        }
    }
}
