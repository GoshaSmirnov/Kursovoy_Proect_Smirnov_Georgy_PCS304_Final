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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        public Client()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().client.ToList();

        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().client.ToList();
            }
        }
        public void AddClick(object sender, EventArgs e)
        {
            Manager.MainFrame.Navigate(new registration(null));
        }
        public void EditClick(object sender, EventArgs e)
        {
            var ClientForUpdate = Aptek.SelectedItems.Cast<client>().FirstOrDefault();
            Manager.MainFrame.Content = new registration(ClientForUpdate);
        }

        string _SearchName;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchName = Search.Text;
            client SearchGood = null;
            SearchGood = testEntities.GetContext().client.Where(b => b.client_surname == _SearchName).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().client.Where(b => b.client_surname == _SearchName).ToList();
            }
        }
        public void BackClick(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<client>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().client.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().client.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}