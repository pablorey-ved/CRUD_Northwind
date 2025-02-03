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

namespace NorthwindCRUD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtPassword.Password;
            if (DBManager.GetUserByUsernameAndPassword(username, password) == -1)
            {
                txtError.Visibility = Visibility.Visible;
            } else
            {
                CRUDWindow crud = new CRUDWindow();
                crud.Show();
                Close();
            }
        }
    }
}
