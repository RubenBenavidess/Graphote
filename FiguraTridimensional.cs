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
        public int Id { get; set;  }
        private MatrizTransformacion Matriz = new MatrizTransformacion();
        public Vector3[] Vertices { get; set; }
        public (int, int)[] Aristas { get; set; }
        public Color Color { get; set; }

        public FiguraTridimensional(int id, Vector3[] vertices, (int, int)[] aristas, Color color)
        {
          this.Id = id;
          this.Vertices = vertices;
          this.Color = color;
          this.Aristas = aristas;
        }

        public void Trasladar(float distancia, char eje)
        {
            Matriz.MatrizTraslacion(distancia, eje);
            float[] VectorAuxiliar = new float[3] {0, 0, 0};
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Length; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
        }
        public void Rotar(float angulo, char eje)
        {
            Matriz.MatrizRotacion(angulo, eje);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Length; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
        }

        public void Escalar(float escala)
        {
            Matriz.MatrizEscalado(escala);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Length; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
        }




    }
}
