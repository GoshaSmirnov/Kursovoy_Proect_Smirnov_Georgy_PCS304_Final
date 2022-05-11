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
    /// Логика взаимодействия для Diskount.xaml
    /// </summary>
    public partial class Diskount : Page
    {
        public Diskount()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().discount.ToList();

        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().discount.ToList();
            }
        }
        public void AddClick(object sender, EventArgs e)
        {
            Manager.MainFrame.Navigate(new AddDiscount(null));
        }
        public void EditClick(object sender, EventArgs e)
        {
            var DiscountForUpdate = Aptek.SelectedItems.Cast<discount>().FirstOrDefault();
            var DiscountForRemoving = Aptek.SelectedItems.Cast<discount>().ToList();
            testEntities.GetContext().discount.RemoveRange(DiscountForRemoving);
            testEntities.GetContext().SaveChanges();
            Manager.MainFrame.Content = new AddDiscount(DiscountForUpdate);
        }
        public void BackClick(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<discount>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().discount.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().discount.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        int _SearchName;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchName = Convert.ToInt32(Search.Text);
            discount SearchGood = null;
            SearchGood = testEntities.GetContext().discount.Where(b => b.discount_id == _SearchName).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().discount.Where(b => b.discount_id == _SearchName).ToList();
            }
        }
    }
}
