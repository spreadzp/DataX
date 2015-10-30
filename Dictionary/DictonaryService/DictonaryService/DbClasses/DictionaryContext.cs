using System.Data.Common;
using System.Data.Entity;

namespace DictonaryService
{
    public class DictonaryContext : DbContext 
    {
       public DictonaryContext()
            : base("DbConnection")
       {
       }

       public DictonaryContext(DbConnection connection)
               : base(connection, true)
       {
           this.Configuration.LazyLoadingEnabled = false;
       }

       public DbSet<RusDictonary> RusDictonaries
       {
           get;
           set;
       }

       public DbSet<EngDictonary> EngDictonaries
       {
           get;
           set;
       }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          modelBuilder.Entity<RusDictonary>()
                    .HasMany<EngDictonary>(s => s.Words)
                    .WithMany(c => c.Words)
                    .Map(cs =>
                    {
                        cs.MapLeftKey("RusDictionaryRefId");
                        cs.MapRightKey("EngDictionaryRefId");
                        cs.ToTable("RelationsTables");
                    });
        }
    }
}