using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarSales.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Merk")]
        [Required(ErrorMessage = "Merknaam is vereist")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Minimaal 2, maximaal 30 letters")]
        public string BrandName { get; set; }
    }
}
