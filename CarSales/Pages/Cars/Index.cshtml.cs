using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarSales.Data;
using CarSales.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSales.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;

        public IndexModel(CarSales.Data.CarContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; } = default!;
        public List<SelectListItem> AvailableBrands { get; set; }
        public List<SelectListItem> AvailableFuels { get; set; }
        public int? SelectedBrandId { get; set; }
        public int? SelectedFuelId { get; set; }

        public void OnGet()
        {
            if (_context.Car != null)
            {
                PopulateCollections(null,null);
            }
        }
        public void OnPost(int? brandFilter, int? fuelFilter)
        {
            SelectedBrandId = brandFilter;
            SelectedFuelId = fuelFilter;
            PopulateCollections(brandFilter, fuelFilter);

        }

        private void PopulateCollections(int? brandFilter, int? fuelFilter)
        {
            IQueryable<Car> query = _context.Car
            .Include(c => c.Brand)
            .Include(c => c.Fuel)
            .OrderBy(b => b.Brand.BrandName)
            .ThenBy(c => c.ModelName);
            if (brandFilter == null && fuelFilter == null)
            {
                Car = query.ToList();
            }
            else if (brandFilter != null && fuelFilter == null)
            {
                Car = (from x in query where (x.BrandId == brandFilter) select x).ToList();
            }
            else if (brandFilter == null && fuelFilter != null)
            {
                Car = (from x in query where (x.FuelId == fuelFilter) select x).ToList();
            }
            else
            {
                Car = (from x in query where (x.BrandId == brandFilter && x.FuelId == fuelFilter) select x).ToList();
            }


            AvailableBrands = _context.Brand.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.BrandName
                }).ToList();
            AvailableBrands = AvailableBrands.OrderBy(b => b.Text).ToList();
            AvailableFuels = _context.Fuel.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FuelName
                }).ToList();
            AvailableFuels = AvailableFuels.OrderBy(b => b.Text).ToList();

            AvailableBrands.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle merken ---"
            });
            AvailableFuels.Insert(0, new SelectListItem()
            {
                Value = null,
                Text = "--- Alle brandstoffen ---"
            });
        }

    }
}
