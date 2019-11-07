using System.Threading.Tasks;

namespace Forkorta.Biz.Commands
{
    /// <summary>
    /// Command to short URL and save in Azure Table
    /// </summary>
    public class ShortUrlAzureTableCommand : ICommand
    {
        private readonly SubjectUrlForAzureTable url;
        private readonly UrlAction urlAction;
        public ShortUrlAzureTableCommand(SubjectUrlForAzureTable url, UrlAction urlAction)
        {
            this.url = url;
            this.urlAction = urlAction;
        }

        /// <summary>
        /// Do the action to short or deshort URL
        /// </summary>
        /// <returns>Shorten or Deshorten URL</returns>
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
