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
    /// Логика взаимодействия для Detail.xaml
    /// </summary>
    public partial class Detail : Page
    {

        public Detail()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().Order_Detail.ToList();

        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().Order_Detail.ToList();
            }
        }
        public void AddClick(object sender, EventArgs e)
        {
            Manager.MainFrame.Navigate(new AddDetail(null)); ;
        }
        public void EditClick(object sender, EventArgs e)
        {
            var DetailForUpdate = Aptek.SelectedItems.Cast<Order_Detail>().FirstOrDefault();
            var DetailForRemoving = Aptek.SelectedItems.Cast<Order_Detail>().ToList();
            testEntities.GetContext().Order_Detail.RemoveRange(DetailForRemoving);
            testEntities.GetContext().SaveChanges();
            Manager.MainFrame.Content = new AddDetail(DetailForUpdate);
        }
        public void BackClick(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<Order_Detail>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().Order_Detail.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().Order_Detail.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
            int _SearchID;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchID = Convert.ToInt32(Search.Text);
            Order_Detail SearchDetail = null;
            SearchDetail = testEntities.GetContext().Order_Detail.Where(b => b.order_id == _SearchID).FirstOrDefault();

            if (SearchDetail == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().Order_Detail.Where(b => b.order_id == _SearchID).ToList();
            }
        }
    }
}