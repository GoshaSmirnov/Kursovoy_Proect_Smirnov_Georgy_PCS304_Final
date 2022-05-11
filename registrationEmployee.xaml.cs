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
    /// Логика взаимодействия для registrationEmployee.xaml
    /// </summary>
    public partial class registrationEmployee : Page
    {
        private int reg = 0;
        private employee _curretEmployee = new employee();
        int maxid = testEntities.GetContext().employee.Max(u => u.employee_id);
        public registrationEmployee(employee SelectEmployee)
        {
            InitializeComponent();
            if (SelectEmployee != null)
            {
                _curretEmployee = SelectEmployee;
                reg = 1;
            }
            else 
            { 
                _curretEmployee.employee_id = maxid + 1; 
            }
            DataContext = _curretEmployee;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_curretEmployee.employee_surname)) errors.AppendLine("Введите фамилию");
            if (string.IsNullOrWhiteSpace(_curretEmployee.employee_name)) errors.AppendLine("Введите имя");
            if (string.IsNullOrWhiteSpace(_curretEmployee.employee_login)) errors.AppendLine("Введите логин");
            if (string.IsNullOrWhiteSpace(_curretEmployee.employee_password)) errors.AppendLine("Введите пароль");
            if (errors.Length > 0) { MessageBox.Show(errors.ToString()); return; }

            if (reg == 0) testEntities.GetContext().employee.Add(_curretEmployee);
            else
            {
                var empls = testEntities.GetContext().employee.Where(b => b.employee_id == _curretEmployee.employee_id).FirstOrDefault();
                empls.employee_login = _curretEmployee.employee_login;
                empls.employee_password = _curretEmployee.employee_password;
                empls.employee_name = _curretEmployee.employee_name;
                empls.employee_surname = _curretEmployee.employee_surname;
                empls.employee_pastname = _curretEmployee.employee_pastname;
            }

            try
            {
                testEntities.GetContext().SaveChanges();
                if (reg == 0) MessageBox.Show("Сотрудник добавлен!");
                else MessageBox.Show("Сотрудник изменён!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
