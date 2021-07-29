using System.Threading.Tasks;
using WpfTestSQL.ViewModels;

namespace WpfTestSQL
{
    /// <summary>
    /// CRUD
    /// </summary>
    public class CarService : ICarService
    {
        public async Task Create(Brand phone, ApplicationContext db)
        {
            db.Brands.Add(phone);
            await db.SaveChangesAsync();
        }
    }
}