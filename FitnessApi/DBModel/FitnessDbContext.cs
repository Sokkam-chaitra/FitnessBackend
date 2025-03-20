using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FitnessApi.DBModel;

public partial class FitnessDbContext : DbContext
{
    public FitnessDbContext()
    {
    }

    public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calory> Calories { get; set; }

    public virtual DbSet<Tblworkout> Tblworkouts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=localhost;integrated security=SSPI;database=FitnessDb;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calory>(entity =>
        {
            entity.HasKey(e => e.Calorieid).HasName("PK__Calories__7F5FB65E57BD066B");

            entity.Property(e => e.Calorieid).HasColumnName("calorieid");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Fooditem)
                .HasMaxLength(255)
                .HasColumnName("fooditem");
            entity.Property(e => e.Indate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Mealtype)
                .HasMaxLength(50)
                .HasColumnName("mealtype");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Calories)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__Calories__userid__46E78A0C");
        });

        modelBuilder.Entity<Tblworkout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tblworko__3213E83F1902FBAD");

            entity.HasIndex(e => e.WorkoutName, "UQ__Tblworko__CA46FABC20B23BBD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Indate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Maxcalories).HasColumnName("maxcalories");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.WorkoutName)
                .HasMaxLength(255)
                .HasColumnName("workout_name");

            entity.HasOne(d => d.User).WithMany(p => p.Tblworkouts)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FBBB77ACD");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC572235D11F0").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityLevel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("activity_level");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_time");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
