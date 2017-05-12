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
using System.Drawing;

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
            
            using (FileStream fs = new FileStream(@"../../../Countries.txt", FileMode.Open, FileAccess.Read))
            {
                string[] data;
                
                StreamReader sr = new StreamReader(fs, Encoding.Default);

                while (!sr.EndOfStream)
                {
                    string NameCountry = sr.ReadLine();
                    string NameCountryE = sr.ReadLine();
                    string Capital = sr.ReadLine();
                    string Language = sr.ReadLine();
                    int Square = int.Parse(sr.ReadLine());
                    int Pop = int.Parse(sr.ReadLine());
                    string Flag = sr.ReadLine();
                    while (true)
                    {
                        string[] SightC = sr.ReadLine().Split('@');
                        string NameSight = SightC[0];

                        if (NameSight == "")
                            break;
                        
                        int YearSight = int.Parse(SightC[1]);
                        string CitySight = SightC[2];
                        string InfSight = SightC[3];

                        sights.Add(new Sight(NameSight, YearSight,InfSight,CitySight,NameCountry, NameCountryE));
                        if (sr.EndOfStream)
                            break;
                    }

                    countries.Add(new Country(NameCountry,NameCountryE, Capital, Language, Square, Pop, Flag));
                  }
                sr.Close();
                fs.Close();
            }

            foreach (var item in countries)
            {
                AllCou.Items.Add(item.ShowName());
            }


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


        private void Sights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void AllCou_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (var cou in countries)
            {
                if (AllCou.SelectedItem.Equals(cou.Name))
                {
                    CountryName.Text = cou.Name;
                    Capital.Text = cou.Capital;
                    Language.Text = cou.Language;
                    /*Flag.Source = item.Flag;*/
                    Square.Text = cou.Square.ToString();
                    Population.Text = cou.Population.ToString();

                    Sights.Items.Clear();
                    foreach (var s in sights)
                    {
                        if (cou.Name == s.Country)
                        {
                            Sights.Items.Add(s.Show());
                        }
                       
                    }
                }
                
             }


            
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in countries)
            {
                if (SearchBox.Text == item.Name)
                {
                    AllCou.Items.Clear();
                    AllCou.Items.Add(item.Name);
                }
                else
                {
                    AllCou.Items.Clear();
                    foreach (var c in countries)
                    {
                        AllCou.Items.Add(c.Name);
                    }
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var item in countries)
            {
                if (SearchBox.Text == item.Name)
                {
                    AllCou.Items.Clear();
                    AllCou.Items.Add(item.Name);

                }
                else
                {
                    AllCou.Items.Clear();
                    foreach (var c in countries)
                    {

                        AllCou.Items.Add(c.Name);
                    }
                }
            }
        }



        private void Spain_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in countries)
            {
                if (Spain.Name == item.NameEng)
                {
                    CountryName.Text = item.Name;
                    Capital.Text = item.Capital;
                    Language.Text = item.Language;
                    /* Flag.Source = item.Flag;*/
                    Square.Text = item.Square.ToString();
                    Population.Text = item.Population.ToString();

                    Sights.Items.Clear();
                    foreach (var s in sights)
                    {
                        if (Spain.Name == s.CountryE)
                        {
                            Sights.Items.Add(s.Show());
                        }
                        else break;
                    }
                }
                else break;
            }

        }









    }
}
