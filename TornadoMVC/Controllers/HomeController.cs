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

        public List<Dataset> getDataSources()
        {
            var sources = new List<Dataset>();

            var data = new List<Item>();
            _context.Category.ToList().ForEach(delegate (Category item)
            {
                data.Add(new Item(item.Id, item.Name));
            });
            sources.Add(new Dataset("Категорії", data.ToList()));

            data.Clear();
            _context.Product.ToList().ForEach(delegate (Product item)
            {
                data.Add(new Item(item.Id, item.Name));
            });
            sources.Add(new Dataset("Товари", data.ToList()));

            data.Clear();
            _context.Product.ToList().ForEach(delegate (Product item)
            {
                data.Add(new Item(item.Id, item.description));
            });
            sources.Add(new Dataset("Описи товарів", data.ToList()));

            return sources;
        }

        [HttpGet]
        public JsonResult Search(string query)
        {
            List<KeyValuePair<string, List<KeyValuePair<int, string>>>> result;
            try
            {
                var dataSources = getDataSources();
                var searchManeger = new SearchManager(dataSources);

                var matches = (List<Dataset>)searchManeger.search(query);

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