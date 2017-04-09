using System;
using System.Collections.Generic;
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

namespace Information_system__Countries_of_Europe
{
    /// <summary>
    /// Логика взаимодействия для Country_Description.xaml
    /// </summary>
    public partial class Country_Description : Window
    {
        MainWindow wnd;
        public Country_Description(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Все хорошо!");
        }

       
    }
}
