using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphote
{
    public partial class MainWindow : Window
    {
      List<FiguraTridimensional> Figuras = new List<FiguraTridimensional>();
      
      FiguraTridimensional? FiguraSeleccionada = null;
      public MainWindow()
      {
          //vista = new VistaTridimensional(pic_canvas.Width, pic_canvas.Height);
          InitializeComponent();
      }

      private void lbl_seleccionar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
        if(FiguraSeleccionada != null) 
          grid_transofrmaciones.Visibility = Visibility.Visible;
        else
          grid_transofrmaciones.Visibility = Visibility.Hidden;
      }

      private void lbl_seleccionar_reiniciar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {

      }

      private void lbl_seleccionar_salir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
        this.Close();
      }

      private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
      {

      }

      private void slider_rotarX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
      {

      }

      private void slider_rotarY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
      {

      }

      private void slider_rotarZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
      {

      }

      private void btn_focusZ_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {

      }

      private void btn_focusX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {

      }

      private void btn_focusY_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {

      }

      private void pic_canvas_MouseDown(object sender, MouseButtonEventArgs e)
      {
        Renderizador render = new Renderizador(pic_canvas);
        FiguraTridimensional cubo = FabricaFiguras.CrearCubo();
        Figuras.Add(cubo);
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.renderTarget;
      }
  }
}
