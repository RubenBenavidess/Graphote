using Graphote.Graficos.Renderizador.Renderizador;
using System.Numerics;

namespace Graphote.Graficos.Espacio
{
    internal class Camara
    {
        public Vector3 Posicion { get; set; }
        public Matrix4x4 MatrizDeVista { get; set; }


        public Camara(Vector3 Posicion)
        {
            this.Posicion = Posicion;
            MatrizDeVista = ControladorPerspectiva.CreateLookAt(Posicion, Vector3.Zero, Vector3.UnitY);
        }
    }
}
