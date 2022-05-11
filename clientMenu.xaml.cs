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

namespace FinalTest
{
    /// <summary>
    /// Логика взаимодействия для clientMenu.xaml
    /// </summary>
    public partial class clientMenu : Page
    {
        public clientMenu()
        {
            InitializeComponent();
            UserInfo.Text = authorization.Surname + " " + authorization.ClientName;
        }
        private void ProfilClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new Profil();

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = null;
            authorization.index = 0;
        }
        private void OrderClientClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new OrderClient2_0();
            
        }
    }
}
