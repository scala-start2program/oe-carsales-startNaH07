using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarSales.Data;
using CarSales.Models;
using static System.Formats.Asn1.AsnWriter;

namespace CarSales.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly CarSales.Data.CarContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public CreateModel(CarSales.Data.CarContext context,
            IWebHostEnvironment webhostEnvironment)

        {
            _context = context;
            this.webhostEnvironment = webhostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand.OrderBy(b=>b.BrandName), "Id", "BrandName");
            ViewData["FuelId"] = new SelectList(_context.Fuel.OrderBy(f=>f.FuelName), "Id", "FuelName");
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }
        [BindProperty]
        public IFormFile PhotoUpload { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            _context.Car.Add(Car);
            _context.SaveChanges();

            return RedirectToPage("./Index");
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
