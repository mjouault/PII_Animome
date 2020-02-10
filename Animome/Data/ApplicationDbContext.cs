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
        public DbSet<Competence> Competence { get; set; }
        public DbSet<Prerequis> Prerequis { get; set; }
        public DbSet<Niveau> Niveau { get; set; }

        public DbSet<PatientCompetence> PatientCompetence { get; set; }
    }
}
