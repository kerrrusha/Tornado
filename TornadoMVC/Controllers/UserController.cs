using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TornadoMVC.Data;
using TornadoMVC.ViewModels;

namespace TornadoMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TornadoMVCContext _context;

        public UserController(ILogger<HomeController> logger, TornadoMVCContext context)
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

        public IActionResult ProfileInfo()
        {
            return View(buildViewModel());
        }
    }
}
