using Microsoft.EntityFrameworkCore;
using DAL.Entity;

namespace DAL.Context
{
    public interface IAlpContext
    {
        void SaveChanges();

        DbSet<Account> Account { get; set; }
        DbSet<Building> Building { get; set; }
        DbSet<Department> Department { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Floor> Floor { get; set; }
        DbSet<Item> Item { get; set; }
        DbSet<ItemNature> ItemNature { get; set; }
        DbSet<ItemState> ItemState { get; set; }
        DbSet<ItemType> ItemType { get; set; }
        DbSet<Location> Location { get; set; }
        DbSet<Operation> Operation { get; set; }
        DbSet<OperationType> OperationType { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<Section> Section { get; set; }
    }
}
