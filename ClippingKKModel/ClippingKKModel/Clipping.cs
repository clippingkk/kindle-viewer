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

    public class KKBookItem
    {
        public int id { get; set; }
        public double rating { get; set; }
        public string author { get; set; }
        public string originTitle { get; set; }
        public string image { get; set; }
        public string doubanId { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string authorIntro { get; set; }
        public string summary { get; set; }
        public DateTime pubdate { get; set; }
    }
}
