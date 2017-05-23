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

            if (File.Exists("../../allcountries.xml"))
            {
                countries = Ser.DeSerialize(countries);
                foreach (var item in countries.ListCou)
                {
                    foreach (var s in item.LSight)
                    {
                        sights.Add(s);
                        AllSightAd.Items.Add(s.ShowC());
                    }
                }
            }
        }
       
        /*ДОБАВИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void AddCountryAd_Click(object sender, RoutedEventArgs e)
        {
            EditSightAd.Visibility = Visibility.Visible;
            ButSaveSightAd.Visibility = Visibility.Visible;
            ButEditSightAd.Visibility = Visibility.Hidden;

            EdNameSightAd.Text = "";
            EdYearSightAd.Text = "";
            EdCitySightAd.Text = "";
            EdInfSightAd.Text = "";
            SightImEdAd.Text = "";

        }

        /*СОХРАНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void ButSaveSightAd_Click(object sender, RoutedEventArgs e)
        {
            ButEditSightAd.Visibility = Visibility.Visible;
            ButSaveSightAd.Visibility = Visibility.Hidden;
            
            Sight si = new Sight(EdNameSightAd.Text, EdYearSightAd.Text, EdInfSightAd.Text, EdCitySightAd.Text, EdCountrySightAd.Text, SightImEdAd.Text);
            sights.Add(si);
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

        /*ВЫБРАТЬ ЭЛЕМЕНТ ИЗ ЛИСТА СО ВСЕМИ ДОСТ */
        private void AllSightAd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        /*ИЗМЕНИТЬ ДОСТОПРИМЕЧАТЕЛЬНОСТЬ */
        private void ButEditSightAd_Click(object sender, RoutedEventArgs e)
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
                       

                    }
                }
            }

            foreach (var item in countries.ListCou)
            {
                if (AllSightAd.SelectedItem != null)
                    if (item.Name == sig.Country)
                    {
                        foreach (var s in item.LSight)
                        {
                            if(s.Name == sig.Name)
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
    }
}
