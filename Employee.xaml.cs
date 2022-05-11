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
    /// Логика взаимодействия для Employee.xaml
    /// </summary>
    public partial class Employee : Page
    {
        public Employee()
        {
            InitializeComponent();
            Aptek.ItemsSource = testEntities.GetContext().employee.ToList();

        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                testEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                Aptek.ItemsSource = testEntities.GetContext().employee.ToList();
            }
        }
        public void AddClick(object sender, EventArgs e)
        {
            Manager.MainFrame.Navigate(new registrationEmployee(null));
        }
        public void EditClick(object sender, EventArgs e)
        {
            var EmployeeForUpdate = Aptek.SelectedItems.Cast<employee>().FirstOrDefault();
            var EmployeeForRemoving = Aptek.SelectedItems.Cast<employee>().ToList();
            testEntities.GetContext().employee.RemoveRange(EmployeeForRemoving);
            testEntities.GetContext().SaveChanges();
            Manager.MainFrame.Content = new registrationEmployee(EmployeeForUpdate);
        }
        public void BackClick(object sender, EventArgs e)
        {
            var clientForRemoving = Aptek.SelectedItems.Cast<employee>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "Внимание!",
            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    testEntities.GetContext().employee.RemoveRange(clientForRemoving);
                    testEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Aptek.ItemsSource = testEntities.GetContext().employee.ToList();
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
            employee SearchEmployee = null;
            SearchEmployee = testEntities.GetContext().employee.Where(b => b.employee_surname == _SearchName).FirstOrDefault();

            if (SearchEmployee == null) MessageBox.Show("Не найдено");
            else
            {
                Aptek.ItemsSource = testEntities.GetContext().employee.Where(b => b.employee_surname == _SearchName).ToList();
            }
        }
    }
}
