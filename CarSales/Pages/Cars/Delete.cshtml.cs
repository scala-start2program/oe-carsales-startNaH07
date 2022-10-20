using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarSales.Data;
using CarSales.Models;

namespace CarSales.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public DeleteModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; }

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

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }
            var car =  _context.Car.Find(id);

            if (car != null)
            {
                Car = car;
                _context.Car.Remove(Car);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
