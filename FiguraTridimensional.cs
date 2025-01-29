using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphote
{
    internal class FiguraTridimensional
    {
        private int Id;
        private MatrizTransformacion Matriz = new MatrizTransformacion();
        private List<Vector3> Vertices;
        private List<Arista> Aristas;
        private List<Cara> Caras;
        private Color color;

        public FiguraTridimensional(int id, Color color)
        {
            Id = id;
            this.color = color;
        }

        public void Trasladar(float a, float b)
        {
            Matriz.MatrizTraslacion(a, b);
            float[] VectorAuxiliar = new float[3] {0, 0, 0};
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Count; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
        }
        public void Rotar(float angulo)
        {
            Matriz.MatrizRotacion(angulo);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Count; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
        }

        public void Escakar(float kx, float ky)
        {
            Matriz.MatrizEscalado(kx, ky);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Count; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
        }




    }
}
