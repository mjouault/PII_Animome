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

                if (context.Domaine.Any()) // Pas bon
                {
                    return;   // DB has been seeded
                }

                context.Domaine.AddRange(
                    new Domaine 
                    {
                        Intitule = "CMotricité globale"
                    },

                    new Domaine 
                    {
                        Intitule = "CMotricité fine"
                    },

                    new Domaine
                    {
                        Intitule = "CLangage"
                    }
                );
                
                context.Competence.AddRange(
                     new Competence
                     {
                         Intitule = "C1"
                     },

                    new Competence
                    {
                        Intitule = "C2"
                    },

                    new Competence
                    {
                        Intitule = "C3"
                    }
                );

                context.DomaineCompetence.AddRange(
                    );
                context.SaveChanges();
            }
        }
    }
}