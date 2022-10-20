using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarSales.Data;
using CarSales.Models;
using Microsoft.AspNetCore.Hosting;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.AspNetCore.Hosting;

namespace CarSales.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public EditModel(CarSales.Data.CarContext context,
            IWebHostEnvironment webhostEnvironment)
        {
            _context = context;
            this.webhostEnvironment = webhostEnvironment;
        }

        [BindProperty]
        public Car Car { get; set; } = default!;
        [BindProperty]
        public IFormFile PhotoUpload { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car =  _context.Car.FirstOrDefault(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            Car = car;
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b=>b.BrandName), "Id", "BrandName");
            ViewData["FuelId"] = new SelectList(_context.Fuel.OrderBy(f=>f.FuelName), "Id", "FuelName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b => b.BrandName), "Id", "BrandName");
            ViewData["FuelId"] = new SelectList(_context.Fuel.OrderBy(f => f.FuelName), "Id", "FuelName");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (PhotoUpload != null)
            {
                if (Car.ImagePath != null)
                {
                    string filePath = Path.Combine(webhostEnvironment.WebRootPath, "images", Car.ImagePath);
                    System.IO.File.Delete(filePath);
                }
                Car.ImagePath = ProcessUploadedFile();
            }

            _context.Attach(Car).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarExists(int id)
        {
          return _context.Car.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (PhotoUpload != null)
            {
                string uploadFolder = Path.Combine(webhostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoUpload.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PhotoUpload.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
