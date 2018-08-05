using Microsoft.EntityFrameworkCore;
using System;

namespace ClippingKKModel
{
    public class ClippingContext: DbContext
    {
        public DbSet<ClippingItem> Clippings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=clippingkk.db");
        }

        public void Migrate()
        {
            this.Database.Migrate();
        }
    }

    public class ClippingItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
        public string Author { get; set; }
        public string DataId { get; set; }
    }
}
