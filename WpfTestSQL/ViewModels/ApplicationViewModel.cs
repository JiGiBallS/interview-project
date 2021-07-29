using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        IEnumerable<Brand> phones;
        IEnumerable<Car> cars;

        private Brand selectedPhone;
        private Car selectedCar;
        //поиск по брендам
        private string _myText;
        //поиск по авто
        private string _myText2;
        private readonly CarService _carService;

        public string MyText
        {
            get { return _myText; }
            set
            {
                _myText = value;
                Phones = Phones.Where(n => n.Brand_id.ToString().StartsWith(_myText) ||  n.Brand_name.ToString().StartsWith(_myText));
            }
        }
        public string MyText2
        {
            get { return _myText2; }
            set
            {
                _myText2 = value;
                Cars = Cars.Where(n => n.Car_id.ToString().StartsWith(_myText2) || n.Car_name.ToString().StartsWith(_myText2));
            }
        }
        public Brand SelectedPhone
        {
            get { return selectedPhone; }
            set
            {
                selectedPhone = value;
                //var suitableCars = Cars.Where(n => n.Car_brand_id == selectedPhone.Brand_id);
                //if (suitableCars.Count() == 0)
                //{
                //    MessageBox.Show("Машин данного бренда нет!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                //    db.Cars.Load();
                //    Cars = db.Cars.Local.ToBindingList();
                //}
                //else
                //{
                //    Cars = suitableCars;
                //}
                
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

        public IEnumerable<Brand> Phones
        {
            get { return phones; }
            set
            {
                phones = value;
                OnPropertyChanged("Brand");
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

        public ApplicationViewModel(CarService carService)
        {
            _carService = carService;
           
            db = new ApplicationContext();

            db.Brand.Load();
            Phones = db.Brand.Local.ToBindingList();
            
            db.Cars.Load();
            Cars = db.Cars.Local.ToBindingList();
            this._carService = carService;
            
        }

        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async (o) =>
                  {
                      PhoneWindow phoneWindow = new PhoneWindow(new Brand());
                      if (phoneWindow.ShowDialog() == true)
                      {
                          Brand phone = phoneWindow.Brand;
                          await _carService.Create(phone,db);

                      }
                  }));
            }
        }

        public RelayCommand AddCarCommand
        {
            get
            {
                return addCarCommand ??
                  (addCarCommand = new RelayCommand((o) =>
                  {
                      CarWindow carWindow = new CarWindow(new Car());
                      if (carWindow.ShowDialog() == true)
                      {
                          Car car = carWindow.Car;
                          db.Cars.Add(car);
                          db.SaveChanges();
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
                          Brand_id = phone.Brand_id,
                          Brand_description = phone.Brand_description,
                          Brand_name = phone.Brand_name,
                          Brand_logo = phone.Brand_logo
                      };
                      PhoneWindow phoneWindow = new PhoneWindow(vm);

                      if (phoneWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          phone = db.Brand.Find(phoneWindow.Brand.Brand_id);
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
                  (editCarCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Car car = selectedItem as Car;

                      Car vm = new Car()
                      {
                          Car_id = car.Car_id,
                          Car_name = car.Car_name,
                          Car_price = car.Car_price,
                          Car_fuel_type = car.Car_fuel_type,
                          Car_brand_id = car.Car_brand_id,
                      };
                      CarWindow carWindow = new CarWindow(vm);


                      if (carWindow.ShowDialog() == true)
                      {
                          // получаем измененный объект
                          car = db.Cars.Find(carWindow.Car.Car_id);
                          if (car != null)
                          {
                              car.Car_name = carWindow.Car.Car_name;
                              car.Car_price = carWindow.Car.Car_price;
                              car.Car_fuel_type = carWindow.Car.Car_fuel_type;
                              car.Car_brand_id = carWindow.Car.Car_brand_id;
                              db.Entry(car).State = EntityState.Modified;
                              db.SaveChanges();
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
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Brand phone = selectedItem as Brand;
                      db.Brand.Remove(phone);
                      db.SaveChanges();
                  }));
            }
        }
        // команда удаления авто
        public RelayCommand DeleteCarCommand
        {
            get
            {
                return deleteCarCommand ??
                  (deleteCarCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      Car car = selectedItem as Car;
                      db.Cars.Remove(car);
                      db.SaveChanges();
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
