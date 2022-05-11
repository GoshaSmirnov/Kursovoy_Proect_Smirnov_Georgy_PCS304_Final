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
    /// Логика взаимодействия для AddDetail.xaml
    /// </summary>
    public partial class AddDetail : Page
    {
        private int reg = 0;
        int maxidDetail = testEntities.GetContext().Order_Detail.Max(u => u.detail_id);
        private Order_Detail _curretDetail = new Order_Detail();
        int maxid = testEntities.GetContext().order.Max(u => u.order_id);
        public AddDetail(Order_Detail SelectDetail)
        {
            InitializeComponent();
            if (SelectDetail != null)
            {
                _curretDetail = SelectDetail;
                reg = 1;
            }
            DataContext = _curretDetail;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _curretDetail.detail_id = maxidDetail + 1;
            StringBuilder errors = new StringBuilder();
            if (_curretDetail.order_id > maxid) errors.AppendLine("Заказа с таким кодом нет");
            if (_curretDetail.number_of_goods == null) errors.AppendLine("Введите количество товара");
            if (_curretDetail.goods_ID == null) errors.AppendLine("Введите код товара");
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }

            if (reg == 0) testEntities.GetContext().Order_Detail.Add(_curretDetail);
            else
            {
                var detl = testEntities.GetContext().Order_Detail.Where(b => b.detail_id == _curretDetail.detail_id).FirstOrDefault();
                detl.goods_ID = _curretDetail.goods_ID;
                detl.number_of_goods = _curretDetail.number_of_goods;
                detl.order_id = _curretDetail.order_id;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0)  MessageBox.Show("Добавлено!");
                else MessageBox.Show("Изменено!");
                _curretDetail = new Order_Detail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
