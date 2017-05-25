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
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {

        MainWindow wnd;
        public Password(MainWindow w)
        {
            wnd = w;
            InitializeComponent();
            passwordBox.Focus();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "123")
            {
                
                Log.L("Выполнен вход в режим администратора; " + DateTime.Now);
                Close();
                wnd.MainPage.Visibility = Visibility.Hidden;
                wnd.Admin.Visibility = Visibility.Visible;
                wnd.AdditionalInf.Visibility = Visibility.Hidden;

            }
        
            else
                MessageBox.Show("Неверный пароль!", "Вход доступен только администратору", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
