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

namespace Graphote
{
    public partial class MainWindow : Window
    {
        List<FiguraTridimensional> Figuras = new List<FiguraTridimensional>();
        Renderizador render;
        FiguraTridimensional? FiguraSeleccionada = null;
        public MainWindow()
        {
            //vista = new VistaTridimensional(pic_canvas.Width, pic_canvas.Height);
            InitializeComponent();
            render = new Renderizador(pic_canvas);
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
            Figuras.Clear();
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
            pic_canvas.Camara.Posicion = new Vector3(0,0,5);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
        }

        private void btn_focusX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pic_canvas.Camara.Posicion = new Vector3(5, 0, 0);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
        }

        private void btn_focusY_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pic_canvas.Camara.Posicion = new Vector3(0, 5, 0);
            render.Renderizar(Figuras, pic_canvas.Camara);
            image_3D.Source = render.RenderTarget;
        }

        private void pic_canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point posicionClic = e.GetPosition(this);
            //Vector3? direccionRayo = ControladorInterfaz.ObtenerRayoDesdeClic(posicionClic, pic_canvas.Camara, render.MatrizProyeccion, render.);

            //if (direccionRayo != null)
            // {
            //    FiguraSeleccionada = SeleccionarFigura(Figuras, pic_canvas.Camara.Posicion, direccionRayo.Value);
            //}
        }

        private void opcion_cilindro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //FiguraTridimensional cilindro= FabricaFiguras.CrearCilindro();
            //Figuras.Add(cilindro);
            grid_figuras.Visibility = Visibility.Hidden;
        }

        private void opcion_piramide_MouseDown(object sender, MouseButtonEventArgs e)
        {
        FiguraTridimensional piramide = FabricaFiguras.CrearPiramide();
        Figuras.Add(piramide);
        grid_figuras.Visibility = Visibility.Hidden;
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;
        }

        private void opcion_cubo_MouseDown(object sender, MouseButtonEventArgs e)
        {
        FiguraTridimensional cubo = FabricaFiguras.CrearCubo();
        Figuras.Add(cubo);
        grid_figuras.Visibility = Visibility.Hidden;
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;
        }

        private void opcion_esfera_MouseDown(object sender, MouseButtonEventArgs e)
        {
        //FiguraTridimensional esfera = FabricaFiguras.CrearEsfera();
        //Figuras.Add(esfera);
        grid_figuras.Visibility = Visibility.Hidden;
        render.Renderizar(Figuras, pic_canvas.Camara);
        image_3D.Source = render.RenderTarget;
        }

        private void lbl_añadir_MouseDown(object sender, MouseButtonEventArgs e)
        {
        if(grid_figuras.Visibility == Visibility.Visible)
        grid_figuras.Visibility = Visibility.Hidden;
        else
        grid_figuras.Visibility = Visibility.Visible;
        }
    }
}
