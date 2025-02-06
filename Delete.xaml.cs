using System;
using System.Data;
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
using System.Diagnostics;

namespace NorthwindCRUD
{
    /// <summary>
    /// Lógica de interacción para Delete.xaml
    /// </summary>
    public partial class Delete : Page
    {
        public Delete()
        {
            InitializeComponent();
            CargarProductos();
            btnBorrar.IsEnabled = false;
            tbError.Visibility = Visibility.Hidden;
        }
        private void CargarProductos()
        {
            ProductosDataGrid.ItemsSource = DBManager.GetProducts().DefaultView;
        }

        private void tbNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombreProducto = tbNombre.Text.Trim();
            btnBorrar.IsEnabled = false;
            ProductosDataGrid.SelectedItem = null;

            if (string.IsNullOrEmpty(nombreProducto))
                return;

            // Iteramos sobre la fuente de datos usando var
            foreach (var item in ProductosDataGrid.ItemsSource)
            {
                // Convertimos el objeto a DataRowView
                var row = item as DataRowView;
                if (row != null)
                {
                    // Comparamos el nombre del producto sin distinguir mayúsculas/minúsculas
                    if (string.Equals(row["ProductName"].ToString(), nombreProducto, StringComparison.OrdinalIgnoreCase))
                    {
                        ProductosDataGrid.SelectedItem = row;
                        btnBorrar.IsEnabled = true;
                        ProductosDataGrid.ScrollIntoView(row);
                        break;
                    }
                }
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (tbNombre.Text.Equals("") || tbNombre.Text == "" || tbNombre.Text == null)
            {
                tbError.Text = "El nombre es obligatorio.";
                tbError.Visibility = Visibility.Visible;
            }
            else
            {
                int rows = DBManager.DeleteProduct(tbNombre.Text.Trim());
                if (rows < 0)
                {
                    tbError.Text = "Se ha producido un error.";
                    tbError.Visibility = Visibility.Visible;
                }
                else if (rows == 0)
                {
                    tbError.Text = "El producto no existe.";
                    tbError.Visibility = Visibility.Visible;
                }
                else
                {
                    tbError.Visibility = Visibility.Hidden;
                    tbNombre.Text = "";
                    CargarProductos();
                }
            }
        }
    }
}
