using CarSales.Data;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace CarSales.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CarContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CarContext>>()))
            {
                if (!context.Fuel.Any() && !context.Brand.Any() && !context.Car.Any())
                {
                    Fuel benzine = new Fuel { FuelName = "Benzine" };
                    Fuel diesel = new Fuel { FuelName = "Diesel" };
                    Fuel lpg = new Fuel { FuelName = "LPG" };
                    Fuel waterstof = new Fuel { FuelName = "Waterstof" };
                    Fuel elektrisch = new Fuel { FuelName = "Elektrisch" };
                    context.Fuel.AddRange(
                        benzine, diesel, lpg, waterstof, elektrisch
                    );
                    context.SaveChanges();

                    Brand bmw = new Brand { BrandName = "BMW" };
                    Brand mercedes = new Brand { BrandName = "Mercedes" };
                    Brand volkswagen = new Brand { BrandName = "Volkswagen" };
                    Brand ford = new Brand { BrandName = "Ford" };
                    Brand renault = new Brand { BrandName = "Renault" };
                    Brand peugeot = new Brand { BrandName = "Peugeot" };
                    Brand nissan = new Brand { BrandName = "Nissan" };
                    Brand toyota = new Brand { BrandName = "Toyota" };
                    context.Brand.AddRange(
                        bmw, mercedes, volkswagen, ford, renault, peugeot, nissan, toyota
                    );
                    context.SaveChanges();
                    context.Car.AddRange(
                        new Car { Brand = bmw, Fuel = benzine, ModelName = "1-serie", Color="Wit", Performance = 175, Mileage=23000,Year=2019, SalesPrice = 19000m,  ImagePath = "bmw.jpg" },
                        new Car { Brand = ford, Fuel = diesel, ModelName = "Fiesta", Color = "Rood", Performance = 75, Mileage =125000, Year =1998, SalesPrice = 500m, ImagePath = "fiesta.jpg" },
                        new Car { Brand = mercedes, Fuel = diesel, ModelName = "C-klasse Break", Color = "Zwart", Performance = 125, Mileage =39000, Year =2021, SalesPrice = 25000m, ImagePath = "cklasse.jpg" },
                        new Car { Brand = mercedes, Fuel = elektrisch, ModelName = "A-klasse Hatchback", Color = "Zilver", Performance = 225, Mileage =9000, Year =2022, SalesPrice = 32000m, ImagePath = "aklasse.jpg" },
                        new Car { Brand = renault, Fuel = benzine, ModelName = "Clio", Color = "Lichtblauw", Performance = 75, Mileage =18500, Year =2015, SalesPrice = 6500m, ImagePath = "clio.jpg" },
                        new Car { Brand = peugeot, Fuel = benzine, ModelName = "SUV 2008", Color = "Wit", Performance = 105, Mileage =55000, Year =2020, SalesPrice = 18500m, ImagePath = "sub2008wit.jpg" },
                        new Car { Brand = peugeot, Fuel = diesel, ModelName = "SUV 2008", Color = "Muisgrijs", Performance = 195, Mileage =89000, Year =2020, SalesPrice = 22000m, ImagePath = "suv2008grijs.jpg" },
                        new Car { Brand = peugeot, Fuel = elektrisch, ModelName = "E-Rifter", Color = "Geel", Performance = 225, Mileage =12000, Year =2019, SalesPrice = 35000m, ImagePath = "erifter.png" },
                        new Car { Brand = toyota, Fuel = benzine, ModelName = "Aygo X", Color = "Roestbruin", Performance = 95, Mileage =0, Year =2022, SalesPrice = 14360m, ImagePath = "aygo.png" },
                        new Car { Brand = nissan, Fuel = elektrisch, ModelName = "XTrail N-Connecta", Color = "Beige", Performance = 215, Mileage =0, Year =2022, SalesPrice = 49600m, ImagePath = "xtrail.jpg" }
                        );
                    context.SaveChanges();
                }

            }
        }
    }
}
