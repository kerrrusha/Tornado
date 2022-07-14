using TornadoMVC.Models;

namespace TornadoMVC.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
    }
}
