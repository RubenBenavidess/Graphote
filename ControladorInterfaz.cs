using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;

namespace Graphote
{
    internal class ControladorInterfaz
    {
        public static FiguraTridimensional? SeleccionarFiguraPorColor(
            Point posicionClic,
            List<FiguraTridimensional> figuras,
            int width,
            int height,
            WriteableBitmap bitmap
        )
        {
            int x = (int)posicionClic.X;
            int y = (int)posicionClic.Y;

            if (x < 0 || x >= width || y < 0 || y >= height)
                return null;

            // Bloquear el bitmap para acceder al buffer
            bitmap.Lock();
            int colorID = 0;

            unsafe
            {
                int* pBuffer = (int*)bitmap.BackBuffer.ToPointer();
                y = height - 1 - y; // Invertir Y
                colorID = pBuffer[y * width + x];
            }

            bitmap.Unlock();

            // Buscar figura por color (ID)
            return figuras.FirstOrDefault(f => f.Color.ToArgb() == colorID);
        }

        

    }
}
