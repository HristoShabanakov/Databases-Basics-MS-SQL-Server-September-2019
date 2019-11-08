using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.PatientId);

                entity.Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

                entity.Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

                entity.Property(p => p.Address)
                .HasMaxLength(250)
                .IsUnicode(true)
                .IsRequired(true);

                entity.Property(p => p.Email)
                .HasMaxLength(80)
                .IsUnicode(true)
                .IsRequired(true);

                entity.Property(p => p.HasInsurance)
                .IsRequired(true);

                entity.HasMany(p => p.Visitations).WithOne(v => v.Patient);

                entity.HasMany(p => p.Diagnoses).WithOne(d => d.Patient);



            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(d => d.DiagnoseId);

                entity.Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

                entity.Property(d => d.Comments)
                .HasMaxLength(250)
                .IsUnicode(true);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(m => m.MedicamentId);

                entity.Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(pm => new
                {
                    pm.PatientId,
                    pm.MedicamentId
                });

                entity.HasOne(pm => pm.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(pm => pm.PatientId);

                entity.HasOne(pm => pm.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(pm => pm.MedicamentId);
            });
        }
    }
}
