using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace Graphote
{
  public partial class MainWindow : Window
  {
    List<FiguraTridimensional> Figuras = new List<FiguraTridimensional>();
    Renderizador render;
    FiguraTridimensional? FiguraSeleccionada = null;
    Vector3[] VerticesOriginalesFigura;
    bool seleccionar = false;

    public MainWindow()
    {
      //vista = new VistaTridimensional(pic_canvas.Width, pic_canvas.Height);
      InitializeComponent();
      render = new Renderizador(pic_canvas);
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void lbl_seleccionar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (!seleccionar)
      {
        seleccionar = true;
        lbl_seleccionar.Background = new SolidColorBrush(
            (Color)ColorConverter.ConvertFromString("#457B9D")
        );
      }
      else
      {
        seleccionar = false;
        grid_transofrmaciones.Visibility = Visibility.Hidden;
        lbl_seleccionar.Background = null;
      }

    }

    private void lbl_seleccionar_reiniciar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      Figuras.Clear();
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void lbl_seleccionar_salir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      this.Close();
    }

    private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (FiguraSeleccionada != null)
      {
        if (VerticesOriginalesFigura != null)
        {
          FiguraSeleccionada.Vertices = VerticesOriginalesFigura.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
        }
        FiguraSeleccionada.Escalar((float)e.NewValue);
        if (lbl_escala_value != null)
        {
          lbl_escala_value.Content = e.NewValue.ToString("0.00");
        }
        render.Renderizar(Figuras, pic_canvas.Camara);
      }
    }

    private void slider_rotarX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (FiguraSeleccionada != null)
      {
        if (VerticesOriginalesFigura != null)
        {
          FiguraSeleccionada.Vertices = VerticesOriginalesFigura.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
        }
        FiguraSeleccionada.Rotar((float)e.NewValue, 'X');
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;
      }
    }

    private void slider_rotarY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (FiguraSeleccionada != null)
      {
        if (VerticesOriginalesFigura != null)
        {
          FiguraSeleccionada.Vertices = VerticesOriginalesFigura.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
        }
        FiguraSeleccionada.Rotar((float)e.NewValue, 'Y');
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;
      }
    }

    private void slider_rotarZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
      if (FiguraSeleccionada != null)
      {
        if (VerticesOriginalesFigura != null)
        {
          FiguraSeleccionada.Vertices = VerticesOriginalesFigura.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
        }
        FiguraSeleccionada.Rotar((float)e.NewValue, 'Z');
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;
      }
    }

    private void btn_focusZ_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      pic_canvas.Camara.Posicion = new Vector3(0, 0, pic_canvas.Camara.Posicion.Length());
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void btn_focusX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      pic_canvas.Camara.Posicion = new Vector3(pic_canvas.Camara.Posicion.Length(), 0, 0);
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void btn_focusY_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      pic_canvas.Camara.Posicion = new Vector3(0, pic_canvas.Camara.Posicion.Length(), 0);
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void opcion_cilindro_MouseDown(object sender, MouseButtonEventArgs e)
    {
      FiguraTridimensional cilindro = FabricaFiguras.CrearCilindro();
      Figuras.Add(cilindro);
      //render.Renderizar(Figuras, pic_canvas.Camara);
      //image_3D.Source = render.RenderTarget;
      lbl_añadir.Background = null;
    }

    private void opcion_piramide_MouseDown(object sender, MouseButtonEventArgs e)
    {
      FiguraTridimensional piramide = FabricaFiguras.CrearPiramide();
      Figuras.Add(piramide);
      grid_figuras.Visibility = Visibility.Hidden;
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
      lbl_añadir.Background = null;
    }

    private void opcion_cubo_MouseDown(object sender, MouseButtonEventArgs e)
    {
      FiguraTridimensional cubo = FabricaFiguras.CrearCubo();
      Figuras.Add(cubo);
      grid_figuras.Visibility = Visibility.Hidden;
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
      lbl_añadir.Background = null;
    }

    private void opcion_esfera_MouseDown(object sender, MouseButtonEventArgs e)
    {
      FiguraTridimensional esfera = FabricaFiguras.CrearEsfera();
      Figuras.Add(esfera);
      grid_figuras.Visibility = Visibility.Hidden;
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
      lbl_añadir.Background = null;
    }

    private void lbl_añadir_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (grid_figuras.Visibility == Visibility.Visible)
      {
        lbl_añadir.Background = null;
        grid_figuras.Visibility = Visibility.Hidden;
      }

      else
      {
        grid_figuras.Visibility = Visibility.Visible;
        lbl_añadir.Background = new SolidColorBrush(
                  (Color)ColorConverter.ConvertFromString("#457B9D"));
      }
    }

    private void pic_canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      Point posicionClic = e.GetPosition(pic_canvas);

      render.Renderizar(Figuras, pic_canvas.Camara);

      // 2. Obtener figura seleccionada
      FiguraTridimensional? FiguraEnClick = ControladorInterfaz.SeleccionarFiguraPorColor(
          posicionClic,
          Figuras,
          (int)pic_canvas.ActualWidth,
          (int)pic_canvas.ActualHeight,
          render.RenderTarget
      );

      if (FiguraEnClick != null)
      {
        FiguraSeleccionada = FiguraEnClick;
        grid_transofrmaciones.Visibility = Visibility.Visible;

        VerticesOriginalesFigura = FiguraSeleccionada.Vertices.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
        // Opcional: Resaltar la figura seleccionada
      }

    }

    private Point _puntoAnteriorMouse;
    private bool _estaMoviendoCamara = false;

    private void pic_canvas_MouseMove(object sender, MouseEventArgs e)
    {
      if (_estaMoviendoCamara && e.MiddleButton == MouseButtonState.Pressed)
      {
        Point posicionActual = e.GetPosition(pic_canvas);
        Vector2 delta = new Vector2(
            (float)(posicionActual.X - _puntoAnteriorMouse.X),
            (float)(posicionActual.Y - _puntoAnteriorMouse.Y)
        );

        // Ajustar sensibilidad (0.01f es un valor recomendado)
        float sensibilidad = 0.10f;
        delta *= sensibilidad;

        // Obtener ejes de la cámara
        Vector3 adelante = Vector3.Normalize(Vector3.Zero - pic_canvas.Camara.Posicion);
        Vector3 derecha = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, adelante));
        Vector3 arriba = Vector3.Cross(adelante, derecha);

        // Mover la cámara en el plano local (pan)
        pic_canvas.Camara.Posicion += derecha * delta.X + arriba * delta.Y;

        // Actualizar el renderizado
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;

        _puntoAnteriorMouse = posicionActual;
      }
    }

    private void pic_canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (e.MiddleButton == MouseButtonState.Pressed)
      {
        _puntoAnteriorMouse = e.GetPosition(pic_canvas);
        _estaMoviendoCamara = true;
        pic_canvas.CaptureMouse(); // Capturar el mouse
      }
    }

    private void pic_canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
      if (e.MiddleButton == MouseButtonState.Released)
      {
        _estaMoviendoCamara = false;
        pic_canvas.ReleaseMouseCapture(); // Liberar el mouse
      }
    }

    private void txt_trasladarX_LostFocus(object sender, RoutedEventArgs e)
    {
      float x;
      try
      {
        x = float.Parse(txt_trasladarX.Text);
      }
      catch
      {
        MessageBox.Show("Solo números", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      FiguraSeleccionada.Trasladar(x, 'X');
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void txt_trasladarY_LostFocus(object sender, RoutedEventArgs e)
    {
      float y;
      try
      {
        y = float.Parse(txt_trasladarY.Text);
      }
      catch
      {
        MessageBox.Show("Solo números", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      FiguraSeleccionada.Trasladar(y, 'Y');
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }

    private void txt_trasladarZ_LostFocus(object sender, RoutedEventArgs e)
    {
      float z;
      try
      {
        z = float.Parse(txt_trasladarX.Text);
      }
      catch
      {
        MessageBox.Show("Solo números", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }
      FiguraSeleccionada.Trasladar(z, 'Z');
      render.Renderizar(Figuras, pic_canvas.Camara);
      image_3D.Source = render.RenderTarget;
    }
  }
}
