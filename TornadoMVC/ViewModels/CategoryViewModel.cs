using TornadoMVC.Models;

namespace TornadoMVC.ViewModels
{
    public class CategoryViewModel : HomeViewModel
    {
        public Category? Category { get; set; }
        public List<Product>? CategoryProducts { get; set; }
    }
}
