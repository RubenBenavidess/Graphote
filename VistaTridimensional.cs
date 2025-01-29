using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Graphote
{
    internal class VistaTridimensional: Canvas
    {
        public Camara Camara { get; set; }
        public WriteableBitmap renderTarget;
        public int[] pixelBuffer; // Buffer de píxeles (ARGB)

        public VistaTridimensional(int Width, int Height) { 
            this.Width = Width;
            this.Height = Height;
            this.Camara = new Camara(new Vector3(10, 10, 10));
        }

        
    }
}
