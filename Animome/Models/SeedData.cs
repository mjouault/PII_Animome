using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Animome.Data;
using System;
using System.Linq;

namespace Animome.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any ApplicationUsers.

                if (context.Domaine.Any()) // Pas bon
                {
                    return;   // DB has been seeded
                }

                context.AddRange(
                  
                    new Domaine
                    {
                        Intitule = "D1"
                    },

                    new Domaine
                    {
                        Intitule = "D2"
                    },
                    new Domaine
                    {
                        Intitule = "D3"
                    }

                ); ;
                context.SaveChanges();
            }
        }
    }
}