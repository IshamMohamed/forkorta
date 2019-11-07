using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forkorta.Biz;
using Forkorta.Biz.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Forkorta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public ShortUrlController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration["AzureTableConnectionString"];
        }
        [HttpPost]
        public async Task<IActionResult> ShortUrl([FromBody] SubjectUrl subjectUrl)
        {
            SubjectUrlForAzureTable url = new SubjectUrlForAzureTable(subjectUrl.Url, connectionString);

            ICommand doShort = new ShortUrlAzureTableCommand(url, UrlAction.Short);
            string shortenUrl = await doShort.ExecuteAction();

            return Ok(shortenUrl);
        }
    }
}