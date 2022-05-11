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
    /// Логика взаимодействия для AddDiscount.xaml
    /// </summary>
    public partial class AddDiscount : Page
    {
        private int reg = 0;
        private discount _curretDiscount = new discount();
        int maxid = testEntities.GetContext().discount.Max(u => u.discount_id);
        int maxgoodid = testEntities.GetContext().goods.Max(u => u.goods_id);
        public AddDiscount(discount SelectDiscount)
        {
            InitializeComponent();
            if (SelectDiscount != null)
            {
                _curretDiscount = SelectDiscount;
                reg = 1;
            }
            ComboName.ItemsSource = testEntities.GetContext().goods.ToList();
            DataContext = _curretDiscount;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _SearchName;
            _SearchName = ComboName.Text;
            goods SearchType = null;
            SearchType = testEntities.GetContext().goods.Where(b => b.goods_name == _SearchName).FirstOrDefault();
            _curretDiscount.goods_ID = SearchType.type_id;
            StringBuilder errors = new StringBuilder();
            if (_curretDiscount.goods_ID > maxgoodid) errors.AppendLine("Товара с таким кодом нет");
            if (_curretDiscount.discount_size == null) errors.AppendLine("Введите размер скидки");
            if (_curretDiscount.goods_ID == null) errors.AppendLine("Введите код товара");
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }
            _curretDiscount.discount_id = maxid + 1;
            if (reg == 0) testEntities.GetContext().discount.Add(_curretDiscount);
            else
            {
                var detl = testEntities.GetContext().discount.Where(b => b.discount_id == _curretDiscount.discount_id).FirstOrDefault();
                detl.goods_ID = _curretDiscount.goods_ID;
                detl.discount_date = _curretDiscount.discount_date;
                detl.discount_size = _curretDiscount.discount_size;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Скидка добавлена!!");
                else MessageBox.Show("Скидка изменена!!");
                _curretDiscount = new discount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
