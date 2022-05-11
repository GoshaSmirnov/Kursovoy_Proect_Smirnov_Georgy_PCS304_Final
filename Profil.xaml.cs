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
    /// Логика взаимодействия для Profil.xaml
    /// </summary>
    public partial class Profil : Page
    {
        public Profil()
        {
            InitializeComponent();
            int _SearchKod = authorization.index;
                order SearchGood = null;
                SearchGood = testEntities.GetContext().order.Where(b => b.client_id == _SearchKod).FirstOrDefault();

                if (SearchGood == null) Aptek.ItemsSource = "Вы ещё не делали заказов";
                else
                {
                    Aptek.ItemsSource = testEntities.GetContext().order.Where(b => b.client_id == _SearchKod).ToList();
                }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var SelectOrder = testEntities.GetContext().order.FirstOrDefault();
            if (Aptek.SelectedItems.Cast<order>().FirstOrDefault() != null)
            {
                SelectOrder = Aptek.SelectedItems.Cast<order>().FirstOrDefault();
            }
            if (SelectOrder.order_status.Replace(" ", "") == "Обрабатывается")
            {
                Check check = new Check(SelectOrder);
                check.Show();
                var ordr = SelectOrder;
                ordr.order_status = "Оплачен";
            }
            else
            {
                if (MessageBox.Show($"Хотите посмотреть чек?", "Этот заказ уже оплачен!",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Check check = new Check(SelectOrder);
                    check.Show();
                }
            }
        }
    }
}
