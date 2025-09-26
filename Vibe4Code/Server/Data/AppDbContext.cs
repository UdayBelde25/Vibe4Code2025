using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<LogEntry> Logs => Set<LogEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Email).HasMaxLength(200);
                entity.Property(p => p.PhoneNumber).HasMaxLength(50);
                entity.Property(p => p.Address).HasMaxLength(500);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(d => d.Name).IsRequired().HasMaxLength(200);
                entity.Property(d => d.Specialization).HasMaxLength(200);
                entity.Property(d => d.Email).HasMaxLength(200);
                entity.Property(d => d.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.Patient)
                    .WithMany()
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Doctor)
                    .WithMany()
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.Property(l => l.Action).IsRequired().HasMaxLength(200);
                entity.Property(l => l.UserType).IsRequired().HasMaxLength(100);
            });
        }
    }
}


