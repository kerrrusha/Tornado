using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TornadoMVC.Models;
using TornadoMVC.ViewModels;
using System.Text.Json;
using TornadoMVC.Data;

namespace TornadoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TornadoMVCContext _context;
        private readonly SearchManager searchManager;

        // GET: Categories, Product
        public ActionResult Index()
        {
            if (_context.Category == null)
                return Problem("Entity set 'TornadoMVCContext.Category'  is null.");
            if (_context.Product == null)
                return Problem("Entity set 'TornadoMVCContext.Product'  is null.");

            HomeViewModel viewModel = new HomeViewModel();
            viewModel.Categories = _context.Category.ToList();
            viewModel.Products = _context.Product.ToList();

            return View(viewModel);
        }

        public HomeController(ILogger<HomeController> logger, TornadoMVCContext context)
        {
            _logger = logger;
            _context = context;
            searchManager = new SearchManager(_context);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search(string? query)
        {
            if (_context.Category == null)
                return Problem("Entity set 'TornadoMVCContext.Category'  is null.");
            if (_context.Product == null)
                return Problem("Entity set 'TornadoMVCContext.Product'  is null.");

            SearchViewModel viewModel = new SearchViewModel();
            viewModel.Categories = _context.Category.ToList();
            viewModel.Products = _context.Product.ToList();
            viewModel.SearchQuery = query;
            viewModel.SearchResults = (List<Dataset>?)searchManager.Search(query);

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult SearchJSON(string query)
        {
            List<KeyValuePair<string, List<KeyValuePair<int, string>>>> result;
            try
            {
                var matches = (List<Dataset>)searchManager.Search(query);

                if (matches.Count <= 0)
                {
                    return new JsonResult(Ok(""));
                }

                result = new List<KeyValuePair<string, List<KeyValuePair<int, string>>>>();
                foreach (var match in matches)
                {
                    var items = new List<KeyValuePair<int, string>>();
                    foreach (var item in match.data)
                    {
                        items.Add(new KeyValuePair<int, string>(item.id, item.name));
                    }
                    result.Add(new KeyValuePair<string, List<KeyValuePair<int, string>>>(match.name, items));
                }
            } catch (Exception e)
            {
                return new JsonResult(Ok(e.Message));
            }

            return new JsonResult(Ok(result));
        }
    }
}