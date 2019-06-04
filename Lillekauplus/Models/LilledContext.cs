using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lillekauplus.Models
{
    public class LilledContext : DbContext
    {
        public DbSet<Lilled> Lilleds { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }

    public class LilledDbInitializer : DropCreateDatabaseIfModelChanges<LilledContext>
    {
        protected override void Seed(LilledContext db)
        {
            db.Lilleds.Add(new Lilled { Nimetus = "Romawka", Muuja = "Ashot", Hind = 300 });
            db.Lilleds.Add(new Lilled { Nimetus = "Rabarbar", Muuja = "Uzber", Hind = 300 });


            base.Seed(db);
        }
    }
}