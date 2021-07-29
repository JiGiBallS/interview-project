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
    public class Brand : INotifyPropertyChanged
    {
        
        private string brand_name;
        private string brand_description;
        private byte[] brand_logo;
        [Key]
        public int Id { get; set; }
       

        public string Brand_name
        {
            get { return brand_name; }
            set
            {
                brand_name = value;
                OnPropertyChanged("Brand_name");
            }
        }
        public string Brand_description
        {
            get { return brand_description; }
            set
            {
                brand_description = value;
                OnPropertyChanged("Brand_description");
            }
        }
        public byte[] Brand_logo
        {
            get { return brand_logo; }
            set
            {
                brand_logo = value;
                OnPropertyChanged("Brand_logo");
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
