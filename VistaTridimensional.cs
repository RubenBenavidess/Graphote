using System.Numerics;
using System.Windows.Controls;
using Graphote.Graficos.Espacio;

namespace Graphote
{
    internal class VistaTridimensional : Canvas
    {
        public Camara Camara { get; set; }

        public VistaTridimensional()
        {
            Width = 1421;
            Height = 812;
            Camara = new Camara(new Vector3(5.77f, 4.77f, 5.77f));
        }


    }
}
