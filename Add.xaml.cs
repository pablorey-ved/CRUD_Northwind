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
    /// Lógica de interacción para Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add()
        {
            InitializeComponent();
            CargarProductos();
            CargarCategorías();
            tbError.Visibility = Visibility.Hidden;
        }

        private void CargarProductos()
        {
            ProductosDataGrid.ItemsSource = DBManager.GetProducts().DefaultView;
        }

        private void CargarCategorías()
        {
            cbCategoria.ItemsSource = DBManager.GetCategories().DefaultView;
            cbCategoria.DisplayMemberPath = "CategoryName";
            cbCategoria.SelectedValuePath = "CategoryID";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text.Equals("") || tbNombre.Text == null || cbCategoria.SelectedItem == null || cbCategoria.SelectedItem.ToString().Equals("")) {
                tbError.Text = "El nombre y la categoría son obligatorios.";
                tbError.Visibility = Visibility.Visible;
                return;
            } else
            {
                int result = DBManager.InsertProduct(tbNombre.Text, (int)cbCategoria.SelectedValue);
                if (result == -1)
                {
                    tbError.Text = "Se ha producido un error.";
                    tbError.Visibility = Visibility.Visible;
                } else
                {
                    CargarProductos();
                    tbNombre.Clear();
                    cbCategoria.SelectedItem = null;
                    tbError.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
