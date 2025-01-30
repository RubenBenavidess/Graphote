using Graphote.Graficos.Espacio;
using Graphote.Graficos.Figuras;
using Graphote.GUI;
using Graphote.Render.Renderizador;
using System.Numerics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace Graphote
{
    public partial class MainWindow : Window
    {
        List<FiguraTridimensional> Figuras = new List<FiguraTridimensional>();
        Renderizador render;
        FiguraTridimensional? FiguraSeleccionada = null;
        bool seleccionar = false;


        public MainWindow()
        {
            InitializeComponent();
            render = new Renderizador(pic_canvas);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
            grid_transofrmaciones.Visibility = Visibility.Hidden;
            slider_escala.Visibility = Visibility.Hidden;
            lbl_escala_value.Visibility = Visibility.Hidden;
            lblEscala.Visibility = Visibility.Hidden;
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
                slider_escala.Visibility = Visibility.Hidden;
                lbl_escala_value.Visibility = Visibility.Hidden;
                lblEscala.Visibility = Visibility.Hidden;
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
                FiguraSeleccionada.Escalar((float)e.NewValue);
                if (lbl_escala_value != null)
                    lbl_escala_value.Content = e.NewValue.ToString("0.00");
                render.Renderizar(Figuras, pic_canvas.Camara);
            }
        }

        private void slider_rotarX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FiguraSeleccionada != null)
            {

                if (lbl_valor_rotarX1 != null)
                    lbl_valor_rotarX1.Content = e.NewValue.ToString("0.00");

                FiguraSeleccionada.Rotar((float)e.NewValue, 'Z');
                render.Renderizar(Figuras, pic_canvas.Camara);
                image_3D.Source = render.RenderTarget;
            }
        }

        private void slider_rotarY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FiguraSeleccionada != null)
            {

                if (lbl_valor_rotarY != null)
                    lbl_valor_rotarY.Content = e.NewValue.ToString("0.00");
                FiguraSeleccionada.Rotar((float)e.NewValue, 'Y');
                render.Renderizar(Figuras, pic_canvas.Camara);
                image_3D.Source = render.RenderTarget;
            }
        }

        private void slider_rotarZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FiguraSeleccionada != null)
            {
                if (lbl_valor_rotarZ != null)
                    lbl_valor_rotarZ.Content = e.NewValue.ToString("0.00");

                FiguraSeleccionada.Rotar((float)e.NewValue, 'X');
                render.Renderizar(Figuras, pic_canvas.Camara);
                image_3D.Source = render.RenderTarget;
            }
        }

        private void btn_focusZ_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            float distanciaCamara = Vector3.Distance(Vector3.Zero, pic_canvas.Camara.Posicion);
            pic_canvas.Camara.Posicion = new Vector3(0, 0, distanciaCamara);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
        }

        private void btn_focusX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            float distanciaCamara = Vector3.Distance(Vector3.Zero, pic_canvas.Camara.Posicion);
            pic_canvas.Camara.Posicion = new Vector3(0, 0, distanciaCamara);
            pic_canvas.Camara.Posicion = new Vector3(pic_canvas.Camara.Posicion.Length(), 0, 0);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
        }

        private void btn_focusY_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            float distanciaCamara = Vector3.Distance(Vector3.Zero, pic_canvas.Camara.Posicion);
            pic_canvas.Camara.Posicion = new Vector3(0, 0, distanciaCamara);
            pic_canvas.Camara.Posicion = new Vector3(0, pic_canvas.Camara.Posicion.Length(), 0);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
        }

        private void opcion_cilindro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FiguraTridimensional cilindro = FabricaFiguras.CrearCilindro();
            Figuras.Add(cilindro);
            grid_figuras.Visibility = Visibility.Hidden;
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
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
            if (seleccionar)
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
                    //Actualizar();
                    grid_transofrmaciones.Visibility = Visibility.Visible;
                    slider_escala.Visibility = Visibility.Visible;
                    lbl_escala_value.Visibility = Visibility.Visible;
                    lblEscala.Visibility = Visibility.Visible;
                    lbl_coordenadaX.Content = FiguraSeleccionada.Posicion.X.ToString();
                    lbl_coordenadaY.Content = FiguraSeleccionada.Posicion.Y.ToString();
                    lbl_coordenadaZ.Content = FiguraSeleccionada.Posicion.Z.ToString();
                    ActualizarGridTransformaciones();

                    // Opcional: Resaltar la figura seleccionada
                }
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

                float sensibilidad = 0.10f;
                delta *= sensibilidad;

                // 1. Evitar que la cámara llegue al origen (causa NaN)
                if (pic_canvas.Camara.Posicion.Length() < 0.1f)
                {
                    pic_canvas.Camara.Posicion = new Vector3(0.1f, 0.1f, 0.1f);
                }

                // 2. Calcular dirección "adelante" con validación
                Vector3 direccionAlOrigen = Vector3.Zero - pic_canvas.Camara.Posicion;
                Vector3 adelante = Vector3.Normalize(direccionAlOrigen);

                // 3. Calcular ejes con fallback seguro
                Vector3 derecha;
                if (Vector3.Cross(Vector3.UnitY, adelante) == Vector3.Zero)
                {
                    // Si la cámara está alineada con Y, usar UnitZ como "derecha"
                    derecha = Vector3.UnitZ;
                }
                else
                {
                    derecha = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, adelante));
                }

                Vector3 arriba = Vector3.Cross(adelante, derecha);

                // 4. Mover cámara
                pic_canvas.Camara.Posicion += derecha * delta.X + arriba * delta.Y;

                // 5. Forzar actualización
                render.Renderizar(Figuras, pic_canvas.Camara);
                image_3D.Source = render.RenderTarget;

                _puntoAnteriorMouse = posicionActual;
            }
            else if (seleccionar)
            {
                Point posicionClic = e.GetPosition(pic_canvas);
                // 2. Obtener figura seleccionada
                FiguraTridimensional? FiguraEnMov = ControladorInterfaz.SeleccionarFiguraPorColor(
                    posicionClic,
                    Figuras,
                    (int)pic_canvas.ActualWidth,
                    (int)pic_canvas.ActualHeight,
                    render.RenderTarget
                );

                if (FiguraEnMov != null)
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.Arrow;
                }
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

        //Pintar opciones del menu de figuras 
        private void opcion_cilindro_MouseMove(object sender, MouseEventArgs e)
        {
            opcion_esfera.Background = null;
            opcion_cubo.Background = null;
            opcion_piramide.Background = null;
            opcion_cilindro.Background = new SolidColorBrush(
                  (Color)ColorConverter.ConvertFromString("#457B9D"));
        }

        private void opcion_piramide_MouseMove(object sender, MouseEventArgs e)
        {
            opcion_esfera.Background = null;
            opcion_cubo.Background = null;
            opcion_cilindro.Background = null;
            opcion_piramide.Background = new SolidColorBrush(
                  (Color)ColorConverter.ConvertFromString("#457B9D"));
        }

        private void opcion_cubo_MouseMove(object sender, MouseEventArgs e)
        {
            opcion_esfera.Background = null;
            opcion_cilindro.Background = null;
            opcion_piramide.Background = null;
            opcion_cubo.Background = new SolidColorBrush(
                  (Color)ColorConverter.ConvertFromString("#457B9D"));
        }

        private void opcion_esfera_MouseMove(object sender, MouseEventArgs e)
        {
            opcion_cubo.Background = null;
            opcion_cilindro.Background = null;
            opcion_piramide.Background = null;
            opcion_esfera.Background = new SolidColorBrush(
                  (Color)ColorConverter.ConvertFromString("#457B9D"));
        }

        // Trasladar la figura con el valor de los txt cuando se pierda el foco
        private void txt_trasladarX_LostFocus(object sender, RoutedEventArgs e)
        {
            validar_texto('X');
        }
        private void txt_trasladarY_LostFocus(object sender, RoutedEventArgs e)
        {
            validar_texto('Y');
        }
        private void txt_trasladarZ_LostFocus(object sender, RoutedEventArgs e)
        {
            validar_texto('Y');
        }

        //Trasladar la figura con el valor de los txt cuando se presione Enter
        private void txt_trasladarX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                validar_texto('X');
        }

        private void txt_trasladarY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                validar_texto('Y');
        }

        private void txt_trasladarZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                validar_texto('Z');
        }

        // Funcion para validar el texto
        public void validar_texto(char eje)
        {
            float _eje;
            try
            {
                _eje = float.Parse(txt_trasladarX.Text) / 10.0f;
                if (_eje > 9)
                {
                    MessageBox.Show("Trasladar menos de 9 puntos", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Solo números", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            FiguraSeleccionada?.Trasladar(_eje, eje);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;

            lbl_coordenadaX.Content = FiguraSeleccionada?.Posicion.X.ToString();
            lbl_coordenadaY.Content = FiguraSeleccionada?.Posicion.Y.ToString();
            lbl_coordenadaZ.Content = FiguraSeleccionada?.Posicion.Z.ToString();
        }
        private void ActualizarGridTransformaciones()
        {
            if (FiguraSeleccionada != null)
            {
                //label escala
                slider_escala.Value = (double)(FiguraSeleccionada.Escala);
                lbl_escala_value.Content = slider_escala.Value.ToString();

                //label rotacion
                slider_rotarX.Value = (double)(FiguraSeleccionada.Rotacion.X);
                lbl_valor_rotarX1.Content = slider_rotarX.Value.ToString();
                slider_rotarY.Value = (double)(FiguraSeleccionada.Rotacion.Y);
                lbl_valor_rotarY.Content = slider_rotarY.Value.ToString();
                slider_rotarZ.Value = (double)(FiguraSeleccionada.Rotacion.Z);
                lbl_valor_rotarZ.Content = slider_rotarZ.Value.ToString();

                //valores de traslado
                txt_trasladarX.Text = FiguraSeleccionada.Posicion.X.ToString();
                txt_trasladarY.Text = FiguraSeleccionada.Posicion.Y.ToString();
                txt_trasladarZ.Text = FiguraSeleccionada.Posicion.Z.ToString();
            }
        }
    }
}
