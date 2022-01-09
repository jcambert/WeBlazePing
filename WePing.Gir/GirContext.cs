using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePing.Gir
{
    public class GirContext : DbContext
    {
        public GirContext()
        {

        }
        public GirContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=spiddb;Username=spiddb;Password=spiddb");
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<ModeleRencontre> ModeleRencontre { get; set; }

        public virtual DbSet<Rencontre> Rencontre { get; set; }


        public virtual DbSet<Partie> Parties { get; set; }

        public virtual DbSet<Joueur> Joueurs { get; set; }

        public virtual DbSet<Score> Score { get; set; }
    }
}
