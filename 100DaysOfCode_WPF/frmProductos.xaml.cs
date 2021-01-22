using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace _100DaysOfCode_WPF
{
    /// <summary>
    /// Lógica de interacción para el CRUD.
    /// </summary>
    public partial class MainWindow : Window
    {
        model_productos mProducto = new model_productos();
        string rutaImagen = Directory.GetCurrentDirectory();
        string nuevaImagen = "NoDisponible";

        public MainWindow()
        {
            InitializeComponent();
            rutaImagen = rutaImagen.Replace("bin\\Debug","Productos\\");
            if (!Directory.Exists(rutaImagen))
                Directory.CreateDirectory(rutaImagen);
            dgRegistros.AutoGenerateColumns = false;
            dgRegistros.ItemsSource = mProducto.BuscarProductos(new productos()).DefaultView;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (sonValidos())
            {
                mProducto.AgregarProducto(new productos("000000", txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()),Convert.ToDouble(txtPrecio.Text.Trim()), nuevaImagen ));
                dgRegistros.ItemsSource = mProducto.BuscarProductos(new productos()).DefaultView;

                if (nuevaImagen != "NoDisponible")
                    guardarImagen(nuevaImagen.Split('.')[1]);

                limpiarForm();
            }
            else
            {
                MessageBox.Show("Verifique los campos");
            }

        }
        private void btnModiicar_Click(object sender, RoutedEventArgs e)
        {
            if (sonValidos())
            {
                productos oProducto = new productos(txtCodigo.Text.Trim(), txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim()),nuevaImagen);
                oProducto.id = id;

                mProducto.ModificarProducto(oProducto);

                dgRegistros.ItemsSource = mProducto.BuscarProductos(new productos()).DefaultView;

                if (nuevaImagen != "NoDisponible")
                    guardarImagen(nuevaImagen.Split('.')[1]);

                limpiarForm();
            }
            else
            {
                MessageBox.Show("Verifique los campos");
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(id > -1)
            {
                mProducto.EliminarProducto(id);
                dgRegistros.ItemsSource = mProducto.BuscarProductos(new productos()).DefaultView;

                if (nuevaImagen != "NoDisponible")
                    File.Delete(nuevaImagen);

                limpiarForm();
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(btnBuscar.Content.ToString() == "Buscar")
            {
                dgRegistros.ItemsSource = mProducto.BuscarProductos(new productos(txtCodigo.Text.Trim(),txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim() == "" ? "0" : txtCantidad.Text.Trim()),Convert.ToDouble(txtPrecio.Text.Trim() == "" ? "0": txtPrecio.Text.Trim()),"")).DefaultView;
            }
            else
            {
                dgRegistros.ItemsSource = mProducto.BuscarProductos(new productos()).DefaultView;
                limpiarForm();
            }
        }

        int id = -1;
        private void dgRegistros_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            DataRowView row = (DataRowView)dgRegistros.Items[dgRegistros.SelectedIndex];

            id = Convert.ToInt32(row["id"]);
            txtCodigo.Text = row["codigo"].ToString();
            txtNombre.Text = row["nombre"].ToString();
            txtCantidad.Text = row["cantidad"].ToString();
            txtPrecio.Text = row["precio"].ToString();
            nuevaImagen = row["rutaImagen"].ToString();
            if (nuevaImagen != "NoDisponible")
            {
                image.Source = getImagenUri(nuevaImagen);
                lblNoDisponible.Visibility = Visibility.Hidden;
            }
            else
            {
                lblNoDisponible.Visibility = Visibility.Visible;
                image.Source = null;
            }
            btnAgregar.IsEnabled = false;
            btnModiicar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnBuscar.Content = "Limpiar";
        }

        private void btnSubirImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdImagen = new OpenFileDialog();

            if (Convert.ToBoolean(ofdImagen.ShowDialog()))
            {
                nuevaImagen = rutaImagen + ofdImagen.SafeFileName;
                image.Source = new BitmapImage(new Uri(ofdImagen.FileName));
                lblNoDisponible.Visibility = Visibility.Hidden;
            }
            else
            {
                image.Source = null;
                nuevaImagen = "NoDisponible";
                lblNoDisponible.Visibility = Visibility.Visible;
            }
        }

        public bool sonValidos()
        {
            if (txtNombre.Text.Trim() != "" && txtCantidad.Text.Trim() != "" && txtPrecio.Text.Trim() != "")
                return true;
            return false;
        }
        public void limpiarForm()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            btnAgregar.IsEnabled = true;
            btnModiicar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnBuscar.Content = "Buscar";
            image.Source = null;
            nuevaImagen = "NoDisponible";
            id = -1;
            lblNoDisponible.Visibility = Visibility.Hidden;
        }
        public void guardarImagen(string extension)
        {
            FileStream fstream;

            if (extension == "jpg")
            {
                JpegBitmapEncoder jbe = new JpegBitmapEncoder();
                jbe.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
                fstream = new FileStream(nuevaImagen, FileMode.Create);
                jbe.Save(fstream);
            }
            else
            {
                PngBitmapEncoder pbe = new PngBitmapEncoder();
                pbe.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
                fstream = new FileStream(nuevaImagen, FileMode.Create);
                pbe.Save(fstream);
            }
           fstream.Close();
        }
        public BitmapImage getImagenUri(string url)
        {
            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(nuevaImagen);
            bi.EndInit();

            return bi;
        }

        private void frmProductos_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Esta por abandonar el programa ¿Desea continuar?","Mensaje del sistema",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

        }
    }
}
