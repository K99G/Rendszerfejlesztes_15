using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MgrClient
{
    public class asd : PageModel
    {
        private readonly ILogger<asd> _logger;

        public asd(ILogger<asd> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}