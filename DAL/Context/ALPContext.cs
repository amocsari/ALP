using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ALPContext : DbContext, IAlpContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Building> Building { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemNature> ItemNature { get; set; }
        public DbSet<ItemState> ItemState { get; set; }
        public DbSet<ItemType> ItemType { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<OperationType> OperationType { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Section> Section { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ALP;Trusted_Connection=True;");
        }

        void IAlpContext.SaveChanges()
        {
            base.SaveChanges();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}
    }
}
