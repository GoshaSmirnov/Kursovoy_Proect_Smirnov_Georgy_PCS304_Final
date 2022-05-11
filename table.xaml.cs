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
    /// Логика взаимодействия для table.xaml
    /// </summary>
    public partial class table : Page
    {
        public table()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame2;
        }
        private void Goods_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Apteki(); 
        }
        private void Client_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Client();
        }
        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Employee();
        }
        private void Diskount_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Diskount();
        }
        private void Order_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Order();
        }
        private void Detail_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Detail();
        }

        private void Type_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Type();
        }
        private void Manufacture_Click(object sender, RoutedEventArgs e)
        {
            MainFrame2.Content = new Manufacture();
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = null;
            authorization.index = 0;
        }
    }
}
