using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WpfTestSQL.ViewModels;

namespace WpfTestSQL.Services
{
    /// <summary>
    /// CRUD
    /// </summary>
    public class CarService : ICarService
    {
        public async Task Create(Car car, ApplicationContext db)
        {
            db.Cars.Add(car);
            await db.SaveChangesAsync();
        }

        public async Task Update(Car car, ApplicationContext db)
        {
            db.Entry(car).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
        public async Task<Car> FindById(int Id, ApplicationContext db)
        {
            return await db.Cars.FindAsync(Id);
        }
        public IEnumerable<Car> GetAll(ApplicationContext db)
        {
            db.Cars.Load();
            return db.Cars.Local.ToBindingList();
        }

        public async Task Delete(Car car, ApplicationContext db)
        {
            db.Cars.Remove(car);
            await db.SaveChangesAsync();
        }
    }
}