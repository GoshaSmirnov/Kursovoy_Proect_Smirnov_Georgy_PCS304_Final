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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().order.ToList();

        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().order.ToList();
            }
        }
        public void AddClick(object sender, EventArgs e)
        {
            Manager.MainFrame.Navigate(new AddOrder(null));
        }
        int _SearchKod;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchKod = Convert.ToInt32(Search.Text);
            order SearchGood = null;
            SearchGood = testEntities.GetContext().order.Where(b => b.client_id == _SearchKod).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().order.Where(b => b.client_id == _SearchKod).ToList();
            }
        }
        public void EditClick(object sender, EventArgs e)
        {
            var OrderForUpdate = Aptek.SelectedItems.Cast<order>().FirstOrDefault();
            var OrderForRemoving = Aptek.SelectedItems.Cast<order>().ToList();
            testEntities.GetContext().order.RemoveRange(OrderForRemoving);
            testEntities.GetContext().SaveChanges();
            Manager.MainFrame.Content = new AddOrder(OrderForUpdate);
        }
        public void BackClick(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<order>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().order.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().order.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
