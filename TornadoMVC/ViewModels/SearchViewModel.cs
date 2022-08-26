using TornadoMVC.Models;

namespace TornadoMVC.ViewModels
{
    public class SearchViewModel : HomeViewModel
    {
        public string? SearchQuery { get; set; }
        public List<Dataset>? SearchResults { get; set; }
    }
}
