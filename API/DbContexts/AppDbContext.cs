// ------------------------------------------------------------------
// © Copyright 2024 Thermo Fisher Scientific Inc. All rights reserved.
// ------------------------------------------------------------------
using DateAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DateAppApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
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
                e.Property(p => p.Username)
                    .IsRequired();
                e.Property(p => p.Gender)
                    .IsRequired();
                e.Property(p => p.HashedPassword)
                    .IsRequired();
                e.Property(p => p.TimeJoined)
                    .IsRequired();
            });

            modelBuilder.Entity<DateIdea>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.CreatingUserId)
                    .IsRequired();
                e.Property(p => p.CreatedDate)
                    .IsRequired();
                e.Property(p => p.Description)
                    .IsRequired();
            });

            modelBuilder.Entity<Date>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.CreatedDate)
                    .IsRequired();
                e.Property(p => p.DateStartDate)
                    .IsRequired();
                e.Property(p => p.DateEndDate)
                    .IsRequired();
                e.Property(p => p.CreatingUserId)
                    .IsRequired();
                e.Property(p => p.OtherUserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Image>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Data)
                    .IsRequired();
                e.Property(p => p.CreateDateTime)
                    .IsRequired();
            });

            modelBuilder.Entity<User>()
                .HasOne(e => e.ProfilePicture)
                .WithOne(e => e.ProfilePictureOfUser)
                .HasForeignKey<User>(e => e.ProfilePictureId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CreatedDateIdeas)
                .WithOne(e => e.CreatingUser)
                .HasForeignKey(e => e.CreatingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CreatedDates)
                .WithOne(e => e.CreatingUser)
                .HasForeignKey(e => e.CreatingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PartOfDates)
                .WithOne(e => e.OtherUser)
                .HasForeignKey(e => e.OtherUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Date>()
                .HasMany(e => e.DateIdeas)
                .WithMany(e => e.DatesPresentOn);

            modelBuilder.Entity<Date>()
                .HasMany(e => e.Images)
                .WithOne(e => e.PictureOfDate)
                .HasForeignKey(e => e.PictureOfDateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}