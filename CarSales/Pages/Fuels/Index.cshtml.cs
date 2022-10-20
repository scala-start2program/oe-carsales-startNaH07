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
    public class IndexModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public IndexModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        public IList<Fuel> Fuel { get;set; } = default!;

        public void OnGet()
        {
            if (_context.Fuel != null)
            {
                Fuel = _context.Fuel
                    .OrderBy(f=>f.FuelName)
                    .ToList();
            }
        }
    }
}
