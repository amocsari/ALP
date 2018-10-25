using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entity;

namespace DAL.Context
{
    public interface IAlpContext
    {
        IQueryable<T> Set<T>() where T : class;

        void SaveChanges();
        Task SaveChangesAsync();

        void Add<T>(T entity) where T : class;
        Task AddAsync<T>(T entity) where T : class;

        void Remove<T>(T entity) where T : class;
        Task RemoveAsync<T>(T entity) where T : class;

        Task<T> Update<T>(T entity) where T : class;

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
