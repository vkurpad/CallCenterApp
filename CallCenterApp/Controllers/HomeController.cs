using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CallCenterApp.Models;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Search.Models;

namespace CallCenterApp.Controllers
{
    public class HomeController : Controller
    {
        private static SearchServiceClient _serviceClient;
        private static ISearchIndexClient _indexClient;
        private IConfiguration configuration;

        public HomeController(IConfiguration iconfig)
        {
            configuration = iconfig;
            
        }
        private void InitSearch()
        {

            string searchServiceName = configuration.GetValue<string>("prefixName") + "search"; 
            string queryApiKey = configuration.GetValue<string>("searchMgmtKey"); 

            // Create a service and index client.
            _serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(queryApiKey));
            _indexClient = _serviceClient.Indexes.GetClient("callcenter-index");
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(SearchData model)
        {
            try
            {
                // Ensure the search string is valid.
                if (model.searchText == null)
                {
                    model.searchText = "";
                }

                // Make the Azure Search call.
                await RunQueryAsync(model);
            }

            catch
            {
                return View("Error", new ErrorViewModel { RequestId = "1" });
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        private async Task<ActionResult> RunQueryAsync(SearchData model)
        {
            InitSearch();

            var parameters = new SearchParameters
            {
                // Enter Hotel property names into this list so only these values will be returned.
                // If Select is empty, all values will be returned, which can be inefficient.
                Select = new[] { "audio_file", "summary", "conversation" }
            };
            try
            {
                // For efficiency, the search call should be asynchronous, so use SearchAsync rather than Search.
                model.resultList = await _indexClient.Documents.SearchAsync<CallCenterLog>(model.searchText, parameters);
            }
            catch(Exception  ex)
            {
                throw ex;
            }

            // Display the results.
            return View("Index", model);
        }
    }
}
