using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrackDr.Models
{
    public partial class TrackDrDbContext : DbContext
    {
        public TrackDrDbContext()
        {
        }

        public TrackDrDbContext(DbContextOptions<TrackDrDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<TrackDrUser> TrackDrUser { get; set; }
        public virtual DbSet<UserChild> UserChild { get; set; }
        public virtual DbSet<UserDoctor> UserDoctor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=TrackDrDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(32)
                    .ValueGeneratedNever();

                entity.HasOne(d => d.UserDoctor)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.UserDoctorId)
                    .HasConstraintName("FK__Doctor__UserDoct__02FC7413");
            });

            modelBuilder.Entity<TrackDrUser>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Street2).HasMaxLength(256);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.HasOne(d => d.UserDoctor)
                    .WithMany(p => p.TrackDrUser)
                    .HasForeignKey(d => d.UserDoctorId)
                    .HasConstraintName("FK__TrackDrUs__UserD__05D8E0BE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TrackDrUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__TrackDrUs__UserI__06CD04F7");
            });

            modelBuilder.Entity<UserChild>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.UserChild)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__UserChild__Paren__09A971A2");
            });

            modelBuilder.Entity<UserDoctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasMaxLength(32);

                entity.HasOne(d => d.DoctorNavigation)
                    .WithMany(p => p.UserDoctorNavigation)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__UserDocto__Docto__160F4887");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDoctorNavigation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserDocto__UserI__151B244E");
            });
        }
    }
}
