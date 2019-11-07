using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Forkorta.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration cong;
        public IndexModel(IConfiguration congif)
        {
            cong = congif;
        }
        public void OnGet()
        {
            ViewData["M"] = cong["AzureTableConnectionString"];
        }
    }
}
