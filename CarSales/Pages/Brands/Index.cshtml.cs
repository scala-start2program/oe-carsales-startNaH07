using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarSales.Data;
using CarSales.Models;

namespace CarSales.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public IndexModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get;set; } = default!;

        public void OnGet()
        {
            if (_context.Brand != null)
            {
                Brand = _context.Brand
                    .OrderBy(b=>b.BrandName)
                    .ToList();
            }
        }
    }
}
