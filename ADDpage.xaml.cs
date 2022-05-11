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
    /// Логика взаимодействия для ADDpage.xaml
    /// </summary>
    public partial class ADDpage : Page
    {
        private goods _curretGoods = new goods();
        private int reg = 0;
        int maxid = testEntities.GetContext().goods.Max(u => u.goods_id);
        int maxtypeid = testEntities.GetContext().type.Max(u => u.type_id);
        int maxManufactureid = testEntities.GetContext().manufacturer.Max(u => u.manufacturer_id);
        public ADDpage(goods selectGood)
        {
            
            if (selectGood != null)
            {
                _curretGoods = selectGood;
                reg = 1;
            }
            else
            {
                _curretGoods.goods_id = maxid + 1;
            }
            InitializeComponent();
            DataContext = _curretGoods;
            ComboType.ItemsSource = testEntities.GetContext().type.ToList();
            ManufactureCombo.ItemsSource = testEntities.GetContext().manufacturer.ToList();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            string _SearchName;
            _SearchName = ComboType.Text;
            type SearchType = null;
            SearchType = testEntities.GetContext().type.Where(b => b.type1 == _SearchName).FirstOrDefault();
            _curretGoods.type_id = SearchType.type_id;
            _SearchName = ManufactureCombo.Text;
            manufacturer SearchManufacturer = null;
            SearchManufacturer = testEntities.GetContext().manufacturer.Where(b => b.manufacturer1 == _SearchName).FirstOrDefault();
            _curretGoods.manufacturer_id = SearchManufacturer.manufacturer_id;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_curretGoods.goods_name)) errors.AppendLine("Введите название");
            if (_curretGoods.goods_name.Length > 50) errors.AppendLine("Название должно быть короче 50 символов");
            if (_curretGoods.goods_price == null) errors.AppendLine("Введите цену товара");
            if (_curretGoods.manufacturer_id == null) errors.AppendLine("Введите код производителя");
            if (_curretGoods.manufacturer_id > maxManufactureid) errors.AppendLine("Такого производителя нет в базе данных");
            if (_curretGoods.number_of_goods == null) errors.AppendLine("Введите количество товара");
            if (_curretGoods.type_id == null) errors.AppendLine("Введите код типа");
            if (_curretGoods.type_id > maxtypeid) errors.AppendLine("Такого типа нет в базе данных");
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }
            
            if (reg == 0 ) testEntities.GetContext().goods.Add(_curretGoods);
            else
            {
                var gods = testEntities.GetContext().goods.Where(b => b.goods_id == _curretGoods.goods_id).FirstOrDefault();
                gods.goods_image = _curretGoods.goods_image;
                gods.goods_name = _curretGoods.goods_name;
                gods.goods_price = _curretGoods.goods_price;
                gods.manufacturer_id = _curretGoods.manufacturer_id;
                gods.number_of_goods = _curretGoods.number_of_goods;
                gods.type_id = _curretGoods.type_id;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Товар добавлен!");
                else MessageBox.Show("Товар изменён!");
                _curretGoods = new goods();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
      
    }
}
