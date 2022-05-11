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
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Window
    {
        private order _currentOrder = new order(), testOrder = new order();
        private goods _currentGoods = new goods();
        private discount _currentDiscount = new discount();

        public Check(order selectOrder)
        {
            InitializeComponent();
            _currentOrder = selectOrder;
            NumberTXT.Text = _currentOrder.order_id.ToString();
            ClientTXT.Text = authorization.Surname + " " + authorization.ClientName + " " + authorization.PastName;
            EmployeeTXT.Text = _currentOrder.employee.employee_name.Replace(" ", "") + " " + _currentOrder.employee.employee_surname.Replace(" ", "") + " " + _currentOrder.employee.employee_pastname.Replace(" ", "");
            DateTXT.Text = _currentOrder.order_date.ToString();
            PriceTXT.Text = _currentOrder.order_price.ToString() + " РУБ";
            int _SearchKod = _currentOrder.order_id;
            testOrder.order_price = 200;
            testOrder.order_id = _currentOrder.order_id;
            Aptek.ItemsSource = testEntities.GetContext().Order_Detail.Where(b => b.order_id == _SearchKod).ToList();
        }
        private void PrintClick(object sender, RoutedEventArgs e)
        {
            try
            {
                (sender as Button).Visibility = Visibility.Hidden;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    MessageBox.Show("Успех");
                }
            }
            finally
            {
                (sender as Button).Visibility = Visibility.Visible;
            }
        }
    }
}
