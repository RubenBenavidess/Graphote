using System.Drawing;
using System.Numerics;

namespace Graphote.Graficos.Figuras
{
    internal class FiguraTridimensional
    {
        public int Id { get; set; }
        private MatrizTransformacion Matriz = new MatrizTransformacion();
        public Vector3[] Vertices { get; set; }
        public (int, int)[] Aristas { get; set; }
        public Color Color { get; set; }

        public Vector3 Posicion { get; private set; }
        public Vector3 Rotacion { get; private set; }
        public float Escala { get; private set; }

        public FiguraTridimensional(int id, Vector3[] vertices, (int, int)[] aristas, Color color)
        {
            Id = id;
            Vertices = vertices;
            Color = color;
            Aristas = aristas;
            Posicion = Vector3.Zero;
            Rotacion = Vector3.Zero;
            Escala = 1.0f;
        }

        public void Trasladar(float distancia, char eje)
        {
            distancia = distancia - (eje == 'X' ? Posicion.X : eje == 'Y' ? Posicion.Y : Posicion.Z);
            Matriz.MatrizTraslacion(distancia, eje);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Length; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
            if (eje == 'X')
                Posicion = new Vector3(Posicion.X + distancia, Posicion.Y, Posicion.Z);
            if (eje == 'Y')
                Posicion = new Vector3(Posicion.X, Posicion.Y + distancia, Posicion.Z);
            if (eje == 'Z')
                Posicion = new Vector3(Posicion.X, Posicion.Y, Posicion.Z + distancia);
        }
        public void Rotar(float angulo, char eje)
        {
            angulo = angulo - (eje == 'X' ? Rotacion.X : eje == 'Y' ? Rotacion.Y : Rotacion.Z);
            Matriz.MatrizRotacion(angulo, eje);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Length; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }

            if (eje == 'X')
                Rotacion = new Vector3(Rotacion.X + angulo, Rotacion.Y, Rotacion.Z);
            if (eje == 'Y')
                Rotacion = new Vector3(Rotacion.X, Rotacion.Y + angulo, Rotacion.Z);
            if (eje == 'Z')
                Rotacion = new Vector3(Rotacion.X, Rotacion.Y, Rotacion.Z + angulo);
        }

        public void Escalar(float escala)
        {
            Matriz.MatrizEscalado(escala / Escala);
            float[] VectorAuxiliar = new float[3] { 0, 0, 0 };
            float[] VectorResultante = new float[3] { 0, 0, 0 };
            for (int i = 0; i < Vertices.Length; i++)
            {
                VectorAuxiliar[0] = Vertices[i].X; VectorAuxiliar[1] = Vertices[i].Y; ; VectorAuxiliar[2] = Vertices[i].Z;
                VectorResultante = Matriz.Transformar(VectorAuxiliar);
                Vertices[i] = new Vector3(VectorResultante[0], VectorResultante[1], VectorResultante[2]);
            }
            Escala = escala;
        }




    }
}
