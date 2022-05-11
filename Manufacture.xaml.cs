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
    /// Логика взаимодействия для Manufacture.xaml
    /// </summary>
    public partial class Manufacture : Page
    {
        public Manufacture()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().manufacturer.ToList();

        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().manufacturer.ToList();
            }
        }
        public void AddClick(object sender, EventArgs e)
        {
            Manager.MainFrame.Navigate(new ManufacturerADD(null));
        }
        public void EditClick(object sender, EventArgs e)
        {
            var ManufactureForUpdate = Aptek.SelectedItems.Cast<manufacturer>().FirstOrDefault();
            Manager.MainFrame.Content = new ManufacturerADD(ManufactureForUpdate);
        }

        string _SearchName;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchName = Search.Text;
            manufacturer SearchGood = null;
            SearchGood = testEntities.GetContext().manufacturer.Where(b => b.manufacturer1 == _SearchName).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().manufacturer.Where(b => b.manufacturer1 == _SearchName).ToList();
            }
        }

        public void BackClick(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<manufacturer>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().manufacturer.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().manufacturer.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
