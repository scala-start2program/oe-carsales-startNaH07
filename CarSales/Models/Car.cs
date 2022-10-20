using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarSales.Models
{
    public class Car
    {
        public int Id { get; set; }

        [ForeignKey("Brand")]
        [Display(Name = "Merk")]
        public int BrandId { get; set; }

        [Display(Name = "Merk")]
        public Brand Brand { get; set; }

        [ForeignKey("Fuel")]
        [Display(Name = "Brandstof")]
        public int FuelId { get; set; }
        [Display(Name = "Brandstof")]
        public Fuel Fuel { get; set; }

        [Display(Name = "Modelnaam")]
        [Required(ErrorMessage = "Geef een model(naam) op !")]
        [StringLength(30, ErrorMessage = "Modelnaam maximaal 30 letters")]
        public string ModelName { get; set; }

        [Display(Name = "Kleur")]
        public string Color { get; set; }

        [Display(Name = "Vermogen")]
        [Required(ErrorMessage = "Voer een waarde in tussen 10 en 20000 !")]
        [Range(10, 20000, ErrorMessage = "Kies een waarde tussen 10 en 20.000")]
        [DisplayFormat(DataFormatString = "{0:#,##0} PK")]
        public int Performance { get; set; }

        [Display(Name = "Kilometerstand")]
        [Required(ErrorMessage = "Voer een waarde in tussen 0 en 1.000.000 !")]
        [Range(0, 1000000, ErrorMessage = "Kies een waarde tussen 0 en 1.000.000")]
        [DisplayFormat(DataFormatString = "{0:#,##0} km")]
        public int Mileage { get; set; }

        [Display(Name = "Bouwjaar")]
        [Required(ErrorMessage = "Voer een waarde in tussen 1990 en 2022 !")]
        [Range(1900, 2022, ErrorMessage = "Kies een waarde tussen 1990 en 2022")]
        public int Year { get; set; }

        [Display(Name = "Prijs")]
        [Required(ErrorMessage = "Voer een waarde in tussen 1 en 50000")]
        [Range(1, 50000, ErrorMessage = "Kies een waarde tussen 1 en 50000")]
        [Column(TypeName = "decimal(8, 2)")]
        [DisplayFormat(DataFormatString = "€{0:#,##0.00}")]
        public decimal SalesPrice { get; set; }

        public string ImagePath { get; set; }
    }
}
