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
    /// Логика взаимодействия для number.xaml
    /// </summary>
    public partial class number : Page
    {
        public static int Clientnumber;
        public number()
        {
            InitializeComponent();
            NumberTXT.Text = testEntities.GetContext().goods.Where(b => b.goods_id == 8).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clientnumber = Convert.ToInt32(vvod.Text);
        }
    }
}