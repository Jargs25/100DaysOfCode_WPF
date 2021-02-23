using _100DaysOfCode_WPF.WCFProductos;
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
        WCFProductoClient svcProducto = new WCFProductoClient();
        Producto[] aryProductos;
        string nuevaImagen = "NoDisponible";

        public MainWindow()
        {
            InitializeComponent();

            dgRegistros.AutoGenerateColumns = false;
            aryProductos = svcProducto.BuscarProductos(GetProducto());
            dgRegistros.ItemsSource = aryProductos;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (sonValidos())
            {
                Producto oProducto = GetProducto(txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim()));
                oProducto.rutaImagen = nuevaImagen;

                if (nuevaImagen != "NoDisponible")
                    oProducto.imagen = GetByteArrayImage(image.Source, nuevaImagen.Split('.')[1]);

                Mensaje.Show(svcProducto.AgregarProducto(oProducto));

                limpiarForm();
            }
            else
            {
                Mensaje.Show("Verifique los campos", 0, 0);
            }
        }
        private void btnModiicar_Click(object sender, RoutedEventArgs e)
        {
            if (sonValidos())
            {
                Producto oProducto = GetProducto(txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim()));
                oProducto.rutaImagen = nuevaImagen;
                oProducto.id = id;

                if (nuevaImagen != "NoDisponible")
                    oProducto.imagen = GetByteArrayImage(image.Source, nuevaImagen.Split('.')[1]);

                Mensaje.Show(svcProducto.ModificarProducto(oProducto));

                limpiarForm();
            }
            else
            {
                Mensaje.Show("Verifique los campos", 0, 0);
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Mensaje.Show("Esta por eliminar el registro ¿Desea continuar?", 1, 1);
            if (result == MessageBoxResult.Yes)
            {
                Producto oProducto = GetProducto();
                oProducto.id = id;
                oProducto.rutaImagen = nuevaImagen;

                Mensaje.Show(svcProducto.EliminarProducto(oProducto));

                limpiarForm();
            }
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (btnBuscar.Content.ToString() == "Buscar")
            {
                Producto oProducto = GetProducto(txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim() == "" ? "0" : txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim() == "" ? "0" : txtPrecio.Text.Trim()));
                oProducto.codigo = txtCodigo.Text.Trim();

                aryProductos = svcProducto.BuscarProductos(oProducto);
                dgRegistros.ItemsSource = aryProductos;
            }
            else
            {
                limpiarForm();
            }
        }
        private void btnSubirImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdImagen = new OpenFileDialog();

            if (Convert.ToBoolean(ofdImagen.ShowDialog()))
            {
                nuevaImagen = ofdImagen.SafeFileName;
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

        int id = -1;
        private void dgRegistros_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (dgRegistros.SelectedIndex > -1)
            {
                Producto selected = aryProductos[dgRegistros.SelectedIndex];

                id = selected.id;
                txtCodigo.Text = selected.codigo;
                txtNombre.Text = selected.nombre;
                txtCantidad.Text = selected.cantidad.ToString();
                txtPrecio.Text = selected.precio.ToString();
                nuevaImagen = selected.rutaImagen;

                if (nuevaImagen != "NoDisponible")
                {
                    image.Source = GetImagenStream(selected.imagen);
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
        }

        private void frmProductos_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = Mensaje.Show("Esta por abandonar el programa ¿Desea continuar?",1,1);
            if (result == MessageBoxResult.No || result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
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
            aryProductos = svcProducto.BuscarProductos(GetProducto());
            dgRegistros.ItemsSource = aryProductos;
            id = -1;
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
            lblNoDisponible.Visibility = Visibility.Hidden;
        }
        public BitmapImage GetImagenStream(byte[] image)
        {
            MemoryStream ms = new MemoryStream(image);
            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.StreamSource = ms;
            bi.EndInit();

            return bi;
        }
        private byte[] GetByteArrayImage(ImageSource imgSource, string extension)
        {
            MemoryStream ms = new MemoryStream();

            if (extension == "jpg")
            {
                JpegBitmapEncoder jbe = new JpegBitmapEncoder();
                jbe.Frames.Add(BitmapFrame.Create((BitmapSource)imgSource));
                jbe.Save(ms);
            }
            else
            {
                PngBitmapEncoder pbe = new PngBitmapEncoder();
                pbe.Frames.Add(BitmapFrame.Create((BitmapSource)imgSource));
                pbe.Save(ms);
            }

            return ms.ToArray();
        }

        private Producto GetProducto()
        {
            Producto oProducto = new Producto();

            oProducto.id = 0;
            oProducto.codigo = "";
            oProducto.nombre = "";
            oProducto.cantidad = 0;
            oProducto.precio = 0;
            oProducto.rutaImagen = "NoDisponible";

            return oProducto;
        }
        private Producto GetProducto(string nombre, int cantidad, double precio)
        {
            Producto oProducto = new Producto();

            oProducto.id = 0;
            oProducto.codigo = "";
            oProducto.nombre = nombre;
            oProducto.cantidad = cantidad;
            oProducto.precio = precio;
            oProducto.rutaImagen = "NoDisponible";

            return oProducto;
        }
    }
}
