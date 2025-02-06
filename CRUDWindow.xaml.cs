using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace NorthwindCRUD
{
    /// <summary>
    /// Lógica de interacción para CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        public CRUDWindow()
        {
            InitializeComponent();

            btnInicio.IsEnabled = false;
            btnAgregar.IsEnabled = true;
            btnModificar.IsEnabled = true;
            btnBorrar.IsEnabled = true;

            homeImg.Style = null;
            addImg.Style = (Style)FindResource("ImageHover");
            updateImg.Style = (Style)FindResource("ImageHover");
            deleteImg.Style = (Style)FindResource("ImageHover");

            MainFrame.Navigate(new Home());

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            DBManager.CloseConnection();
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                string tag = btn.Tag.ToString();
                NavigateToSection(tag);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DBManager.CloseConnection();
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image btn && btn.Tag != null)
            {
                string tag = btn.Tag.ToString();
                NavigateToSection(tag);
            }
        }

        private void NavigateToSection(string tag)
        {
            // Verifica si la página actual ya es la deseada.
            if ((tag == "InicioPage" && MainFrame.Content is Home) ||
                (tag == "AgregarPage" && MainFrame.Content is Add) ||
                (tag == "ModificarPage" && MainFrame.Content is Update) ||
                (tag == "BorrarPage" && MainFrame.Content is Delete))
            {
                // Si ya estamos en esa sección, no hacemos nada.
                return;
            }

            // Actualiza el estado de los botones y navega a la sección correspondiente.
            switch (tag)
            {
                case "InicioPage":
                    btnInicio.IsEnabled = false;
                    btnAgregar.IsEnabled = true;
                    btnModificar.IsEnabled = true;
                    btnBorrar.IsEnabled = true;

                    homeImg.Style = null;
                    addImg.Style = (Style) FindResource("ImageHover");
                    updateImg.Style = (Style) FindResource("ImageHover");
                    deleteImg.Style = (Style) FindResource("ImageHover");

                    MainFrame.Navigate(new Home());
                    break;
                case "AgregarPage":
                    btnInicio.IsEnabled = true;
                    btnAgregar.IsEnabled = false;
                    btnModificar.IsEnabled = true;
                    btnBorrar.IsEnabled = true;

                    homeImg.Style = (Style)FindResource("ImageHover");
                    addImg.Style = null;
                    updateImg.Style = (Style)FindResource("ImageHover");
                    deleteImg.Style = (Style)FindResource("ImageHover");

                    MainFrame.Navigate(new Add());
                    break;
                case "ModificarPage":
                    btnInicio.IsEnabled = true;
                    btnAgregar.IsEnabled = true;
                    btnModificar.IsEnabled = false;
                    btnBorrar.IsEnabled = true;

                    homeImg.Style = (Style)FindResource("ImageHover");
                    addImg.Style = (Style)FindResource("ImageHover");
                    updateImg.Style = null;
                    deleteImg.Style = (Style)FindResource("ImageHover");

                    MainFrame.Navigate(new Update());
                    break;
                case "BorrarPage":
                    btnInicio.IsEnabled = true;
                    btnAgregar.IsEnabled = true;
                    btnModificar.IsEnabled = true;
                    btnBorrar.IsEnabled = false;

                    homeImg.Style = (Style)FindResource("ImageHover");
                    addImg.Style = (Style)FindResource("ImageHover");
                    updateImg.Style = (Style)FindResource("ImageHover");
                    deleteImg.Style = null;

                    MainFrame.Navigate(new Delete());
                    break;
            }
        }

    }
}
