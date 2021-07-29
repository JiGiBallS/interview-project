using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestSQL
{
    public class Car : INotifyPropertyChanged
    {
        private string car_name;
        private string car_fuel_type;
        private int car_price; 
        private int car_brand_id;
        private string car_date_of_appearance;
        private string car_class;
        private int car_capacity;
        private string car_in_stock;
        [Key]
        public int Id { get; set; }

        public string Car_name
        {
            get { return car_name; }
            set
            {
                car_name = value;
                OnPropertyChanged("Car_name");
            }
        }
        public string Car_fuel_type
        {
            get { return car_fuel_type; }
            set
            {
                car_fuel_type = value;
                OnPropertyChanged("Car_fuel_type");
            }
        }
        public int Car_price
        {
            get { return car_price; }
            set
            {
                car_price = value;
                OnPropertyChanged("Car_price");
            }
        }
        public int Car_brand_id
        {
            get { return car_brand_id; }
            set
            {
                car_brand_id = value;
                OnPropertyChanged("Car_brand_id");
            }
        }
        public string Car_date_of_appearance
        {
            get { return car_date_of_appearance; }
            set
            {
                car_date_of_appearance = value;
                OnPropertyChanged("Car_date_of_appearance");
            }
        }
        public string Car_class
        {
            get { return car_class; }
            set
            {
                car_class = value;
                OnPropertyChanged("Car_class");
            }
        }
        public int Car_capacity
        {
            get { return car_capacity; }
            set
            {
                car_capacity = value;
                OnPropertyChanged("Car_capacity");
            }
        }
        public string Car_in_stock
        {
            get { return car_in_stock; }
            set
            {
                car_in_stock = value;
                OnPropertyChanged("Car_in_stock");
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
