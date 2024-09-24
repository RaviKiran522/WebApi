using Microsoft.EntityFrameworkCore;
using NZwalks.Models.Domain;

namespace NZwalks.Data
{
    public class NZWalksContext: DbContext
    {
        //ctrl + . => get import suggestions
        public NZWalksContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
