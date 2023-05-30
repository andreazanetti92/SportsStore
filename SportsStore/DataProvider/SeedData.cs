using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.DataProvider
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDBContext ctx = app.ApplicationServices.CreateScope().ServiceProvider.GetService<StoreDBContext>();

            if(ctx.Database.GetPendingMigrations().Any())
            {
                ctx.Database.Migrate();
            }
            
            if(!ctx.Products.Any())
            {
                ctx.Products.AddRange(
                    new Product
                    {
                        Name = "Korg MicroKors S",
                        Description = "The Legendary Microkorg",
                        Category = "Synthesizers",
                        Price = 499
                    },
                    new Product
                    {
                        Name = "Arturia MicroFreak",
                        Description = "The Legendary MicroFreak",
                        Category = "Synthesizers",
                        Price = 399
                    },
                    new Product
                    {
                        Name = "Arturia MicroBrute",
                        Description = "The Legendary MicroBrute",
                        Category = "Synthesizers",
                        Price = 399
                    },
                    new Product
                    {
                        Name = "Arturia KeyLab 49",
                        Description = "The Legendary KeyLab",
                        Category = "Synthesizers",
                        Price = 499
                    },
                    new Product
                    {
                        Name = "Akai MPK 49",
                        Description = "The Legendary MPK",
                        Category = "Synthesizers",
                        Price = 499
                    },
                    new Product
                    {
                        Name = "Roland T-909",
                        Description = "The Legendary T-909",
                        Category = "Drum Machines",
                        Price = 599
                    },
                    new Product
                    {
                        Name = "Arturia BeatStep Pro",
                        Description = "The Legendary BeatStep Pro",
                        Category = "Drum Machines",
                        Price = 499
                    },
                    new Product
                    {
                        Name = "Behringher Heavy Distortion Pedal",
                        Description = "The Legendary Heavy Distortion Pedal",
                        Category = "Pedal Effects",
                        Price = 499
                    }
                );
                ctx.SaveChanges();
            }
        }
    }
}
