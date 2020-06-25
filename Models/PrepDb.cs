
using FileManager.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using server.Models;
using System;
using System.Linq;


// User for local development with docker do to add migrations and seed some initial data to DB
namespace FileManager.Models
{
       public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }

        }

        public static void SeedData(DataContext context)
        {
            System.Console.WriteLine("Appling Migrations...");

            context.Database.Migrate();

            // if(!context.Words.Any()){
            //     System.Console.WriteLine("Data seeding");
            //     context.Words.AddRange(
            //       new Words(){Id=new Guid(),Text="add",Value=(decimal)-0.3},
            //       new Words(){Id=new Guid(),Text="help",Value=(decimal)0.3},
            //       new Words(){Id=new Guid(),Text="test",Value=(decimal)0.7},
            //       new Words(){Id=new Guid(),Text="nice",Value=(decimal)-0.45},
            //       new Words(){Id=new Guid(),Text="bad",Value=(decimal)1}  
            //     );

            //     context.SaveChanges();
            // }
            // else{
            //     System.Console.WriteLine("Already has data");
            // }
        }
    }
}