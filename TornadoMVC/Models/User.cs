using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace TornadoMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Пошта")]
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? last_name { get; set; }
        public string? city { get; set; }
        [DisplayName("№ відділення Нової Пошти")]
        public string? department { get; set; }
    }
}
