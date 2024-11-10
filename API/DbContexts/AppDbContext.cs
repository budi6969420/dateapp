// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DateIdea> DateIdeas { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(k => k.Id);
                e.Property(p => p.Username).IsRequired().HasMaxLength(100);
                e.Property(p => p.Gender).IsRequired();
                e.Property(p => p.HashedPassword).IsRequired();
                e.Property(p => p.TimeJoined).IsRequired();

                e.HasOne(u => u.ProfilePicture)
                    .WithOne(i => i.ProfilePictureOfUser)
                    .HasForeignKey<User>(u => u.ProfilePictureId)
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasMany(u => u.CreatedDateIdeas)
                    .WithOne(d => d.CreatingUser)
                    .HasForeignKey(d => d.CreatingUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(u => u.CreatedDates)
                    .WithOne(d => d.CreatingUser)
                    .HasForeignKey(d => d.CreatingUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(u => u.PartOfDates)
                    .WithOne(d => d.OtherUser)
                    .HasForeignKey(d => d.OtherUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DateIdea>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.Description).IsRequired();
                e.Property(d => d.CreatedDate).IsRequired();

                e.HasMany(d => d.DatesPresentOn)
                    .WithMany(d => d.DateIdeas);
            });

            modelBuilder.Entity<Date>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.CreatedDate).IsRequired();
                e.Property(d => d.DateStartDate).IsRequired();
                e.Property(d => d.DateEndDate).IsRequired();
                e.Property(d => d.CreatingUserId).IsRequired();
                e.Property(d => d.OtherUserId).IsRequired();

                e.HasMany(d => d.DateIdeas)
                    .WithMany(i => i.DatesPresentOn);

                e.HasMany(d => d.Images)
                    .WithOne(i => i.PictureOfDate)
                    .HasForeignKey(i => i.PictureOfDateId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Image>(e =>
            {
                e.HasKey(i => i.Id);
                e.Property(i => i.Data).IsRequired();
                e.Property(i => i.CreateDateTime).IsRequired();
                e.HasOne(i => i.PictureOfDate)
                    .WithMany(d => d.Images)
                    .HasForeignKey(i => i.PictureOfDateId);
            });
        }
    }
}