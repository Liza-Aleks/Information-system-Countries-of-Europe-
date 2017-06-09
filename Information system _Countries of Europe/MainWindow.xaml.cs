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
using System.Xml.Serialization;
using System.Drawing;
using static System.Security.Permissions.FileIOPermission;
using Microsoft.Win32;

namespace Information_system__Countries_of_Europe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {


        public ListCountries countries = new ListCountries();

        public List<Sight> sights = new List<Sight>();

        

        public MainWindow()
        {
            InitializeComponent();
            try
            {

                countries.ListCou = new List<Country>();
                Log.L("Вход в режим пользователя; " + DateTime.Now);

                if (File.Exists("../../allcountries.xml"))
                {
                    countries = Ser.DeSerialize(countries);
                    Sort.C(countries.ListCou);
                    foreach (var item in countries.ListCou)
                    {
                        AllCou.Items.Add(item.Name);

                        foreach (var s in item.LSight)
                        {
                            Sort.S(item.LSight);
                            sights.Add(s);
                        }
                    }

                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*ПОКАЗАТЬ АДМИН*/
        private void SHOW_Click(object sender, RoutedEventArgs e)
        {
            Password wnd = new Password(this);
            wnd.Show();
            

        }

        /*CСЫЛКИ НА СТРАНИЦЫ */
        private void CountryPage_Click(object sender, RoutedEventArgs e)
        {
            try {
                Main.Content = new CountryAd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }
         private void SightPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Main.Content = new SightAd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*НА СТРАНИЧКУ ДЛЯ ПОЛЬЗОВАТЕЛЯ */
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainPage.Visibility = Visibility.Visible;
                Admin.Visibility = Visibility.Hidden;
                AdditionalInf.Visibility = Visibility.Visible;
                AllCou.Items.Clear();
                sights.Clear();

                if (File.Exists("../../allcountries.xml"))
                {
                    countries = Ser.DeSerialize(countries);
                    foreach (var item in countries.ListCou)
                    {
                        AllCou.Items.Add(item.Name);

                        foreach (var s in item.LSight)
                        {
                            Sort.S(item.LSight);
                            sights.Add(s);
                        }
                    }
                }
               
                Log.L("Вход в режим пользователя; " + DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }

        }


        /*ВЫБОР ЭЛЕМЕНТА В ЛИСТЕ С ДОСТОПРИМЕЧАТЕЛЬНОСТЯМИ*/
        private void Sights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                if (Sights.SelectedItem != null)
                {
                    Sight_Inform wnd = new Sight_Inform(this);
                    wnd.Show();

                    foreach (var item in sights)
                    {
                        if (Sights.SelectedItem.ToString() == item.Show())
                        {
                            wnd.SightIm.Source = new BitmapImage(new Uri(item.SightIm));
                            wnd.Name.Text = item.Name;
                            wnd.Year.Text = item.Year;
                            wnd.City.Text = item.City;
                            wnd.Inf.Text = item.Information;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*ВЫБОР ЭЛЕМЕНТА В ЛИСТЕ СО СТРАНАМИ*/
        private void AllCou_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                foreach (var cou in countries.ListCou)
                {
                    if (AllCou.SelectedItem.Equals(cou.Name))
                    {
                        CountryName.Text = cou.Name;
                        Capital.Text = cou.Capital;
                        Language.Text = cou.Language;
                        Flag.Source = new BitmapImage(new Uri(cou.Flag));
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }


        /*ПОИСК ПО ЛИСТБОКСУ ALLCOU*/
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                List<Country> c = new List<Country>();

                if (SearchBox.Text != null)
                {
                    AllCou.Items.Clear();
                    foreach (var item in countries.ListCou)
                    {
                        if (item.Name.Contains(SearchBox.Text))
                            if (!c.Contains(item))
                            {
                                c.Add(item);
                            }
                            else
                            {
                                c.Clear();
                                foreach (var cou in countries.ListCou)
                                {
                                    c.Add(item);
                                }
                            }
                    }
                    foreach (var item in c)
                    {
                        AllCou.Items.Add(item.ShowName());
                    }
                }
               else
                { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }




        /*КНОПКИ ПО СТРАНАМ НА КАРТЕ */

        private void Country_MouseUp(object sender, RoutedEventArgs e)
        {
            try
            {
                StackPanel sp = (StackPanel)sender;
               var inf = (TextBlock)sp.Children[0];
           
                    Sights.Items.Clear();
                    foreach (var item in countries.ListCou)
                    {
                        if (inf.Text == item.Name)
                        {
                            CountryName.Text = item.Name;
                            Capital.Text = item.Capital;
                            Language.Text = item.Language;
                            Flag.Source = new BitmapImage(new Uri(item.Flag));
                            Square.Text = item.Square.ToString();
                            Population.Text = item.Population.ToString();

                            foreach (var s in item.LSight)
                            {
                                if (item.Name == s.Country)
                                {
                                    Sights.Items.Add(s.Show());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }

        }




        }
}
