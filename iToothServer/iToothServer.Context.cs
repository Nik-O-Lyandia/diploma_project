using Microsoft.EntityFrameworkCore;

namespace iToothServer
{
    public class iToothServerContext : DbContext
    {
        public virtual DbSet<bill> bill { get; set; }
        public virtual DbSet<doctor> doctor { get; set; }
        public virtual DbSet<note> note { get; set; }
        public virtual DbSet<pacient> pacient { get; set; }
        public virtual DbSet<pacient_doctor> pacient_doctor { get; set; }
        public virtual DbSet<reseption> reseption { get; set; }

        public iToothServerContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Database=iToothServ;Trusted_Connection=True;");
        }
    }
}
