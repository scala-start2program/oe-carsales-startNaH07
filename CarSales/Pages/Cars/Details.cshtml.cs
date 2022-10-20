using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarSales.Data;
using CarSales.Models;
using static System.Formats.Asn1.AsnWriter;

namespace CarSales.Pages.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public DetailsModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        public Car Car { get; set; }
        public int? PreviousId { get; set; }
        public int? NextId { get; set; }
        private IList<Car> Cars { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = _context.Car                
                .Include(b => b.Brand)
                .Include(f => f.Fuel)
                .FirstOrDefault(m => m.Id == id);

            Cars = _context.Car
            .Include(b => b.Brand)
            .Include(f => f.Fuel)
            .OrderBy(s => s.Brand.BrandName)
            .ThenBy(s => s.ModelName).ToList();

            PreviousId = null;
            NextId = null;

            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id == id)
                {
                    if (i > 0)
                        PreviousId = Cars[i - 1].Id;
                    if (i < Cars.Count - 1)
                        NextId = Cars[i + 1].Id;
                    break;
                }
            }
            if (PreviousId == null) PreviousId = id;
            if (NextId == null) NextId = id;


            if (car == null)
            {
                return NotFound();
            }
            else 
            {
                Car = car;
            }
            return Page();
        }
    }
}
