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

        public async Task ExecuteAction()
        {
            if(urlAction == UrlAction.Short)
            {
                await url.ShortenUrl();
            }

            if(urlAction == UrlAction.Deshort)
            {
                await url.DeshortenUrl();
            }
        }
    }
}
