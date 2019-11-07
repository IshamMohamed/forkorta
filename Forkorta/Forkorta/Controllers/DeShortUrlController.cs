using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Forkorta.Biz;
using Forkorta.Biz.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Forkorta.Controllers
{
    [Route("{shortUrl}")]
    [ApiController]
    public class DeShortUrlController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public DeShortUrlController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration["AzureTableConnectionString"];
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAndRedirectToLongUrl(string shortUrl)
        {
            SubjectUrlForAzureTable url = new SubjectUrlForAzureTable(shortUrl, connectionString);

            ICommand doDeshort = new ShortUrlAzureTableCommand(url, UrlAction.Deshort);
            string deshortenUrl = await doDeshort.ExecuteAction();


            return Redirect(deshortenUrl);
        }
    }
}