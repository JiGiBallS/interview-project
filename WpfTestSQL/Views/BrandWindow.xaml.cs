using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfTestSQL.ViewModels;

namespace WpfTestSQL
{
    /// <summary>
    /// Логика взаимодействия для BrandWindow.xaml
    /// </summary>
    public partial class BrandWindow : Window
    {
        public Brand Brand { get; private set; }
        public BrandWindow(Brand p)
        {
            InitializeComponent();
            Brand = p;
            this.DataContext = Brand;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void logoDownload_Click(object sender, RoutedEventArgs e)
        {
            string d = "";
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                d = dlg.FileName;
            }

            byte[] imageData;
            using (FileStream fs = new FileStream(d, FileMode.Open))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, imageData.Length);
            }
            Brand.Brand_logo = imageData;
        }
    }
}
