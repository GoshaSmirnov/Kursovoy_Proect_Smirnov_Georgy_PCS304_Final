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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Manager.MainFrame = MainFrame;

        }

    public void ButtonClick(object sender, EventArgs e)
        {

            if (authorization.index == 0) {
                MainFrame.Content = new authorization();
            }
            else if (authorization.index == -1) MainFrame.Content = new table();
            else MainFrame.Content = new clientMenu();
        }

        public void BackClick(object sender, EventArgs e)
        {
            if (MainFrame.Content != null)
            {
                if (authorization.index > 0) MainFrame.Content = new clientMenu();
                else if (authorization.index == 0) MainFrame.Content = null;
                if (authorization.index == -1) MainFrame.Content = new table();
            }
        }

        private void TablClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new table();
        }
    }
}
