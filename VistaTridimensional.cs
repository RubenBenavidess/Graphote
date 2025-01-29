using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Graphote
{
    internal class VistaTridimensional: Canvas
    {
        public Camara Camara { get; set; }

        public VistaTridimensional() { 
            this.Width = 1421;
            this.Height = 812;
            this.Camara = new Camara(new Vector3(5, 5, 10));
        }

    }
}
