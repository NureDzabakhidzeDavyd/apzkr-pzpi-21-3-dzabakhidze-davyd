using Kolosok.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Action = Kolosok.Domain.Entities.Action;

namespace Kolosok.Infrastructure;

public class KolosokDbContext : DbContext
{
    public KolosokDbContext(DbContextOptions<KolosokDbContext> options) : base(options)
        {
        }

        public DbSet<Victim> Victims { get; set; }
        public DbSet<BrigadeRescuer> BrigadeRescuers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
            .HasOne(c => c.Victim)
            .WithOne(p => p.Contact)
            .HasForeignKey<Victim>(p => p.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
            .HasOne(c => c.BrigadeRescuer)
            .WithOne(p => p.Contact)
            .HasForeignKey<BrigadeRescuer>(p => p.ContactId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Brigade>()
                .HasMany(p => p.BrigadeRescuers)
                .WithOne(m => m.Brigade)
                .HasForeignKey(p => p.BrigadeId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Victim>()
                .HasMany(p => p.Diagnoses)
                .WithOne(m => m.Victim)
                .HasForeignKey(p => p.VictimId);
            
            modelBuilder.Entity<BrigadeRescuer>()
                .HasMany(p => p.Actions)
                .WithOne(m => m.BrigadeRescuer)
                .HasForeignKey(p => p.BrigadeRescuerId);
            
            modelBuilder.Entity<Victim>()
                .HasMany(p => p.Actions)
                .WithOne(m => m.Victim)
                .HasForeignKey(p => p.VictimId);
            
            modelBuilder.Entity<Victim>()
                .HasOne(p => p.BrigadeRescuer)
                .WithMany(m => m.Victims)
                .HasForeignKey(p => p.BrigadeRescuerId)
                .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Contact>()
        .HasIndex(c => new { c.LastName, c.FirstName });

        modelBuilder.Entity<BrigadeRescuer>()
            .HasIndex(br => br.Specialization);

        modelBuilder.Entity<Victim>()
            .HasIndex(v => v.ContactId);

        modelBuilder.Entity<Contact>().Property(e => e.Role).HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            var entities = ChangeTracker.Entries().ToList();
            var added = entities.Where(x => x.State == EntityState.Added).Select(x => x.Entity);
            var updated = entities.Where(x => x.State == EntityState.Modified).Select(x => x.Entity);
            var time = DateTime.UtcNow;

            foreach (var item in added.OfType<BaseEntity>())
            {
                item.DateCreated = time;
                item.DateUpdated = time;
            }

            foreach (var item in updated.OfType<BaseEntity>())
            {
                item.DateUpdated = time;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
}