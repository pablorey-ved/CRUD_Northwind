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

namespace NorthwindCRUD
{
    /// <summary>
    /// Lógica de interacción para Update.xaml
    /// </summary>
    public partial class Update : Page
    {
        public Update()
        {
            InitializeComponent();
            CargarCategorías();
            CargarProductos();
            cbCategoria.IsEnabled = false;
            tbError.Visibility = Visibility.Hidden;
        }

        private void tbOldName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nombreProducto = tbOldName.Text.Trim();
            ProductosDataGrid.SelectedItem = null;

            if (string.IsNullOrEmpty(nombreProducto))
                return;
            
            int cat = DBManager.GetCategoryIDByProductName(nombreProducto);
            if (cat <= 0)
            {
                cbCategoria.SelectedItem = null;
                cbCategoria.IsEnabled = false;
            } else
            {
                cbCategoria.SelectedValue = cat;
                cbCategoria.IsEnabled = true;
            }

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
                        ProductosDataGrid.ScrollIntoView(row);
                        break;
                    }
                }
            }
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
            if (tbOldName.Text.Equals("") || tbOldName.Text == null || tbNewProduct.Text.Equals("") || tbNewProduct.Text == null || cbCategoria.SelectedItem == null || cbCategoria.SelectedItem.ToString().Equals(""))
            {
                tbError.Text = "Los nombres y la categoría son obligatorios.";
                tbError.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                int result = DBManager.UpdateProduct(tbNewProduct.Text, tbOldName.Text, (int)cbCategoria.SelectedValue);
                if (result == -1)
                {
                    tbError.Text = "Se ha producido un error.";
                    tbError.Visibility = Visibility.Visible;
                }
                else
                {
                    CargarProductos();
                    tbNewProduct.Clear();
                    tbOldName.Clear();
                    cbCategoria.SelectedItem = null;
                    tbError.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
