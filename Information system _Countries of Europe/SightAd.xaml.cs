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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Information_system__Countries_of_Europe
{
    /// <summary>
    /// Логика взаимодействия для SightAd.xaml
    /// </summary>
    public partial class SightAd : Page
    {
        public ListCountries countries = new ListCountries();

        public List<Sight> sights = new List<Sight>();

        public SightAd()
        {
            InitializeComponent();
            try
            {
                if (File.Exists("../../allcountries.xml"))
                {
                    countries = Ser.DeSerialize(countries);
                    foreach (var item in countries.ListCou)
                    {
                        EdCountrySightAd.Items.Add(item.Name);
                        foreach (var s in item.LSight)
                        {
                            sights.Add(s);
                        }
                    }

                    Sort.S(sights);
                    foreach (var item in sights)
                    {
                        AllSightAd.Items.Add(item.ShowC());
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*ДОБАВИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void AddCountryAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditSightAd.Visibility = Visibility.Visible;
                ButSaveSightAd.Visibility = Visibility.Visible;
                ButEditSightAd.Visibility = Visibility.Hidden;

                EdNameSightAd.Text = "";
                EdYearSightAd.Text = "";
                EdCitySightAd.Text = "";
                EdCountrySightAd.SelectedIndex = -1;
                EdInfSightAd.Text = "";
                SightImEdAd.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }

        }

        /*СОХРАНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void ButSaveSightAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButEditSightAd.Visibility = Visibility.Visible;
                ButSaveSightAd.Visibility = Visibility.Hidden;

                Sight si = new Sight(EdNameSightAd.Text, EdYearSightAd.Text, EdInfSightAd.Text, EdCitySightAd.Text, EdCountrySightAd.Text, SightImEdAd.Text);
                sights.Add(si);
                Log.L("Добавлена достопримечательность" + si.Name + "; " + DateTime.Now);
                Sort.S(sights);
                foreach (var item in countries.ListCou)
                {
                    if (item.Name == si.Country)
                    {
                        item.LSight.Add(si);
                    }
                }

                AllSightAd.Items.Clear();

                foreach (var item in sights)
                {
                    AllSightAd.Items.Add(item.ShowC());
                }


                Ser.Serialize(countries);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*ВЫБРАТЬ ЭЛЕМЕНТ ИЗ ЛИСТА СО ВСЕМИ ДОСТ */
        private void AllSightAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AddSightSP.Visibility = Visibility.Hidden;
                AddDeleteSightSP.Visibility = Visibility.Visible;
                EditSightAd.Visibility = Visibility.Visible;
                ButEditSightAd.Visibility = Visibility.Visible;
                ButSaveSightAd.Visibility = Visibility.Hidden;

                foreach (var item in sights)
                {
                    if (AllSightAd.SelectedItem != null)
                    {
                        if (AllSightAd.SelectedItem.ToString() == item.ShowC())
                        {
                            EdNameSightAd.Text = item.Name;
                            EdYearSightAd.Text = item.Year.ToString();
                            EdCitySightAd.Text = item.City;
                            EdCountrySightAd.Text = item.Country;
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

        /*ИЗМЕНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void ButEditSightAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sight sig = new Sight();
                foreach (var item in sights)
                {
                    if (AllSightAd.SelectedItem != null)
                    {
                        if (AllSightAd.SelectedItem.ToString() == item.ShowC())
                        {
                            sig = item;
                            item.Name = EdNameSightAd.Text;
                            item.Year = EdYearSightAd.Text;
                            item.City = EdCitySightAd.Text;
                            item.Country = EdCountrySightAd.Text;
                            item.Information = EdInfSightAd.Text;
                            item.SightIm = SightImEdAd.Text;
                            Log.L("Изменена достопримечательность " + item.Name + "; " + DateTime.Now);

                        }
                    }
                }

                Sort.S(sights);
                foreach (var item in countries.ListCou)
                {
                    if (AllSightAd.SelectedItem != null)
                        if (item.Name == sig.Country)
                        {
                            foreach (var s in item.LSight)
                            {
                                if (s.Name == sig.Name)
                                {
                                    s.Name = EdNameSightAd.Text;
                                    s.Year = EdYearSightAd.Text;
                                    s.City = EdCitySightAd.Text;
                                    s.Country = EdCountrySightAd.Text;
                                    s.Information = EdInfSightAd.Text;
                                    s.SightIm = SightImEdAd.Text;
                                }
                            }
                        }
                }

                AllSightAd.Items.Clear();
                foreach (var item in sights)
                {
                    AllSightAd.Items.Add(item.ShowC());
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
        
        /*УДАЛИТЬ */
        private void DeletSightAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in sights)
                {
                    if (AllSightAd.SelectedItem.ToString() == item.ShowC())
                    {
                        int i = sights.IndexOf(item);
                        sights.RemoveAt(i);
                        Log.L("Удалена достопримечательность" + item.Name + "; " + DateTime.Now);

                        break;
                    }
                }

                foreach (var item in countries.ListCou)
                {
                    foreach (var ls in item.LSight)
                    {
                        if (AllSightAd.SelectedItem.ToString() == ls.ShowC())
                        {
                            item.LSight.Remove(ls);
                            break;
                        }

                    }
                }

                AllSightAd.Items.Clear();

                foreach (var item in sights)
                {
                        AllSightAd.Items.Add(item.ShowC());
                }
                
                Ser.Serialize(countries);

                EdNameSightAd.Text = "";
                EdYearSightAd.Text = "";
                EdCitySightAd.Text = "";
                EdCountrySightAd.SelectedIndex = -1;
                EdInfSightAd.Text = "";
                SightImEdAd.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }


        /*ФОТОГРАФИЯ */
        private void SightImDownEdAd_Click(object sender, RoutedEventArgs e)
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
                    PathNew = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Pathnow) + "/imgFlag/", PathNew);

                    File.Copy(fileN, PathNew, true);
                    BitmapImage img;
                    img = new BitmapImage(new Uri(PathNew));
                    SightImEdAd.Text = PathNew;

                }

                Log.L("Загружено изображение; " + DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Warning);
                Log.L("Ошибка! " + ex.Message);
            }
        }

        /*ПОИСК ПО ЛИСТУ */
        private void SearchSightAd_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Sight> s = new List<Sight>();

                if (SearchSightAd.Text != null)
                {
                    AllSightAd.Items.Clear();

                    foreach (var item in sights)
                    {
                        if (item.Name.Contains(SearchSightAd.Text))
                            if (!s.Contains(item))
                            {
                                s.Add(item);
                            }
                            else
                            {
                                s.Clear();
                                foreach (var cou in countries.ListCou)
                                {
                                    s.Add(item);
                                }
                            }
                    }
                    foreach (var item in s)
                    {
                        AllSightAd.Items.Add(item.ShowC());
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
