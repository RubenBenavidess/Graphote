using Graphote.Graficos.Figuras;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Graphote.GUI
{
    internal class ControladorInterfaz
    {
        public static FiguraTridimensional? SeleccionarFiguraPorColor(
            Point posicionClic,
            List<FiguraTridimensional> figuras,
            int width,
            int height,
            WriteableBitmap bitmap)
        {
            int x = (int)posicionClic.X;
            int y = (int)posicionClic.Y;

            if (x < 0 || x >= width || y < 0 || y >= height)
                return null;

            var colorMap = figuras.ToDictionary(f => f.Color.ToArgb(), f => f);
            int flippedY = height - 1 - y;

            var offsets = new (int dx, int dy)[]
            {
            (0, 0),    // Punto exacto
            (0, -1),   // Arriba
            (1, -1),   // Arriba-Derecha
            (1, 0),    // Derecha
            (1, 1),    // Abajo-Derecha
            (0, 1),    // Abajo
            (-1, 1),   // Abajo-Izquierda
            (-1, 0),   // Izquierda
            (-1, -1)   // Arriba-Izquierda
            };

            bitmap.Lock();
            try
            {
                unsafe
                {
                    int* pBuffer = (int*)bitmap.BackBuffer.ToPointer();
                    foreach (var (dx, dy) in offsets)
                    {
                        int newX = x + dx;
                        int newFlippedY = flippedY + dy;

                        if (newX >= 0 && newX < width && newFlippedY >= 0 && newFlippedY < height)
                        {
                            int colorID = pBuffer[newFlippedY * width + newX];
                            if (colorMap.TryGetValue(colorID, out var figura))
                            {
                                return figura;
                            }
                        }
                    }
                }
            }
            finally
            {
                bitmap.Unlock();
            }

            return null;
        }



    }
}
