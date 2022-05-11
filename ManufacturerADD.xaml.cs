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
    /// Логика взаимодействия для ManufacturerADD.xaml
    /// </summary>
    public partial class ManufacturerADD : Page
    {
        private int reg = 0;
        private manufacturer _curretManufacturer = new manufacturer();
        int maxManufactureid = testEntities.GetContext().manufacturer.Max(u => u.manufacturer_id);
        public ManufacturerADD(manufacturer SelectManufacturer)
        {
            InitializeComponent();
            if (SelectManufacturer != null)
            {
                _curretManufacturer = SelectManufacturer;
                reg = 1;
            }
            DataContext = _curretManufacturer;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_curretManufacturer.manufacturer1)) errors.AppendLine("Введите название");
            if (string.IsNullOrWhiteSpace(_curretManufacturer.country)) errors.AppendLine("Введите страну");
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }
            _curretManufacturer.manufacturer_id = maxManufactureid + 1;
            if (reg == 0) testEntities.GetContext().manufacturer.Add(_curretManufacturer);
            else
            {
                var gods = testEntities.GetContext().manufacturer.Where(b => b.manufacturer_id == _curretManufacturer.manufacturer_id).FirstOrDefault();
                gods.manufacturer1 = _curretManufacturer.manufacturer1;
                gods.country = _curretManufacturer.country;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Производитель добавлен!");
                else MessageBox.Show("Производитель изменён!");
                _curretManufacturer = new manufacturer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
