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
using System.Windows.Shapes;

namespace _100DaysOfCode_WPF
{
    /// <summary>
    /// Lógica de interacción para Mensaje.xaml
    /// </summary>
    public partial class Mensaje : Window
    {
        static Mensaje oMensaje;
        static MessageBoxResult mbr;
        public Mensaje()
        {
            InitializeComponent();
        }

        public static MessageBoxResult Show(string texto)
        {
            oMensaje = new Mensaje();
            oMensaje.tbkMensaje.Text = texto;
            oMensaje.lblMensaje.Width = 450;
            oMensaje.lblMensaje.Margin = new Thickness(20, 25, 20, 0);
            oMensaje.imagen.Visibility = Visibility.Hidden;
            oMensaje.btnAceptar.IsDefault = true;
            oMensaje.ShowDialog();

            return mbr;
        }
        public static MessageBoxResult Show(string texto, int opcion)
        {
            oMensaje = new Mensaje();
            oMensaje.tbkMensaje.Text = texto;
            oMensaje.lblMensaje.Width = 450;
            oMensaje.lblMensaje.Margin = new Thickness(20, 30, 20, 0);
            oMensaje.imagen.Visibility = Visibility.Hidden;

            switch (opcion)
            {
                case 1:
                    oMensaje.btnAceptar.Margin = new Thickness(165, 125, 0, 0);
                    oMensaje.btnAceptar.Content = "SI";
                    oMensaje.btnNo.Margin = new Thickness(253, 125, 0, 0);
                    oMensaje.btnNo.Visibility = Visibility.Visible;
                    oMensaje.btnAceptar.IsDefault = false;
                    oMensaje.btnNo.IsDefault = true;
                    oMensaje.btnNo.Focus();
                    break;
                default:
                    oMensaje.btnAceptar.Margin = new Thickness(208, 125, 0, 0);
                    oMensaje.btnNo.Visibility = Visibility.Hidden;
                    oMensaje.btnAceptar.IsDefault = true;
                    break;
            }
            oMensaje.ShowDialog();
            return mbr;
        }
        public static MessageBoxResult Show(string texto, int opcion, int icono)
        {
            string rutaImagen = Directory.GetCurrentDirectory().Replace("bin\\Debug", "Iconos\\");

            oMensaje = new Mensaje();
            oMensaje.tbkMensaje.Text = texto;

            switch (opcion)
            {
                case 1:
                    oMensaje.btnAceptar.Margin = new Thickness(165, 125, 0, 0);
                    oMensaje.btnAceptar.Content = "SI";
                    oMensaje.btnNo.Margin = new Thickness(253, 125, 0, 0);
                    oMensaje.btnNo.Visibility = Visibility.Visible;
                    oMensaje.btnAceptar.IsDefault = false;
                    oMensaje.btnNo.IsDefault = true;
                    oMensaje.btnNo.Focus();
                    break;
                default:
                    oMensaje.btnAceptar.Margin = new Thickness(208, 125, 0, 0);
                    oMensaje.btnNo.Visibility = Visibility.Hidden;
                    oMensaje.btnAceptar.IsDefault = true;
                    break;
            }
            switch (icono)
            {
                case 1:
                    oMensaje.imagen.Source = oMensaje.getImagenUri(rutaImagen + "warning.png");
                    break;
                default:
                    oMensaje.imagen.Source = oMensaje.getImagenUri(rutaImagen + "info.png");
                    break;
            }
            oMensaje.ShowDialog();
            return mbr;
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(oMensaje.btnAceptar.Content.ToString() == "OK")
            {
                mbr = MessageBoxResult.OK;
            }
            else
            {
                mbr = MessageBoxResult.Yes;
            }
            DialogResult = true;
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            mbr = MessageBoxResult.No;
            DialogResult = false;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mbr == MessageBoxResult.None)
            mbr = MessageBoxResult.Cancel;
        }
        public BitmapImage getImagenUri(string url)
        {
            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(url);
            bi.EndInit();

            return bi;
        }
    }
}
