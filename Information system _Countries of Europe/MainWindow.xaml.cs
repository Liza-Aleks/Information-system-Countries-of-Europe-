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


        public void Log(string log)
        {
            using (FileStream fs = new FileStream("../../log.txt", FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(log);

                sw.Close();
                fs.Close();
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            countries.ListCou = new List<Country>();

            if (File.Exists("../../allcountries.xml"))
            {
                countries = Ser.DeSerialize(countries);
                foreach (var item in countries.ListCou)
                {
                    AllCou.Items.Add(item.Name);
                    AllCouAd.Items.Add(item.Name);
                    foreach (var s in item.LSight)
                    {
                        sights.Add(s);
                    }
                }
            }
        }


        private void SHOW_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            Admin.Visibility = Visibility.Visible;
        }


        private void Sights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sight_Inform wnd = new Sight_Inform(this);
            wnd.Show();
            foreach (var item in sights)
            {
                if(Sights.SelectedItem.ToString() == item.Show())
                {
                    wnd.SightIm.Source = new BitmapImage(new Uri(item.SightIm));
                    wnd.Name.Text = item.Name;
                    wnd.Year.Text = item.Year;
                    wnd.City.Text = item.City;
                    wnd.Inf.Text = item.Information;
                }
            }
            
        }

        private void AllCou_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in countries.ListCou)
            {
                if (SearchBox.Text == item.Name)
                {
                    AllCou.Items.Clear();
                    AllCou.Items.Add(item.Name);
                }
                else
                {
                    AllCou.Items.Clear();
                    foreach (var c in countries.ListCou)
                    {
                        AllCou.Items.Add(c.Name);
                    }
                }

            }
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }



        private void Spain_Click(object sender, RoutedEventArgs e)
        {



            foreach (var item in countries.ListCou)
            {
                if (Spain.Name == item.NameEng)
                {
                    CountryName.Text = item.Name;
                    Capital.Text = item.Capital;
                    Language.Text = item.Language;
                    Flag.Source = new BitmapImage(new Uri(item.Flag));
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



        /*СТРАНИЧКА ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN*/

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Visible;
            Admin.Visibility = Visibility.Hidden;


        }


        /*При нажатии элемента из ListBox в admin  */
        private void AllCouAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddCountrySP.Visibility = Visibility.Hidden;
            AddDeleteCountrySP.Visibility = Visibility.Visible;
            CountryRedAd.Visibility = Visibility.Visible;
            EditCountryAd.Visibility = Visibility.Visible;
            EditSightAd.Visibility = Visibility.Visible;
            SaveCountryAd.Visibility = Visibility.Hidden;
            EditSightListAd.Items.Clear();
            EdNameSightAd.Text = "";
            EdYearSightAd.Text = "";
            EdCitySightAd.Text = "";
            EdInfSightAd.Text = "";


            foreach (var item in countries.ListCou)
            {
                if (AllCouAd.SelectedItem != null)
                {
                    if (AllCouAd.SelectedItem.ToString() == item.Name)
                    {
                        CountryAd.Text = item.Name;
                        CountryEngAd.Text = item.NameEng;
                        CapitalAd.Text = item.Capital;
                        LangAd.Text = item.Language;
                        SquareAd.Text = item.Square.ToString();
                        PopAd.Text = item.Population.ToString();
                        FlagAd.Text = item.Flag;
                        foreach (var s in item.LSight)
                        {
                            EditSightListAd.Items.Add(s.Show());
                        }
                    }
                }
            }




        }

        /*ДОБАВИТЬ СТРАНУ*/
        private void AddCountryAd_Click(object sender, RoutedEventArgs e)
        {
            CountryRedAd.Visibility = Visibility.Visible;
            SaveCountryAd.Visibility = Visibility.Visible;
            EditCountryAd.Visibility = Visibility.Hidden;
            EditSightAd.Visibility = Visibility.Hidden;

            CountryAd.Text = "";
            CountryEngAd.Text = "";
            CapitalAd.Text = "";
            LangAd.Text = "";
            SquareAd.Text = "";
            PopAd.Text = "";
            FlagAd.Text = "";

        }

        /*ДОБАВИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ*/
        private void AddSightAd_Click(object sender, RoutedEventArgs e)
        {
            SightAd.Visibility = Visibility.Visible;
            EditSightAd.Visibility = Visibility.Hidden;
            NameSightAd.Text = "";
            YearSightAd.Text = "";
            InfSightAd.Text = "";
            CitySightAd.Text = "";

        }

        /*ЗАГРУЗИТЬ ФОТО*/
        public void DownloadImage_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpeg|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                var fileN = openFileDialog.FileName;
                var PathNew = System.IO.Path.GetFileName(fileN);
                var Pathnow = System.Reflection.Assembly.GetExecutingAssembly().Location;
                PathNew = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Pathnow) + "/imgFlag/", PathNew);

                File.Copy(fileN, PathNew, true);
                BitmapImage img;
                img = new BitmapImage(new Uri(PathNew));
                FlagAd.Text = PathNew;

            }

        }

        public void SightImDownAd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "jpeg|*.jpg";
            if (openFileDialog.ShowDialog() == true)
            {
                var fileN = openFileDialog.FileName;
                var PathNew = System.IO.Path.GetFileName(fileN);
                var Pathnow = System.Reflection.Assembly.GetExecutingAssembly().Location;
                PathNew = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Pathnow) + "/imgSight/", PathNew);

                File.Copy(fileN, PathNew, true);
                BitmapImage img;
                img = new BitmapImage(new Uri(PathNew));
                SightImAd.Text = PathNew;

            }
        }

        /*СОХРАНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ*/
        private void SaveSightAd_Click(object sender, RoutedEventArgs e)
        {
            SightAd.Visibility = Visibility.Hidden;

            foreach (var item in countries.ListCou)
            {
                if(CountryAd.Text == item.Name)
                EditSightAd.Visibility = Visibility.Visible;
            }
           

            Sight si = new Sight(NameSightAd.Text, YearSightAd.Text, InfSightAd.Text, CitySightAd.Text, CountryAd.Text, CountryEngAd.Text, SightImAd.Text);
            sights.Add(si);
            foreach (var item in countries.ListCou)
            {
                if (item.Name == si.Country)
                {
                    item.LSight.Add(si);
                }
            }

            EditSightListAd.Items.Clear();

            foreach (var item in sights)
            {
                if (AllCouAd.SelectedItem != null)
                {
                    if (AllCouAd.SelectedItem.ToString() == item.Country)
                    {
                        EditSightListAd.Items.Add(item.Show());
                    }
                }
            }

            Ser.Serialize(countries);


        }

        /*СОХРАНИТЬ СТРАНУ*/
        private void button_Click(object sender, RoutedEventArgs e)
        {
            SaveCountryAd.Visibility = Visibility.Hidden;
            EditCountryAd.Visibility = Visibility.Visible;

            EditSightAd.Visibility = Visibility.Visible;

            List<Sight> sightscou = new List<Sight>();
            foreach (var item in sights)
            {
                if (CountryAd.Text == item.Country)
                {
                    sightscou.Add(item);
                }
            }
            Country cou = new Country(CountryAd.Text, CountryEngAd.Text, CapitalAd.Text, LangAd.Text, int.Parse(SquareAd.Text), int.Parse(PopAd.Text), FlagAd.Text, sightscou);

            countries.ListCou.Add(cou);

            AllCouAd.Items.Add(cou.Name);
            AllCou.Items.Add(cou.Name);

            Ser.Serialize(countries);

          

        }

        /*УДАЛИТЬ СТРАНУ */
        private void DeleteCountryAd_Click(object sender, RoutedEventArgs e)
        {
            if (AllCouAd.SelectedItem != null)
            {
                foreach (var item in countries.ListCou)
                {
                    if (AllCouAd.SelectedItem.ToString() == item.Name)
                    {
                        int i = countries.ListCou.IndexOf(item);
                        countries.ListCou.RemoveAt(i);
                        break;
                    }
                }
                AllCouAd.Items.Clear();
                AllCou.Items.Clear();
                foreach (var item in countries.ListCou)
                {
                    AllCouAd.Items.Add(item.Name);
                    AllCou.Items.Add(item.Name);
                }

                Ser.Serialize(countries);


                CountryAd.Text = "";
                CountryEngAd.Text = "";
                CapitalAd.Text = "";
                LangAd.Text = "";
                SquareAd.Text = "";
                PopAd.Text = "";
                FlagAd.Text = "";
            }
            else
                MessageBox.Show("Выберите страну", "", MessageBoxButton.OK, MessageBoxImage.Warning);
      }

        /*УДАЛИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ*/
        private void DeleteSightAd_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in sights)
            {
                if (EditSightListAd.SelectedItem.ToString() == item.Show())
                {
                    int i = sights.IndexOf(item);
                    sights.RemoveAt(i);
                    break;
                }
            }

            foreach (var item in countries.ListCou)
            {
               foreach (var ls in item.LSight)
                {
                    if( EditSightListAd.SelectedItem.ToString() == ls.Show())
                    {
                        int i = item.LSight.IndexOf(ls);
                        item.LSight.RemoveAt(i);
                        break;
                    }

                 }
             }

            EditSightListAd.Items.Clear();

            foreach (var item in sights)
            {
                if (CountryAd.Text == item.Country)
                    EditSightListAd.Items.Add(item.Show());
            }


            Ser.Serialize(countries);

            EdNameSightAd.Text = "";
            EdYearSightAd.Text = "";
            EdCitySightAd.Text = "";
            EdInfSightAd.Text = "";

        }

        /*ИЗМЕНИТЬ СТРАНУ */
        private void EditCountryAd_Click(object sender, RoutedEventArgs e)
        {
            EditSightAd.Visibility = Visibility.Visible;

            foreach (var item in countries.ListCou)
            {    if(AllCouAd.SelectedItem != null)
                if(AllCouAd.SelectedItem.ToString() == item.Name)
                {
                    string s = item.Name;
                    item.Name = CountryAd.Text;
                    item.NameEng = CountryEngAd.Text;
                    item.Capital = CapitalAd.Text;
                    item.Language = LangAd.Text;
                    item.Population = int.Parse(PopAd.Text);
                    item.Square = int.Parse(SquareAd.Text);
                    item.Flag = FlagAd.Text;
                    foreach (var ls in item.LSight)
                    {
                        ls.Country= CountryAd.Text;
                        ls.CountryE=CountryEngAd.Text;
                    }

                }
            }
            AllCouAd.Items.Clear();
            AllCou.Items.Clear();
            foreach (var item in countries.ListCou)
            {
                AllCouAd.Items.Add(item.Name);
                AllCou.Items.Add(item.Name);
              
            }
            Ser.Serialize(countries);

            EditSightListAd.Items.Clear();
            foreach (var item in sights)
            {
                if (CountryAd.Text == item.Country)
                    EditSightListAd.Items.Add(item.Show());
            }
     
            MessageBox.Show("Изменения успешно сохранены");
        }

        /*ИЗМЕНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void EdSaveSightAd_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in sights)
            {
                if (EditSightListAd.SelectedItem != null)
                {
                    if (EditSightListAd.SelectedItem.ToString() == item.Show())
                    {
                        item.Name = EdNameSightAd.Text;
                        item.Year = EdYearSightAd.Text;
                        item.City = EdCitySightAd.Text;
                        item.Information = EdInfSightAd.Text;
                    }
                }
            }
            EditSightListAd.Items.Clear();
            foreach (var item in sights)
            {
                if (CountryAd.Text == item.Country)
                    EditSightListAd.Items.Add(item.Show());
            }

            Ser.Serialize(countries);



            MessageBox.Show("Изменения успешно сохранены");
        }


        /*ЛИСТ С ДОСТОПРИМЕЧАТЕЛЬНОСТЯМИ У ОДНОЙ СТРАНЫ */
        private void EditSightListAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            foreach (var item in sights)
            {
                if (EditSightListAd.SelectedItem != null)
                {
                    if (EditSightListAd.SelectedItem.ToString() == item.Show())
                    {
                        EdNameSightAd.Text = item.Name;
                        EdYearSightAd.Text = item.Year.ToString();
                        EdCitySightAd.Text = item.City;
                        EdInfSightAd.Text = item.Information;
                    }
                }
            }
        }

        /*ПОИСК ПО ЛИСТЕ В АДМИНКЕ */
        private void SearchAd_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Country> c = new List<Country>();
           
            if (SearchAd.Text != null)
            {
                AllCouAd.Items.Clear();

                foreach (var item in countries.ListCou)
                {
                    if (item.Name.Contains(SearchAd.Text))
                    if(!c.Contains(item))
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
                    AllCouAd.Items.Add(item.ShowName());
                }
            }

        }
    }
}
