
using asw.Model;

using Microsoft.EntityFrameworkCore;

namespace asw.Data
{
    public class My_db_s:DbContext
    {

        public My_db_s(DbContextOptions<My_db_s> options):base(options) 
        {
            
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Departl> departls { get; set; }
        public DbSet<Invoice>invoices { get; set; }
        public DbSet<Doctor>doctors { get; set; }
        public DbSet<Nu>nus { get; set; }
        public DbSet<User>users { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<MedicalRecord> medicalRecords { get; set; }

       
    }

}
