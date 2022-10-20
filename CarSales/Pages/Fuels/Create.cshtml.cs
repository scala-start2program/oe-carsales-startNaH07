using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarSales.Data;
using CarSales.Models;

namespace CarSales.Pages.Fuels
{
    public class CreateModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public CreateModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Fuel Fuel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Fuel.Add(Fuel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
