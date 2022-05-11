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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        private int reg = 0;
        private order _curreOrder = new order();

        int maxid = testEntities.GetContext().order.Max(u => u.order_id);
        public AddOrder(order SelectOrder)
        {
            InitializeComponent();
            if (SelectOrder != null)
            {
                _curreOrder = SelectOrder;
                reg = 1;
            }
            DataContext = _curreOrder;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_curreOrder.order_status)) errors.AppendLine("Введите название");
            if (_curreOrder.order_price == null) errors.AppendLine("Введите цену товара");
            if (_curreOrder.client_id == null) errors.AppendLine("Введите код производителя");
            if (_curreOrder.employee_id == null) errors.AppendLine("Введите количество товара");
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }
            _curreOrder.order_id = maxid + 1;
            if (reg == 0) testEntities.GetContext().order.Add(_curreOrder);
            else
            {
                var detl = testEntities.GetContext().order.Where(b => b.order_id == _curreOrder.order_id).FirstOrDefault();
                detl.order_price = _curreOrder.order_price;
                detl.order_status = _curreOrder.order_status;
                detl.client_id = _curreOrder.client_id;
                detl.employee_id = _curreOrder.employee_id;
                detl.order_date = _curreOrder.order_date;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Заказ добавлен!");
                else MessageBox.Show("Заказ изменён!");
                _curreOrder = new order();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
