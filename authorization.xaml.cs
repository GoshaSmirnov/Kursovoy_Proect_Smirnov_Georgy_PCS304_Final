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
    /// Логика взаимодействия для authorization.xaml
    /// </summary>
    public partial class authorization : Page
    {
        public static int index = 0;
        public static string Surname, ClientName, PastName;
        string _login, _password;

        public authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client User = null;

            _login = LoginText.Text;
            _password = PasswordText.Password;
            User = testEntities.GetContext().client.Where(b => b.client_password == _password && b.client_login == _login).FirstOrDefault();

            if (User == null) MessageBox.Show("Не найдено");
            else
            {
                MessageBox.Show("Успешно");
                index = User.client_id;
                Surname = User.client_surname;
                Surname = Surname.Replace(" ", "");
                ClientName = User.client_name;
                ClientName = ClientName.Replace(" ", "");
                PastName = User.client_pastname;
                PastName = PastName.Replace(" ", "");
                Manager.MainFrame.Content = new clientMenu();
            }
        }
        private void Sotrudnik_Click(object sender, RoutedEventArgs e)
        {
            employee User = null;

            _login = LoginText.Text;
            _password = PasswordText.Password;
            User = testEntities.GetContext().employee.Where(b => b.employee_password == _password && b.employee_login == _login).FirstOrDefault();

            if (User == null) MessageBox.Show("Не найдено");
            else
            {
                MessageBox.Show("Успешно");
                index = -1;
                Manager.MainFrame.Content = new table();
            }
        }


        private void Registr_Click (object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Content = new registration(null);
        }
    }
}
