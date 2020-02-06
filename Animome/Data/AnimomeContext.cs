using Microsoft.EntityFrameworkCore;
using Animome.Models;


namespace Animome.Data
{
    public class AnimomeContext: DbContext
    {
        public AnimomeContext(DbContextOptions<AnimomeContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Competence> Competence { get; set; }
        public DbSet<Prerequis> Prerequis { get; set; }
        public DbSet<Niveau> Niveau { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
    }
}
