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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Information_system__Countries_of_Europe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Country> countries = new List<Country>();
        public List<Sight> sights = new List<Sight>();
        public MainWindow()
        {
            InitializeComponent();
            
           /* using (FileStream fs = new FileStream(@"../../Countries.txt", FileMode.Open, FileAccess.Read))
            {
                string[] data;
                Country cou;
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                while (!sr.EndOfStream)
                {
                    string NameCountry = sr.ReadLine();
                    string Capital = sr.ReadLine();
                    string PopCapital = sr.ReadLine();
                    string[] SightCap = sr.ReadLine().Split(';');
                    string NameSightCap = SightCap[0];
                    int YearSightCap = int.Parse(SightCap[1]);
                    string InfSightCap = SightCap[2];

                }
                
                sr.Close();
                fs.Close();
            }*/


        }

        private void Country_Click(object sender, RoutedEventArgs e)
        {
            Country_Description wnd = new Country_Description(this);
            wnd.Show();
        }

        private void SHOW_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            CountryPage.Visibility = Visibility.Visible;
        }

        private void Spain_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
