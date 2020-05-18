using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CypherBot.Web.Models;
using CypherBot.Core.DataAccess.Repos;

namespace CypherBot.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CypherContext _cypherContext;

        public HomeController(ILogger<HomeController> logger, CypherContext cypherContext)
        {
            _logger = logger;
            _cypherContext = cypherContext;
        }

        public IActionResult Index()
        {
            return View(_cypherContext.Cyphers.Take(10).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
