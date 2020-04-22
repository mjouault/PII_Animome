using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Animome.Data;
using System;
using System.Linq;

namespace Animome.Models
{
    public static class SeedData
    {
        /// <summary>
        /// Base de données initiale
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                Domaine d1 = new Domaine("Motricité globale");
                Domaine d2 = new Domaine("Motricité fine");
                Domaine d3 = new Domaine("Langage");
                Domaine d4 = new Domaine("Habiletés sociales");

                if (!context.Domaine.Any()) // Si vide
                {
                    context.Domaine.AddRange(d1, d2, d3, d4) ;
                    context.SaveChanges(); ;   // DB has been seeded
                }

                Competence c1 = new Competence("Compétence Motricite 1");
                Competence c2 = new Competence("COmpétence Motricite Globale 2");
                Competence c3 = new Competence("COmpétence Motricite Fine 2");
                Competence c4 = new Competence("Compétence Langage 1");
                Competence c5 = new Competence("Compétence Habilite Sociale 1");

                if (!context.Competence.Any())
                {
                    context.Competence.AddRange(c1,c2,c3,c4,c5);

                    context.SaveChanges();   // DB has been seeded
                }

                Prerequis p1 = new Prerequis("P1");
                Prerequis p2 = new Prerequis("P2");
                Prerequis p3 = new Prerequis("P3");
                Prerequis p4 = new Prerequis("P4");
                Prerequis p5 = new Prerequis("P5");
                Prerequis p6 = new Prerequis("P6");

                if (!context.Prerequis.Any()) 
                {
                    context.Prerequis.AddRange(p1,p2,p3,p4,p5,p6); 
                    context.SaveChanges();   // DB has been seeded
                }

                Niveau n1 = new Niveau("N1");
                Niveau n2 = new Niveau("N2");
                Niveau n3 = new Niveau("N3");

                if (context.Niveau.Any()) 
                {
                    context.Niveau.AddRange(n1,n2,n3); 
                    context.SaveChanges();   // DB has been seeded
                }

                if(!context.DomaineCompetence.Any())
                {
                    context.DomaineCompetence.AddRange(
                        new DomaineCompetence
                        {
                            Domaine = d1,
                            Competence = c1
                        },

                         new DomaineCompetence
                         {
                             Domaine = d1,
                             Competence = c2
                         },

                          new DomaineCompetence
                          {
                              Domaine = d2,
                              Competence = c1
                          },

                           new DomaineCompetence
                           {
                               Domaine = d1,
                               Competence = c3
                           },

                            new DomaineCompetence
                            {
                                Domaine = d3,
                                Competence = c4
                            },

                             new DomaineCompetence
                             {
                                 Domaine = d4,
                                 Competence = c5
                             }
                        );
                }

                if(!context.CompetencePrerequis.Any())
                {
                    context.CompetencePrerequis.AddRange(
                    new CompetencePrerequis
                    {
                        Competence = c1,
                        Prerequis = p1
                    },

                     new CompetencePrerequis
                     {
                         Competence = c1,
                         Prerequis = p2
                     },

                      new CompetencePrerequis
                      {
                          Competence = c2,
                          Prerequis = p1
                      },

                       new CompetencePrerequis
                       {
                           Competence = c2,
                           Prerequis = p3
                       },

                        new CompetencePrerequis
                        {
                            Competence = c3,
                            Prerequis = p4
                        },

                         new CompetencePrerequis
                         {
                             Competence = c4,
                             Prerequis = p5
                         },

                          new CompetencePrerequis
                          {
                              Competence = c5,
                              Prerequis = p5
                          },

                           new CompetencePrerequis
                           {
                               Competence = c5,
                               Prerequis = p6
                           }
                     );
                }

                if(!context.PrerequisNiveau.Any())
                {
                    context.PrerequisNiveau.AddRange(
                        new PrerequisNiveau
                        {
                            Niveau = n1,
                            Prerequis = p1
                        },

                        new PrerequisNiveau
                        {
                            Niveau = n1,
                            Prerequis = p2
                        },

                        new PrerequisNiveau
                        {
                            Niveau = n1,
                            Prerequis = p3
                        },

                        new PrerequisNiveau
                        {
                            Niveau = n2,
                            Prerequis = p1
                        },

                        new PrerequisNiveau
                        {
                            Niveau = n2,
                            Prerequis = p2
                        },

                        new PrerequisNiveau
                        {
                            Niveau = n3,
                            Prerequis = p3
                        }
                        );
                }
            }
        }
    }
}