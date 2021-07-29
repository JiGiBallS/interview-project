using Microsoft.Extensions.DependencyInjection;
using WpfTestSQL.Services;
using WpfTestSQL.ViewModels;

namespace WpfTestSQL
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();
            services.AddScoped<ApplicationViewModel>();
            services.AddScoped<CarService>();
            services.AddScoped<BrandService>();
            _provider = services.BuildServiceProvider();
        }

        public ApplicationViewModel ApplicationViewModel => _provider.GetRequiredService<ApplicationViewModel>();
    }
}