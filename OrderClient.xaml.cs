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
    /// Логика взаимодействия для OrderClient.xaml
    /// </summary>
    public partial class OrderClient : Page
    {
        private goods _curretGoods = new goods();
        private order _curreOrder = new order();
        private Order_Detail _curreDetail = new Order_Detail();
        int maxid = testEntities.GetContext().order.Max(u => u.order_id);

        public OrderClient()
        {
            Random rnd = new Random();
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().goods.ToList();
            DataContext = _curreOrder;
            DataContext = _curretGoods;
            _curreOrder.order_price = 0;
            _curreOrder.order_id = maxid + 1;
            _curreOrder.client_id = authorization.index;
            _curreOrder.order_date = DateTime.Now.Date;
            _curreOrder.order_status = "Обрабатывается";
            int i = rnd.Next(1, 3);
            _curreOrder.employee_id = i;
        }
        public void AddClick(object sender, EventArgs e)
        {
            int a = 0, c = 0;
            _curreDetail.detail_id = 5;
            _curretGoods = Aptek.SelectedItems.Cast<goods>().FirstOrDefault();
            a = (int)_curretGoods.manufacturer_id;
            c = (int)_curretGoods.type_id;
            testEntities.GetContext().goods.Remove(_curretGoods);
            testEntities.GetContext().SaveChanges();
            _curretGoods.number_of_goods--;
            testEntities.GetContext().goods.Add(_curretGoods);
            testEntities.GetContext().SaveChanges();
            _curreOrder.order_price += Convert.ToInt32(_curretGoods.goods_price);
            _curreDetail.goods_ID = _curretGoods.goods_id;
            _curreDetail.number_of_goods = 1;
            _curreDetail.order_id = _curreOrder.order_id;
            testEntities.GetContext().order.Add(_curreOrder);
            testEntities.GetContext().Order_Detail.Add(_curreDetail);
            testEntities.GetContext().SaveChanges();
            Aptek2.ItemsSource = testEntities.GetContext().goods.Where(b => b.goods_id == _curretGoods.goods_id).ToList();
        }
        string _SearchName;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchName = Search.Text;
            goods SearchGood = null;
            SearchGood = testEntities.GetContext().goods.Where(b => b.goods_name == _SearchName).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().goods.Where(b => b.goods_name == _SearchName).ToList();
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            testEntities.GetContext().order.Add(_curreOrder);

            try
            {
                testEntities.GetContext().SaveChanges();
                MessageBox.Show("Okey!");
                _curreOrder = new order();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
