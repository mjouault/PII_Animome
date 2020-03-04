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

                if (!context.Domaine.Any()) // Si vide
                {
                    context.Domaine.AddRange(
                    new Domaine
                    {
                        Intitule = "DMotricité globale"
                    },

                    new Domaine
                    {
                        Intitule = "DMotricité fine"
                    },

                    new Domaine
                    {
                        Intitule = "DLangage"
                    }
                    );

                    context.SaveChanges(); ;   // DB has been seeded
                }


                if (!context.Competence.Any()) // Pas bon
                {
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

                    context.SaveChanges();   // DB has been seeded
                }

                if (!context.Prerequis.Any()) // Pas bon
                {
                    context.Prerequis.AddRange(
                 new Prerequis
                 {
                     Intitule = "P1"
                 },

                 new Prerequis
                 {
                     Intitule = "P2"
                 },

                 new Prerequis
                 {
                     Intitule = "P3"
                 }

                 ); 
                    context.SaveChanges();   // DB has been seeded
                }

                if (context.Niveau.Any()) // Pas bon
                {
                    context.Niveau.AddRange(
                   new Niveau
                   {
                       Intitule = "N1"
                   },

                   new Niveau
                   {
                       Intitule = "N2"
                   },

                    new Niveau
                    {
                        Intitule = "N3"
                    }

                   ); 
                    context.SaveChanges();   // DB has been seeded
                }
            }
        }
    }
}