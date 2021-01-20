using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        List<productos> lstProducto = new List<productos>();
        string rutaImagen = Directory.GetCurrentDirectory();
        string nuevaImagen = "NoDisponible";

        public MainWindow()
        {
            InitializeComponent();
            rutaImagen = rutaImagen.Replace("bin\\Debug","Productos\\");
            if (!Directory.Exists(rutaImagen))
                Directory.CreateDirectory(rutaImagen);
            dgRegistros.AutoGenerateColumns = false;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (sonValidos())
            {
                lstProducto.Add(new productos("000000", txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()),Convert.ToDouble(txtPrecio.Text.Trim()), nuevaImagen ));
                dgRegistros.ItemsSource = lstProducto;
                dgRegistros.Items.Refresh();

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
                productos row = lstProducto[id];

                row.codigo = "00000";
                row.nombre = txtNombre.Text.Trim();
                row.cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
                row.precio = Convert.ToDouble(txtPrecio.Text.Trim());
                row.rutaImagen = nuevaImagen;

                dgRegistros.ItemsSource = lstProducto;
                dgRegistros.Items.Refresh();

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
                lstProducto.RemoveAt(id);
                dgRegistros.ItemsSource = lstProducto;
                dgRegistros.Items.Refresh();
                image.Source = null;

                if (nuevaImagen != "NoDisponible")
                    File.Delete(nuevaImagen);

                limpiarForm();
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(btnBuscar.Content.ToString() == "Buscar")
                {
                List<productos> filtro = new List<productos>();

                for (int i = 0; i < lstProducto.Count; i++)
                {
                    productos p = lstProducto[i];

                    if (p.codigo.Contains(txtCodigo.Text.Trim()) && p.nombre.Contains(txtNombre.Text.Trim()) &&
                        p.cantidad.ToString().Contains(txtCantidad.Text.Trim()) && p.precio.ToString().Contains(txtPrecio.Text.Trim()))
                        filtro.Add(p);
                }
                dgRegistros.ItemsSource = filtro;
            }
            else
            {
                dgRegistros.ItemsSource = lstProducto;
                limpiarForm();
            }

            dgRegistros.Items.Refresh();
        }

        int id = -1;
        private void dgRegistros_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            id = dgRegistros.SelectedIndex;

            productos row = (productos)dgRegistros.Items[id];
            txtCodigo.Text = row.codigo;
            txtNombre.Text = row.nombre;
            txtCantidad.Text = row.cantidad.ToString();
            txtPrecio.Text = row.precio.ToString();
            nuevaImagen = row.rutaImagen;
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
