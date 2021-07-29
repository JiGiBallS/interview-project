using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfTestSQL.Services;

namespace WpfTestSQL.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        ApplicationContext db;
        RelayCommand addCommand;
        RelayCommand addCarCommand;
        RelayCommand editCommand;
        RelayCommand editCarCommand;
        RelayCommand deleteCommand;
        RelayCommand deleteCarCommand;
        RelayCommand showAllCarCommand;
        IEnumerable<Brand> brands;
        IEnumerable<Car> cars;

        private Brand selectedPhone;
        private Car selectedCar;
        //поиск по брендам
        private string _myText;
        //поиск по авто
        private string _myText2;
        private readonly CarService _carService;
        private readonly BrandService _brandService;

        public string MyText
        {
            get { return _myText; }
            set
            {
                _myText = value;
                Brands = Brands.Where(n => n.Id.ToString().StartsWith(_myText) || n.Brand_name.ToString().StartsWith(_myText));
            }
        }
        public string MyText2
        {
            get { return _myText2; }
            set
            {
                _myText2 = value;
                Cars = Cars.Where(n => n.Id.ToString().StartsWith(_myText2) || n.Car_name.ToString().StartsWith(_myText2));
            }
        }
        public Brand SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                if (selectedPhone != null)
                {
                    var suitableCars = Cars.Where(n => n.Car_brand_id == selectedPhone.Id);
                    if (suitableCars.Count() == 0)
                    {
                        MessageBox.Show("Машин данного бренда нет!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        db.Cars.Load();
                        Cars = db.Cars.Local.ToBindingList();
                    }
                    else
                    {
                        Cars = suitableCars;
                    }
                }
                OnPropertyChanged("SelectedPhone");
            }
        }

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }

        public IEnumerable<Brand> Brands
        {
            get { return brands; }
            set
            {
                brands = value;
                OnPropertyChanged("Brands");
            }
        }

        public IEnumerable<Car> Cars
        {
            get { return cars; }
            set
            {
                cars = value;
                OnPropertyChanged("Cars");
            }
        }

        public ApplicationViewModel(CarService carService, BrandService brandService)
        {
            _carService = carService;
            _brandService = brandService;

            db = new ApplicationContext();

            Brands = _brandService.GetAll(db);
            Cars = _carService.GetAll(db);
            this._carService = carService;
            this._brandService = brandService;

        }

        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async (o) =>
                  {
                      BrandWindow brandWindow = new BrandWindow(new Brand());
                      if (brandWindow.ShowDialog() == true)
                      {
                          Brand brand = brandWindow.Brand;
                          await _brandService.Create(brand, db);
                      }
                  }));
            }
        }

        public RelayCommand ShowAllCarCommand
        {
            get
            {
                return showAllCarCommand ??
                  (showAllCarCommand = new RelayCommand((o) =>
                  {
                      Cars = _carService.GetAll(db);
                  }));
            }
        }

        public RelayCommand AddCarCommand
        {
            get
            {
                return addCarCommand ??
                  (addCarCommand = new RelayCommand(async (o) =>
                  {
                      CarWindow carWindow = new CarWindow(new Car());
                      if (carWindow.ShowDialog() == true)
                      {
                          Car car = carWindow.Car;
                          car.Car_name = carWindow.Car.Car_name;
                          car.Car_price = carWindow.Car.Car_price;
                          car.Car_fuel_type = carWindow.Car.Car_fuel_type.Replace("System.Windows.Controls.ComboBoxItem: ", "");
                          car.Car_brand_id = carWindow.Car.Car_brand_id;
                          car.Car_capacity = carWindow.Car.Car_capacity;
                          car.Car_class = carWindow.Car.Car_class.Replace("System.Windows.Controls.ComboBoxItem: ", "");
                          car.Car_date_of_appearance = carWindow.Car.Car_date_of_appearance;
                          car.Car_in_stock = carWindow.Car.Car_in_stock.Replace("System.Windows.Controls.ComboBoxItem: ", "");
                          await _carService.Create(car, db);
                      }
                  }));
            }
        }
        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Brand phone = selectedItem as Brand;

                      Brand vm = new Brand()
                      {
                          Id = phone.Id,
                          Brand_description = phone.Brand_description,
                          Brand_name = phone.Brand_name,
                          Brand_logo = phone.Brand_logo
                      };
                      BrandWindow phoneWindow = new BrandWindow(vm);

                      if (phoneWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          phone = db.Brands.Find(phoneWindow.Brand.Id);
                          if (phone != null)
                          {
                              phone.Brand_description = phoneWindow.Brand.Brand_description;
                              phone.Brand_name = phoneWindow.Brand.Brand_name;
                              phone.Brand_logo = phoneWindow.Brand.Brand_logo;

                              db.Entry(phone).State = EntityState.Modified;
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }

        public RelayCommand EditCarCommand
        {
            get
            {
                return editCarCommand ??
                  (editCarCommand = new RelayCommand(async (selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Car car = selectedItem as Car;

                      Car vm = new Car()
                      {
                          Id = car.Id,
                          Car_name = car.Car_name,
                          Car_price = car.Car_price,
                          Car_fuel_type = car.Car_fuel_type,
                          Car_brand_id = car.Car_brand_id,
                          Car_capacity = car.Car_capacity,
                          Car_class = car.Car_class,
                          Car_date_of_appearance = car.Car_date_of_appearance,
                          Car_in_stock = car.Car_in_stock
                      };
                      CarWindow carWindow = new CarWindow(vm);


                      if (carWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          car = await _carService.FindById(carWindow.Car.Id, db);
                          if (car != null)
                          {
                              car.Car_name = carWindow.Car.Car_name;
                              car.Car_price = carWindow.Car.Car_price;
                              car.Car_fuel_type = carWindow.Car.Car_fuel_type.Replace("System.Windows.Controls.ComboBoxItem: ", "");
                              car.Car_brand_id = carWindow.Car.Car_brand_id;
                              car.Car_capacity = carWindow.Car.Car_capacity; 
                              car.Car_class = carWindow.Car.Car_class.Replace("System.Windows.Controls.ComboBoxItem: ", "");
                              car.Car_date_of_appearance = carWindow.Car.Car_date_of_appearance;
                              car.Car_in_stock = carWindow.Car.Car_in_stock.Replace("System.Windows.Controls.ComboBoxItem: ", "");
                              await _carService.Update(car, db);
                          }
                      }
                  }));
            }
        }
        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(async(selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      Brand brand = selectedItem as Brand;
                      await _brandService.Delete(brand, db);
                  }));
            }
        }
        // команда удаления авто
        public RelayCommand DeleteCarCommand
        {
            get
            {
                return deleteCarCommand ??
                  (deleteCarCommand = new RelayCommand(async (selectedItem) =>
                  {
                      if (selectedItem == null) return;

                      Car car = selectedItem as Car;
                      await _carService.Delete(car, db);
                  }));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public CarService Delete2CarCommand
        {
            get
            {
                return new CarService();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
