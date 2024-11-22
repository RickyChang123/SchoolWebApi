using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolWebApi.Models;

public partial class SchoolSystemContext : DbContext
{
    public SchoolSystemContext()
    {
    }

    public SchoolSystemContext(DbContextOptions<SchoolSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<SelectClass> SelectClasses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DynaBook-Lab\\MSSQLSERVER01;Database=school_system;User ID=sa;Password=!ricky911017;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cid);

            entity.ToTable("Course");

            entity.Property(e => e.Cid).ValueGeneratedNever();
            entity.Property(e => e.CEnd)
                .HasColumnType("datetime")
                .HasColumnName("c_end");
            entity.Property(e => e.CStart)
                .HasColumnType("datetime")
                .HasColumnName("c_start");
            entity.Property(e => e.CourseDescription)
                .HasMaxLength(50)
                .HasColumnName("course_description");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("course_name");
            entity.Property(e => e.Credits)
                .HasMaxLength(10)
                .HasColumnName("credits");
            entity.Property(e => e.Day)
                .HasMaxLength(10)
                .HasColumnName("day");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.Location)
                .HasMaxLength(10)
                .HasColumnName("location");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.TeacherId).HasColumnName("Teacher_id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK_Course_Teacher");
        });

        modelBuilder.Entity<SelectClass>(entity =>
        {
            entity.HasKey(e => e.Eid);

            entity.ToTable("SelectClass");

            entity.Property(e => e.Eid).ValueGeneratedNever();
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EEnd)
                .HasColumnType("datetime")
                .HasColumnName("e_end");
            entity.Property(e => e.EStart)
                .HasColumnType("datetime")
                .HasColumnName("e_start");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Course).WithMany(p => p.SelectClasses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SelectClass_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.SelectClasses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SelectClass_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid);

            entity.ToTable("Student");

            entity.Property(e => e.Sid).ValueGeneratedNever();
            entity.Property(e => e.SEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("s_email");
            entity.Property(e => e.SPassword)
                 .HasMaxLength(15)
                 .HasColumnName("s_password");
            entity.Property(e => e.SEnd)
                .HasColumnType("datetime")
                .HasColumnName("s_end");
            entity.Property(e => e.SName)
                .HasMaxLength(10)
                .HasColumnName("s_name");
            entity.Property(e => e.SStart)
                .HasColumnType("datetime")
                .HasColumnName("s_start");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.Tid);

            entity.ToTable("Teacher");

            entity.Property(e => e.Tid).ValueGeneratedNever();
            entity.Property(e => e.TEmail)
                .HasMaxLength(50)
                .HasColumnName("t_email");
            entity.Property(e => e.TEnd)
                .HasColumnType("datetime")
                .HasColumnName("t_end");
            entity.Property(e => e.TName)
                .HasMaxLength(10)
                .HasColumnName("t_name");
            entity.Property(e => e.TOffice)
                .HasMaxLength(50)
                .HasColumnName("t_office");
            entity.Property(e => e.TStart)
                .HasColumnType("datetime")
                .HasColumnName("t_start");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
