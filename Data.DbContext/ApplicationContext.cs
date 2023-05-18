using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.DbContext;

public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ApplicationContext(){ }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Procedure> Procedures { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Treatment> Treatments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Procedure>().HasKey(m => m.ProcedureId);
        builder.Entity<Provider>().HasKey(m => m.ProviderId);
        builder.Entity<Treatment>().HasKey(m => m.TreatmentId);
        builder.Entity<Patient>().HasKey(m => m.PatientId);

        base.OnModelCreating(builder);
    }
}