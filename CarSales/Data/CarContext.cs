using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarSales.Models;

namespace CarSales.Data
{
    public class CarContext : DbContext
    {
        public CarContext (DbContextOptions<CarContext> options)
            : base(options)
        {
        }

        public DbSet<CarSales.Models.Brand> Brand { get; set; } = default!;

        public DbSet<CarSales.Models.Fuel> Fuel { get; set; }

        public DbSet<CarSales.Models.Car> Car { get; set; }
    }
}
