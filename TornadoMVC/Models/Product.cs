using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace TornadoMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Назва")]
        public string? Name { get; set; }
        public int? Category_Id { get; set; }
        public decimal cost { get; set; }
        public decimal? old_cost { get; set; }
        public string? image_url { get; set; }
        public int? remains { get; set; }
        [DisplayName("Опис")]
        public string? description { get; set; }
        public DateTime creating_date { get; set; }
        public int? code { get; set; }
    }
}
