using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scheduler.Windows;

namespace Scheduler.Models;

public partial class SchedulerDbContext : DbContext
{
    public static IConfiguration AppConfig = null!;
    public static SchedulerDbContext dbContext = null!;

    public SchedulerDbContext(IConfiguration configuration)
    {
        AppConfig = configuration;
    }

    public virtual DbSet<Cabinet> Cabinets { get; set; }

    public virtual DbSet<ClassesTimingBody> ClassesTimingBodies { get; set; }

    public virtual DbSet<ClassesTimingHeader> ClassesTimingHeaders { get; set; }

    public virtual DbSet<DailyScheduleBody> DailyScheduleBodies { get; set; }

    public virtual DbSet<DailyScheduleHeader> DailyScheduleHeaders { get; set; }

    public virtual DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EventLog> EventLogs { get; set; }

    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Tution> Tutions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ConnectionPickWindow connectionPickWindow = new ConnectionPickWindow();
        connectionPickWindow.ShowDialog();
        optionsBuilder.UseNpgsql(connectionPickWindow.ReturnString);
    }

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
            entity.Property(e => e.TutionRowId).HasColumnName("TutionRow_ID");

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

            entity.HasOne(d => d.TutionRow).WithMany(p => p.DailyScheduleBodies)
                .HasForeignKey(d => d.TutionRowId)
                .HasConstraintName("DailySchedule_body_TutionRow_ID_fkey");
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

        modelBuilder.Entity<EventLog>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("EVENT_LOG_pkey");

            entity.ToTable("EVENT_LOG");

            entity.Property(e => e.EventId).HasColumnName("Event_ID");
            entity.Property(e => e.Level).HasColumnType("character varying");
            entity.Property(e => e.Message).HasColumnType("character varying");
            entity.Property(e => e.Time).HasColumnType("timestamp without time zone");
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

        modelBuilder.Entity<Tution>(entity =>
        {
            entity.HasKey(e => e.TutionRowId).HasName("Tution_pkey");

            entity.ToTable("Tution");

            entity.Property(e => e.TutionRowId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("TutionRow_ID");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");
            entity.Property(e => e.SubjectId).HasColumnName("Subject_ID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Tutions)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tution_Employee_ID_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.Tutions)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tution_Subject_ID_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
