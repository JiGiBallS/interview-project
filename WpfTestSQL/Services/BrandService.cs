using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WpfTestSQL.ViewModels;

namespace WpfTestSQL.Services
{
    /// <summary>
    /// CRUD
    /// </summary>
    public class BrandService : ICarService
    {
        public async Task Create(Brand brand, ApplicationContext db)
        {
            db.Brands.Add(brand);
            await db.SaveChangesAsync();
        }

        public async Task Update(Brand brand, ApplicationContext db)
        {
            db.Entry(brand).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task<Brand> FindById(int Id, ApplicationContext db)
        {
            return await db.Brands.FindAsync(Id);
        }
        public IEnumerable<Brand> GetAll(ApplicationContext db)
        {
            db.Brands.Load();
            return db.Brands.Local.ToBindingList();
        }

        public async Task Delete(Brand brand, ApplicationContext db)
        {
            db.Brands.Remove(brand);
            await db.SaveChangesAsync();
        }
    }
}