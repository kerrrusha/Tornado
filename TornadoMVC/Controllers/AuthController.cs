using Microsoft.AspNetCore.Mvc;
using System.Net;
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

        class CustomResponse
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
        }
        public JsonResult Authorize(string userCredentialsJson)
        {
            UserCredentials? credentials = JsonSerializer.Deserialize<UserCredentials>(userCredentialsJson);
            if (_context.User.Any(o => o.email == credentials.email))
            {
                var user = _context.User.FirstOrDefault(o => o.password == credentials.password);

                if (user != null)
                {
                    SetAuthCookie(user.Id.ToString());
                    return Json(new CustomResponse
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Successfully authorized"
                    });
                }
                return Json(new CustomResponse
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Incorrect password"
                });
            }
            return Json(new CustomResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "There are no such user with email: " + credentials.email
            });
        }
        private void SetAuthCookie(string uid)
        {
            Response.Cookies.Append("uid", uid);
        }
        private void RemoveAuthCookie()
        {
            Response.Cookies.Delete("uid");
        }
        public JsonResult Register(string userJSON)
        {
            User newUser = JsonSerializer.Deserialize<User>(userJSON);
            if (_context.User.Any(o => o.email == newUser.email))
            {
                JsonResult res = Json(new CustomResponse
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Such user already exists."
                });
                return res;
            }
            _context.Add<User>(newUser);
            _context.SaveChanges();

            SetAuthCookie(newUser.Id.ToString());
            return Json(new CustomResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Register successfull."
            });
        }
        public JsonResult Signout()
        {
            if (!Request.Cookies.ContainsKey("uid"))
            {
                return Json(new CustomResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Can't sign out user - already not authorised"
                });
            }

            RemoveAuthCookie();

            return Json(new CustomResponse
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Sign out user successfully"
            });
        }
    }
}
