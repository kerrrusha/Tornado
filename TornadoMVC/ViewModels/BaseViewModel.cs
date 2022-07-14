using TornadoMVC.Models;

namespace TornadoMVC.ViewModels
{
    public class BaseViewModel
    { 
        public IEnumerable<Category>? Categories { get; set; }
    }
}
