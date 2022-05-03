using Microsoft.EntityFrameworkCore;
using HealthAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HealthAPI.Repositories.Configuration;

namespace HealthAPI.Data
{
    public class HealthAPIContext : IdentityDbContext<User>
    {
        public HealthAPIContext (DbContextOptions<HealthAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User - AutoDate
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);
            #endregion

            #region Patient - AutoDate
            modelBuilder.Entity<Patient>()
                .Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Patient>()
                .Property(u => u.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);
            #endregion

            #region User -> Patient Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(u => u.User)
                .HasForeignKey<Patient>(p => p.UserId);
            #endregion

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public virtual DbSet<Patient> Patients { get; set; }
    }
}
