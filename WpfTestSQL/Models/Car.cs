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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
