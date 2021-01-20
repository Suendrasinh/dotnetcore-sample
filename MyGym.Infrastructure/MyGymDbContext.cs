using Microsoft.EntityFrameworkCore;
using MyGym.Core.Model;

namespace MyGym.Infrastructure
{
    /// <summary>
    ///     My Gym Db Context.
    /// </summary>
    public class MyGymDbContext : DbContext
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="options"></param>
        public MyGymDbContext(DbContextOptions<MyGymDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        ///     On Model Creating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(50);

                entity.Property(e => e.Email)
                 .IsRequired()
                 .HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });
        }
    }
}
