using System.Threading.Tasks;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public partial class ALPContext
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

        DbSet<T> IAlpContext.Set<T>()
        {
            return base.Set<T>();
        }

        void IAlpContext.Add<T>(T entity)
        {
            base.Add(entity);
            base.SaveChanges();
        }

        async Task IAlpContext.AddAsync<T>(T entity)
        {
            await base.AddAsync(entity);
            await base.SaveChangesAsync();
        }

        void IAlpContext.Remove<T>(T entity)
        {
            base.Remove(entity);
            base.SaveChanges();
        }

        async Task IAlpContext.RemoveAsync<T>(T entity)
        {
            base.Remove(entity);
            await base.SaveChangesAsync();
        }

        void IAlpContext.SaveChanges()
        {
            base.SaveChanges();
        }

        async Task IAlpContext.SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }

        async Task<T> IAlpContext.Update<T>(T newEntity)
        {
            var entity = await Set<T>().FirstOrDefaultAsync();
            entity = newEntity;
            await SaveChangesAsync();
            return entity;
        }
    }
}
