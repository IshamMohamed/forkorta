using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forkorta.Biz.Commands
{
    public class ShortUrlCommand : ICommand
    {
        private readonly SubjectUrl url;
        private readonly UrlAction urlAction;
        public ShortUrlCommand(SubjectUrl url, UrlAction urlAction)
        {
            this.url = url;
            this.urlAction = urlAction;
        }

        public async Task<string> ExecuteAction()
        {
            if (urlAction == UrlAction.Short)
            {
                return await url.ShortenUrl();
            }

            else
            {
                return await url.DeshortenUrl();
            }
        }
    }
}
