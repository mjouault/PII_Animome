using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Animome.Models;

namespace Animome.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Suivi> Suivi { get; set; }
        public DbSet<SuiviCompetence> SuiviCompetence { get; set; }
        public DbSet<SuiviPrerequis> SuiviPrerequis { get; set; }
        public DbSet<SuiviNiveau> SuiviNiveau { get; set; }
        public DbSet<SuiviExercice> SuiviExercice { get; set; }
        public DbSet<SuiviApplicationUser> SuiviApplicationUser { get; set; }

        public DbSet<Competence> Competence { get; set; }
        public DbSet<Domaine> Domaine { get; set; }
        public DbSet<Niveau> Niveau { get; set; }
        public DbSet<Prerequis> Prerequis { get; set; }
        public DbSet<Exercice> Exercice { get; set; }
        public DbSet<DomaineCompetence> DomaineCompetence { get; set; }
        public DbSet<CompetencePrerequis> CompetencePrerequis { get; set; }
        public DbSet<PrerequisNiveau> GetPrerequisNiveau { get; set; }
        public DbSet<NiveauExercice> NiveauExercice { get; set; }
    }
}
