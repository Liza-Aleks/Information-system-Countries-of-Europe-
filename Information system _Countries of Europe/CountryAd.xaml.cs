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
using Microsoft.Win32;
using System.IO;

namespace Information_system__Countries_of_Europe
{
    /// <summary>
    /// Логика взаимодействия для CountryAd.xaml
    /// </summary>
    public partial class CountryAd : Page
    {
        public ListCountries countries = new ListCountries();

        public List<Sight> sights = new List<Sight>();

        public CountryAd()
        {
            InitializeComponent();
            try {
                countries.ListCou = new List<Country>();

                if (File.Exists("../../allcountries.xml"))
                {
                    countries = Ser.DeSerialize(countries);

                    Sort.C(countries.ListCou);
                    foreach (var item in countries.ListCou)
                    {
                        AllCouAd.Items.Add(item.Name);
                        foreach (var s in item.LSight)
                        {
                            Sort.S(item.LSight);
                            sights.Add(s);
                        }
                    }
                    Sort.S(sights);
                   
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }
       
        /*СТРАНИЧКА ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN ADMIN*/

        


        /*При нажатии элемента из ListBox в admin  */
        private void AllCouAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                AddCountrySP.Visibility = Visibility.Hidden;
                AddDeleteCountrySP.Visibility = Visibility.Visible;
                CountryRedAd.Visibility = Visibility.Visible;
                EditCountryAd.Visibility = Visibility.Visible;
                EditSightAd.Visibility = Visibility.Visible;
                SaveCountryAd.Visibility = Visibility.Hidden;
                SightAd.Visibility = Visibility.Hidden;
                EditSightListAd.Items.Clear();
                EdNameSightAd.Text = "";
                EdYearSightAd.Text = "";
                EdCitySightAd.Text = "";
                EdInfSightAd.Text = "";
                SightImEdAd.Text = "";


                foreach (var item in countries.ListCou)
                {
                    if (AllCouAd.SelectedItem != null)
                    {
                        if (AllCouAd.SelectedItem.ToString() == item.Name)
                        {
                            CountryNameAd.Text = item.Name;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }



        }

        /*ДОБАВИТЬ СТРАНУ*/
        //private void AddCountryAd_Click(object sender, RoutedEventArgs e)
        //{
        //    try {
        //        CountryRedAd.Visibility = Visibility.Visible;
        //        SaveCountryAd.Visibility = Visibility.Visible;
        //        EditCountryAd.Visibility = Visibility.Hidden;
        //        EditSightAd.Visibility = Visibility.Hidden;

        //        CountryNameAd.Text = "";
        //        CapitalAd.Text = "";
        //        LangAd.Text = "";
        //        SquareAd.Text = "";
        //        PopAd.Text = "";
        //        FlagAd.Text = "";

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        Log.L("Ошибка! " + ex.Message);
        //    }

        //}

        /*ДОБАВИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ*/
        private void AddSightAd_Click(object sender, RoutedEventArgs e)
        {
            try {
                SightAd.Visibility = Visibility.Visible;
                EditSightAd.Visibility = Visibility.Hidden;
                NameSightAd.Text = "";
                YearSightAd.Text = "";
                InfSightAd.Text = "";
                CitySightAd.Text = "";
                SightImAd.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }

        }

        /*ЗАГРУЗИТЬ ФОТО*/
        public void DownloadImage_Click(object sender, RoutedEventArgs e)
        {
            try {
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
                
                Log.L("Загружено изображение; " + DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }

        }

        public void SightImDownAd_Click(object sender, RoutedEventArgs e)
        {
            try
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

                    Log.L("Загружено изображение; " + DateTime.Now);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }

        }
        

        /*СОХРАНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ*/
        private void SaveSightAd_Click(object sender, RoutedEventArgs e)
        {
            try {
                SightAd.Visibility = Visibility.Hidden;

                foreach (var item in countries.ListCou)
                {
                    if (CountryNameAd.Text == item.Name)
                        EditSightAd.Visibility = Visibility.Visible;
                }


                Sight si = new Sight(NameSightAd.Text, YearSightAd.Text, InfSightAd.Text, CitySightAd.Text, CountryNameAd.Text, SightImAd.Text);
                sights.Add(si);
                Log.L("Добавлена достопримечательность " + si.Name + "; " + DateTime.Now);
                foreach (var item in countries.ListCou)
                {
                    if (item.Name == si.Country)
                    {
                        item.LSight.Add(si);
                        Sort.S(item.LSight);
                    }
                }

                Sort.S(sights);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
          
        }

        /*СОХРАНИТЬ СТРАНУ*/
        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    try {
        //        SaveCountryAd.Visibility = Visibility.Hidden;
        //        EditCountryAd.Visibility = Visibility.Visible;

        //        EditSightAd.Visibility = Visibility.Visible;

        //        List<Sight> sightscou = new List<Sight>();
        //        foreach (var item in sights)
        //        {
        //            if (CountryNameAd.Text == item.Country)
        //            {
        //                sightscou.Add(item);
        //            }
        //        }

        //        Country cou = new Country(CountryNameAd.Text, CapitalAd.Text, LangAd.Text, float.Parse(SquareAd.Text), float.Parse(PopAd.Text), FlagAd.Text, sightscou);
               
        //        Log.L("Добавлена страна " + cou.Name + "; " + DateTime.Now);
        //        countries.ListCou.Add(cou);

        //        Sort.C(countries.ListCou);

        //        AllCouAd.Items.Clear();
        //        foreach (var item in countries.ListCou)
        //        {
        //            AllCouAd.Items.Add(item.ShowName());
        //        }

        //        EditSightListAd.Items.Clear();
        //        foreach (var item in sights)
        //        {
        //            if (cou.Name == item.Country)
        //                EditSightListAd.Items.Add(item.Show());
        //        }

        //        Ser.Serialize(countries);

        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        Log.L("Ошибка! " + ex.Message);
        //    }


        //}

        /*УДАЛИТЬ СТРАНУ */
        //private void DeleteCountryAd_Click(object sender, RoutedEventArgs e)
        //{
        //    try {
        //        if (AllCouAd.SelectedItem != null)
        //        {
        //            foreach (var item in countries.ListCou)
        //            {
        //                if (AllCouAd.SelectedItem.ToString() == item.Name)
        //                {
        //                    int i = countries.ListCou.IndexOf(item);
        //                    countries.ListCou.RemoveAt(i);
        //                    Log.L("Удалена страна" + item.Name + "; " + DateTime.Now);
        //                    break;

        //                }
        //            }
        //            AllCouAd.Items.Clear();

        //            foreach (var item in countries.ListCou)
        //            {
        //                AllCouAd.Items.Add(item.Name);

        //            }

        //            Ser.Serialize(countries);


        //            CountryNameAd.Text = "";
        //            CapitalAd.Text = "";
        //            LangAd.Text = "";
        //            SquareAd.Text = "";
        //            PopAd.Text = "";
        //            FlagAd.Text = "";
        //        }
        //        else
        //            MessageBox.Show("Выберите страну", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //      }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        Log.L("Ошибка! " + ex.Message);
        //    }
        //}

        /*УДАЛИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ*/
        private void DeleteSightAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in sights)
                {
                    if (EditSightListAd.SelectedItem.ToString() == item.Show())
                    {
                        int i = sights.IndexOf(item);
                        sights.RemoveAt(i);
                        Log.L("Удалена достопримечательность" +item.Name+"; " +DateTime.Now);
                      
                        break;
                    }
                }

                foreach (var item in countries.ListCou)
                {
                    foreach (var ls in item.LSight)
                    {
                        if (EditSightListAd.SelectedItem.ToString() == ls.Show())
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
                    if (CountryNameAd.Text == item.Country)
                        EditSightListAd.Items.Add(item.Show());
                }


                Ser.Serialize(countries);

                EdNameSightAd.Text = "";
                EdYearSightAd.Text = "";
                EdCitySightAd.Text = "";
                EdInfSightAd.Text = "";
                SightImEdAd.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }


        }

        /*ИЗМЕНИТЬ СТРАНУ */
        private void EditCountryAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditSightAd.Visibility = Visibility.Visible;

                foreach (var item in countries.ListCou)
                {
                    if (AllCouAd.SelectedItem != null)
                        if (AllCouAd.SelectedItem.ToString() == item.Name)
                        {
                            string s = item.Name;
                            item.Name = CountryNameAd.Text;
                            item.Capital = CapitalAd.Text;
                            item.Language = LangAd.Text;
                            item.Population = float.Parse(PopAd.Text);
                            item.Square = float.Parse(SquareAd.Text);
                            item.Flag = FlagAd.Text;
                            foreach (var ls in item.LSight)
                            {
                                ls.Country = CountryNameAd.Text;
                            }

                            Log.L("Изменена страна" +item.Name+"; " +DateTime.Now);
                            
                        }
                }

                Sort.C(countries.ListCou);
                AllCouAd.Items.Clear();

                foreach (var item in countries.ListCou)
                {
                    AllCouAd.Items.Add(item.Name);
                }

                Ser.Serialize(countries);

                EditSightListAd.Items.Clear();
                foreach (var item in sights)
                {
                    if (CountryNameAd.Text == item.Country)
                        EditSightListAd.Items.Add(item.Show());
                }

                MessageBox.Show("Изменения успешно сохранены");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*ИЗМЕНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void EdSaveSightAd_Click(object sender, RoutedEventArgs e)
        {
            try {
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
                            item.SightIm = SightImAd.Text;
                            Log.L("Изменена достопримечательность " +item.Name+"; " +DateTime.Now);
                           
                        }
                    }
                }
                EditSightListAd.Items.Clear();
                foreach (var item in sights)
                {
                    if (CountryNameAd.Text == item.Country)
                        EditSightListAd.Items.Add(item.Show());
                }

                Ser.Serialize(countries);



                MessageBox.Show("Изменения успешно сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }


        /*ЛИСТ С ДОСТОПРИМЕЧАТЕЛЬНОСТЯМИ У ОДНОЙ СТРАНЫ */
        private void EditSightListAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
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
                            SightImEdAd.Text = item.SightIm;
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

        /*ПОИСК ПО ЛИСТЕ В АДМИНКЕ */
        private void SearchAd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                List<Country> c = new List<Country>();

                if (SearchAd.Text != null)
                {
                    AllCouAd.Items.Clear();

                    foreach (var item in countries.ListCou)
                    {
                        if (item.Name.Contains(SearchAd.Text))
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
                        AllCouAd.Items.Add(item.ShowName());
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
