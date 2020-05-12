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
                Domaine d1 = new Domaine("Domaine Motricité globale");
                Domaine d2 = new Domaine("Domaine Motricité fine");
                Domaine d3 = new Domaine("Domaine Langage");
                Domaine d4 = new Domaine("Domaine Cognition");

                if (!context.Domaine.Any()) // Si vide
                {
                    context.Domaine.AddRange(d1, d2, d3, d4) ;
                    context.SaveChanges(); ;   // DB has been seeded
                }

                Competence c1 = new Competence("Compétence Motricité");
                Competence c2 = new Competence("COmpétence Motricite Globale 1");
                Competence c3 = new Competence("COmpétence Motricite Fine 1");
                Competence c4 = new Competence("Compétence Langage 1");
                Competence c5 = new Competence("Compétence Cognition 1");

                if (!context.Competence.Any())
                {
                    context.Competence.AddRange(c1,c2,c3,c4,c5);

                    context.SaveChanges();   // DB has been seeded
                }

                Prerequis p1 = new Prerequis("Prérequis 1");
                Prerequis p2 = new Prerequis("Prérequis 2");
                Prerequis p3 = new Prerequis("Prérequis 3");
                Prerequis p4 = new Prerequis("Prérequis 4");

                if (!context.Prerequis.Any()) 
                {
                    context.Prerequis.AddRange(p1,p2,p3,p4); 
                    context.SaveChanges();   // DB has been seeded
                }

                Niveau n1 = new Niveau("N1");
                Niveau n2 = new Niveau("N2");
                Niveau n3 = new Niveau("N3");
                Niveau n4 = new Niveau("N4");

                if (context.Niveau.Any()) 
                {
                    context.Niveau.AddRange(n1,n2,n3, n4); 
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