using Microsoft.AspNetCore.Mvc;
using TornadoMVC.Data;
using TornadoMVC.ViewModels;

namespace TornadoMVC.Controllers
{
    public class InfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TornadoMVCContext _context;

        public InfoController(ILogger<HomeController> logger, TornadoMVCContext context)
        {
            _logger = logger;
            _context = context;
        }

        private HomeViewModel buildViewModel()
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.Categories = _context.Category.ToList();
            viewModel.Products = _context.Product.ToList();
            return viewModel;
        }

        public IActionResult About()
        {
            return View(buildViewModel());
        }
        public IActionResult Contacts()
        {
            return View(buildViewModel());
        }
        public IActionResult Delivery()
        {
            return View(buildViewModel());
        }
        public IActionResult Warranty()
        {
            return View(buildViewModel());
        }
    }
}
