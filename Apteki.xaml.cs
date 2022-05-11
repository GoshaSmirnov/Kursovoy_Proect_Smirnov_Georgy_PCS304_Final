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
    /// Логика взаимодействия для Apteki.xaml
    /// </summary>
    public partial class Apteki : Page
    {
        public Apteki()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().goods.ToList();
            
        }
    private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().goods.ToList();
            }
        }
    public void AddClick (object sender, EventArgs e)
        {
            MainFrame.Content = new ADDpage(null);
           
        }
    public void EditClick(object sender, EventArgs e)
        {
            var clientForUpdate = Aptek.SelectedItems.Cast<goods>().FirstOrDefault();
            MainFrame.Content = new ADDpage(clientForUpdate);
        }

        public void Back_Click(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<goods>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().goods.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().goods.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        string _SearchName;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _SearchName = Search.Text;
            goods SearchGood = null;
            SearchGood = testEntities.GetContext().goods.Where(b => b.goods_name == _SearchName).FirstOrDefault();

            if (SearchGood == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().goods.Where(b => b.goods_name == _SearchName).ToList();
            }
        }
    }
}
