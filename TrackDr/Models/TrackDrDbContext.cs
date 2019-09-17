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
        public virtual DbSet<Child> Child { get; set; }
        public virtual DbSet<ChildDoctor> ChildDoctor { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Parent> Parent { get; set; }
        public virtual DbSet<ParentDoctor> ParentDoctor { get; set; }
        public virtual DbSet<SavedInsurance> SavedInsurance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                optionsBuilder.UseSqlServer("DefaultConnection");

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

            modelBuilder.Entity<Child>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(64);

                entity.Property(e => e.ParentId).HasMaxLength(450);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Child)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__Child__ParentId__4D94879B");
            });

            modelBuilder.Entity<ChildDoctor>(entity =>
            {
                entity.Property(e => e.ChildDoctorId).ValueGeneratedNever();

                entity.Property(e => e.DoctorId).HasMaxLength(450);

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.ChildDoctor)
                    .HasForeignKey(d => d.ChildId)
                    .HasConstraintName("FK__ChildDoct__Child__5165187F");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ChildDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__ChildDoct__Docto__5070F446");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SavedInsurance>(entity =>
            {
                entity.HasKey(e => e.InsuranceUid)
                    .HasName("PK__Insuranc__E770AC3DDC00211E");

                entity.Property(e => e.InsuranceUid)
                    .HasColumnName("InsuranceUID")
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.InsuranceBaseName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.InsuranceSpecialtyName).HasMaxLength(128);
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.Property(e => e.ParentId).ValueGeneratedNever();

                entity.Property(e => e.City).HasMaxLength(64);

                entity.Property(e => e.Email).HasMaxLength(128);

                entity.Property(e => e.HouseNumber).HasMaxLength(16);

                entity.Property(e => e.InsuranceBaseName).HasMaxLength(64);

                entity.Property(e => e.PhoneNumber).HasMaxLength(11);

                entity.Property(e => e.State).HasMaxLength(32);

                entity.Property(e => e.Street).HasMaxLength(64);

                entity.Property(e => e.Street2).HasMaxLength(64);

                entity.Property(e => e.ZipCode).HasMaxLength(10);
            });

            modelBuilder.Entity<ParentDoctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasMaxLength(450);

                entity.Property(e => e.ParentId).HasMaxLength(450);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ParentDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__ParentDoc__Docto__5441852A");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ParentDoctor)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__ParentDoc__Paren__5535A963");
            });

            modelBuilder.Entity<SavedInsurance>(entity =>
            {
                entity.HasKey(e => e.InsuranceUid)


                    //.HasName("PK__Insuranc__E770AC3D62E32930");

                    .HasName("PK__Insuranc__E770AC3DDC00211E");

                    //.HasName("PK__Insuranc__E770AC3DBA24CC1B");



                entity.Property(e => e.InsuranceUid)
                    .HasColumnName("InsuranceUID")
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.InsuranceBaseName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.InsuranceSpecialtyName).HasMaxLength(128);
            });
        }
    }
}
