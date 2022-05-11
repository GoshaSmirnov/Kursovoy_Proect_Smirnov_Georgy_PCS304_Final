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
    public partial class OrderClient2_0 : Page
    {
        private goods _curretGoods = new goods();
        private order _curreOrder = new order();
        private Order_Detail _curreDetail = new Order_Detail();
        private discount _curretDiscount = new discount();
        int maxid = testEntities.GetContext().order.Max(u => u.order_id);
        
        public OrderClient2_0()
        {
            Random rnd = new Random();
            InitializeComponent();
            LViewGoods.ItemsSource = testEntities.GetContext().goods.ToList();
            DataContext = _curreOrder;
            DataContext = _curretGoods;
            _curreOrder.order_price = 0;
            _curreOrder.order_id = maxid + 1;          
            _curreOrder.client_id = authorization.index;
            _curreOrder.order_date = DateTime.Now.Date;
            _curreOrder.order_status = "Обрабатывается";
            int i = rnd.Next(1, 10);
            _curreOrder.employee_id = i;
            _curreOrder.employee = testEntities.GetContext().employee.Where(b => b.employee_id == _curreOrder.employee_id).FirstOrDefault();
            testEntities.GetContext().order.Add(_curreOrder);
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {

            int maxidDetail = testEntities.GetContext().Order_Detail.Max(u => u.detail_id);
            _curretGoods = LViewGoods.SelectedItems.Cast<goods>().FirstOrDefault();
            if (_curretGoods.number_of_goods > 0)
            {
                _curreDetail.detail_id = maxidDetail + 1;
                var gods = testEntities.GetContext().goods.Where(b => b.goods_id == _curretGoods.goods_id).FirstOrDefault();
                gods.number_of_goods = _curretGoods.number_of_goods - 1;
                testEntities.GetContext().SaveChanges();
                var ordr = testEntities.GetContext().order.Where(b => b.order_id == _curreOrder.order_id).FirstOrDefault();
                _curretDiscount = testEntities.GetContext().discount.Where(b => b.goods_ID == _curretGoods.goods_id).FirstOrDefault();
                if (_curretDiscount != null)
                {
                    if (_curretDiscount.discount_date == DateTime.Now.Date)
                    {
                        ordr.order_price += _curretGoods.goods_price - (_curretGoods.goods_price) * (_curretDiscount.discount_size) / 100;
                        testEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        ordr.order_price += _curretGoods.goods_price;
                        testEntities.GetContext().SaveChanges();
                    }
                }
                else
                {
                    ordr.order_price += _curretGoods.goods_price;
                    testEntities.GetContext().SaveChanges();
                }
                _curreDetail.goods_ID = _curretGoods.goods_id;
                _curreDetail.order_id = _curreOrder.order_id;
                var detl = testEntities.GetContext().Order_Detail.Where(b => b.order_id == _curreOrder.order_id && b.goods_ID == _curretGoods.goods_id).FirstOrDefault();
                if (detl == null)
                {
                    _curreDetail.number_of_goods = 1;
                    testEntities.GetContext().Order_Detail.Add(_curreDetail);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Товар добавлен");
                    _curreDetail = new Order_Detail();
                }
                else
                {
                    
                    detl.number_of_goods ++;
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Товар повторно добавлен");
                }
                
            }
            else { MessageBox.Show("Товар закончился"); }
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (_curreOrder.order_price > 0)
            {
                try
                {
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно!");
                    _curreOrder = new order();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else { MessageBox.Show("Заказ пустой"); }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = null;
            authorization.index = 0;
        }
        string _SearchName;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchName = Search.Text;
            goods SearchGood = null;
            SearchGood = testEntities.GetContext().goods.Where(b => b.goods_name ==  _SearchName).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                LViewGoods.ItemsSource = testEntities.GetContext().goods.Where(b => b.goods_name == _SearchName).ToList();
            }
        }
    }
}
