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
    /// Логика взаимодействия для ADDtype.xaml
    /// </summary>
    public partial class ADDtype : Page
    {
        private int reg = 0;
        private type _curretType = new type();
        int maxtypeid = testEntities.GetContext().type.Max(u => u.type_id);
        public ADDtype(type SelectType)
        {
            InitializeComponent();
            if (SelectType != null)
            {
                _curretType = SelectType;
                reg = 1;
            }
            else
            {
                _curretType.type_id = maxtypeid + 1;
            }
            DataContext = _curretType;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (reg == 0) testEntities.GetContext().type.Add(_curretType);
            else
            {
                var _type = testEntities.GetContext().type.Where(b => b.type_id == _curretType.type_id).FirstOrDefault();
                _type.type1 = _curretType.type1;
                _type.characteristics = _curretType.characteristics;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Тип добавлен!");
                else MessageBox.Show("Тип изменён!");
                _curretType = new type();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
