using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphote
{
    internal class Camara
    {
        public Vector3 Posicion { get; set; }
        public Matrix4x4 MatrizDeVista { get; set; }


        public Camara(Vector3 Posicion) { 
            this.Posicion = Posicion;
            this.MatrizDeVista = ControladorPerspectiva.CreateLookAt(Posicion, Vector3.Zero, Vector3.UnitY);
        }
    }
}
