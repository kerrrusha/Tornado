using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TornadoMVC.Data;
using TornadoMVC.Models;
using TornadoMVC.ViewModels;

namespace TornadoMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TornadoMVCContext _context;

        public AuthController(ILogger<HomeController> logger, TornadoMVCContext context)
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

        public IActionResult Signin()
        {
            return View(buildViewModel());
        }
        public IActionResult Signup()
        {
            return View(buildViewModel());
        }

        public JsonResult Register(string userJSON)
        {
            User newUser = JsonSerializer.Deserialize<User>(userJSON);
            return new JsonResult(Ok("endpoint hit: " + userJSON));
        }
    }
}
