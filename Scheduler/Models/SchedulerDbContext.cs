using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.Models;

public partial class SchedulerDbContext : DbContext
{
    public static SchedulerDbContext dbContext = new SchedulerDbContext();

    public SchedulerDbContext()
    {
    }

    public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<ClassesTimingBody> ClassesTimingBodies { get; set; }

    public virtual DbSet<ClassesTimingHeader> ClassesTimingHeaders { get; set; }

    public virtual DbSet<DailyScheduleBody> DailyScheduleBodies { get; set; }

    public virtual DbSet<DailyScheduleHeader> DailyScheduleHeaders { get; set; }

    public virtual DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ScheduleView> ScheduleViews { get; set; }

    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TutionLog> TutionLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=SchedulerDB;UserName=postgres;Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabinet>(entity =>
        {
            entity.HasKey(e => e.CabinetId).HasName("Cabinet_pkey");

            entity.ToTable("Cabinet");

            entity.HasIndex(e => e.Number, "Cabinet_Number_key").IsUnique();

            entity.Property(e => e.CabinetId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Cabinet_ID");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<ClassesTimingBody>(entity =>
        {
            entity.HasKey(e => e.TimeSlotId).HasName("ClassesTiming_body_pkey");

            entity.ToTable("ClassesTiming_body");

            entity.Property(e => e.TimeSlotId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("TimeSlot_ID");
            entity.Property(e => e.ClassesTimingHeaderId).HasColumnName("ClassesTiming_header_ID");

            entity.HasOne(d => d.ClassesTimingHeader).WithMany(p => p.ClassesTimingBodies)
                .HasForeignKey(d => d.ClassesTimingHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClassesTiming_body_ClassesTiming_header_ID_fkey");
        });

        modelBuilder.Entity<ClassesTimingHeader>(entity =>
        {
            entity.HasKey(e => e.ClassesTimingHeaderId).HasName("ClassesTiming_header_pkey");

            entity.ToTable("ClassesTiming_header");

            entity.HasIndex(e => e.Name, "ClassesTiming_header_Name_key").IsUnique();

            entity.Property(e => e.ClassesTimingHeaderId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("ClassesTiming_header_ID");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<DailyScheduleBody>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("DailySchedule_body_pkey");

            entity.ToTable("DailySchedule_body");

            entity.Property(e => e.LessonId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Lesson_ID");
            entity.Property(e => e.CabinetId).HasColumnName("Cabinet_ID");
            entity.Property(e => e.DailyScheduleHeaderId).HasColumnName("DailySchedule_header_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");
            entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");
            entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlot_ID");

            entity.HasOne(d => d.Cabinet).WithMany(p => p.DailyScheduleBodies)
                .HasForeignKey(d => d.CabinetId)
                .HasConstraintName("DailySchedule_body_Cabinet_ID_fkey");

            entity.HasOne(d => d.DailyScheduleHeader).WithMany(p => p.DailyScheduleBodies)
                .HasForeignKey(d => d.DailyScheduleHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DailySchedule_body_DailySchedule_header_ID_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.DailyScheduleBodies)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("DailySchedule_body_Employee_ID_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.DailyScheduleBodies)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("DailySchedule_body_Subject_ID_fkey");

            entity.HasOne(d => d.TimeSlot).WithMany(p => p.DailyScheduleBodies)
                .HasForeignKey(d => d.TimeSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DailySchedule_body_TimeSlot_ID_fkey");
        });

        modelBuilder.Entity<DailyScheduleHeader>(entity =>
        {
            entity.HasKey(e => e.DailyScheduleHeaderId).HasName("DailySchedule_header_pkey");

            entity.ToTable("DailySchedule_header");

            entity.Property(e => e.DailyScheduleHeaderId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("DailySchedule_header_ID");
            entity.Property(e => e.ClassesTimingHeaderId).HasColumnName("ClassesTiming_header_ID");
            entity.Property(e => e.DayOfTheWeekId).HasColumnName("DayOfTheWeek_ID");
            entity.Property(e => e.StudentGroupId).HasColumnName("StudentGroup_ID");

            entity.HasOne(d => d.ClassesTimingHeader).WithMany(p => p.DailyScheduleHeaders)
                .HasForeignKey(d => d.ClassesTimingHeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DailySchedule_header_ClassesTiming_header_ID_fkey");

            entity.HasOne(d => d.DayOfTheWeek).WithMany(p => p.DailyScheduleHeaders)
                .HasForeignKey(d => d.DayOfTheWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DailySchedule_header_DayOfTheWeek_ID_fkey");

            entity.HasOne(d => d.StudentGroup).WithMany(p => p.DailyScheduleHeaders)
                .HasForeignKey(d => d.StudentGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DailySchedule_header_StudentGroup_ID_fkey");
        });

        modelBuilder.Entity<DayOfTheWeek>(entity =>
        {
            entity.HasKey(e => e.DayOfTheWeekId).HasName("DayOfTheWeek_pkey");

            entity.ToTable("DayOfTheWeek");

            entity.Property(e => e.DayOfTheWeekId)
                .ValueGeneratedNever()
                .HasColumnName("DayOfTheWeek_ID");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Login, "Employee_Login_key").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Employee_ID");
            entity.Property(e => e.EMail)
                .HasColumnType("character varying")
                .HasColumnName("E-mail");
            entity.Property(e => e.Login).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("character varying")
                .HasColumnName("Phone_Number");
            entity.Property(e => e.TelegramId)
                .HasColumnType("character varying")
                .HasColumnName("Telegram_ID");
        });

        modelBuilder.Entity<ScheduleView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ScheduleView");

            entity.Property(e => e.AtCabinet).HasColumnName("At cabinet");
            entity.Property(e => e.AtDay)
                .HasColumnType("character varying")
                .HasColumnName("At day");
            entity.Property(e => e.ClassesTimings)
                .HasColumnType("character varying")
                .HasColumnName("Classes timings");
            entity.Property(e => e.EndTime).HasColumnName("End time");
            entity.Property(e => e.OfDate).HasColumnName("Of date");
            entity.Property(e => e.StartTime).HasColumnName("Start time");
            entity.Property(e => e.StudentGroup)
                .HasColumnType("character varying")
                .HasColumnName("Student group");
            entity.Property(e => e.Subject).HasColumnType("character varying");
            entity.Property(e => e.Tutor).HasColumnType("character varying");
        });

        modelBuilder.Entity<StudentGroup>(entity =>
        {
            entity.HasKey(e => e.StudentGroupId).HasName("StudentGroup_pkey");

            entity.ToTable("StudentGroup");

            entity.HasIndex(e => e.Code, "StudentGroup_Code_key").IsUnique();

            entity.Property(e => e.StudentGroupId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("StudentGroup_ID");
            entity.Property(e => e.Code).HasColumnType("character varying");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("Subject_pkey");

            entity.ToTable("Subject");

            entity.HasIndex(e => e.Name, "Subject_Name_key").IsUnique();

            entity.Property(e => e.SubjectId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("Subject_ID");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<TutionLog>(entity =>
        {
            entity.HasKey(e => e.StatementId).HasName("TutionLog_pkey");

            entity.ToTable("TutionLog");

            entity.Property(e => e.StatementId)
                .ValueGeneratedNever()
                .HasColumnName("Statement_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");
            entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.TutionLogs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TutionLog_Employee_ID_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.TutionLogs)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TutionLog_Subject_ID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
