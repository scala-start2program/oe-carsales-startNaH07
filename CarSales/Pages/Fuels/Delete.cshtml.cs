using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarSales.Data;
using CarSales.Models;

namespace CarSales.Pages.Fuels
{
    public class DeleteModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public DeleteModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Fuel Fuel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel.FirstOrDefaultAsync(m => m.Id == id);

            if (fuel == null)
            {
                return NotFound();
            }
            else 
            {
                Fuel = fuel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }
            var fuel = await _context.Fuel.FindAsync(id);

            if (fuel != null)
            {
                Fuel = fuel;
                _context.Fuel.Remove(Fuel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
