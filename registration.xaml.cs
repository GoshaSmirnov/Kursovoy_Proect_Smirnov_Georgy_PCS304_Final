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
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        private int reg = 0;
        private client _curretClient = new client();
        int maxid = testEntities.GetContext().client.Max(u => u.client_id);
        public registration(client SelectClient)
        {
            InitializeComponent();
            if (SelectClient != null)
            {
                _curretClient = SelectClient;
                reg = 1;
                ClientPasswordText.Password = _curretClient.client_password;
            }
            else
            {
                _curretClient.client_id = maxid + 1;
            }
            DataContext = _curretClient;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _curretClient.client_password = ClientPasswordText.Password;
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_curretClient.client_surname)) errors.AppendLine("Введите фамилию");
            if (string.IsNullOrWhiteSpace(_curretClient.client_name)) errors.AppendLine("Введите имя");
            if (string.IsNullOrWhiteSpace(_curretClient.client_login)) errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(_curretClient.client_password)) errors.AppendLine("Введите пароль");
            //проверка пороля:
            string str2; int i = 0; bool boo; int yes = 0;
            if (_curretClient.client_password.Length < 6) errors.AppendLine("Пароль должен быть длиннее 6 символов"); 
            str2 = _curretClient.client_password.ToLower();
            if (_curretClient.client_password == str2) errors.AppendLine("В пароле должны быть большие буквы");
            char[] array = _curretClient.client_password.ToCharArray();
            while (_curretClient.client_password.Length > i)
            {
                boo = Char.IsDigit(array[i]);
                if (boo == true) yes = yes + 1;
                i = i + 1;
            }
            if (_curretClient.client_password.Length <= yes) errors.AppendLine("Пароль должен включать в себя ещё и буквы, большие и маленькие");
            if (yes == 0) errors.AppendLine("Пароль должен включать в себя ещё и цифры");
            //проверки длины:
            if (_curretClient.client_surname != null)
            {
                if (_curretClient.client_surname.Length > 40) errors.AppendLine("Фамилия должна быть короче 40 символов");
            }
            if (_curretClient.client_name != null)
            {
                if (_curretClient.client_name.Length > 40) errors.AppendLine("имя должно быть короче 40 символов");
            }
            if (_curretClient.client_pastname != null)
            {
                if (_curretClient.client_pastname.Length > 40) errors.AppendLine("отчество должна быть короче 40 символов");
            }
            if (_curretClient.client_login != null)
            {
                if (_curretClient.client_login.Length > 70) errors.AppendLine("Логин должна быть короче 70 символов");
            }
            if (_curretClient.client_password != null)
            {
                if (_curretClient.client_password.Length > 70) errors.AppendLine("Пароль должен быть короче 70 символов");
            }
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }
            
            if (reg == 0) testEntities.GetContext().client.Add(_curretClient);
            else
            {
                var clnts = testEntities.GetContext().client.Where(b => b.client_id == _curretClient.client_id).FirstOrDefault();
                clnts.client_login = _curretClient.client_login;
                clnts.client_password = _curretClient.client_password;
                clnts.client_name = _curretClient.client_name;
                clnts.client_surname = _curretClient.client_surname;
                clnts.client_pastname = _curretClient.client_pastname;
            }
            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Вы зарегестрированы!");
                else MessageBox.Show("Данные изменены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
