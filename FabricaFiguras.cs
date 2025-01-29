using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Graphote
{
    internal class FabricaFiguras
    {
        static int Id = -1;

        public static FiguraTridimensional CrearCubo()
        {
            Id++;

            Vector3[] Vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, -0.5f),  // 0
                new Vector3( 0.5f, -0.5f, -0.5f),  // 1
                new Vector3( 0.5f,  0.5f, -0.5f),  // 2
                new Vector3(-0.5f,  0.5f, -0.5f),  // 3
                new Vector3(-0.5f, -0.5f,  0.5f),  // 4
                new Vector3( 0.5f, -0.5f,  0.5f),  // 5
                new Vector3( 0.5f,  0.5f,  0.5f),  // 6
                new Vector3(-0.5f,  0.5f,  0.5f)   // 7
            };

            (int, int)[] Aristas = new (int, int)[]
            {
                (0, 1), (1, 2), (2, 3), (3, 0),  // Base inferior
                (4, 5), (5, 6), (6, 7), (7, 4),  // Base superior
                (0, 4), (1, 5), (2, 6), (3, 7)   // Conexión entre bases
            };

            Color Color = Color.White;

            return new FiguraTridimensional(Id, Vertices, Aristas, Color);

        }

        public static FiguraTridimensional CrearPiramide()
        {
            Id++;

            Vector3[] Vertices = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, -0.5f),  // 0 - Base inferior izquierda
                new Vector3( 0.5f, -0.5f, -0.5f),  // 1 - Base inferior derecha
                new Vector3( 0.5f,  0.5f, -0.5f),  // 2 - Base superior derecha
                new Vector3(-0.5f,  0.5f, -0.5f),  // 3 - Base superior izquierda
                new Vector3( 0.0f,  0.0f,  0.5f)   // 4 - Punta
            };

            (int, int)[] Aristas = new (int, int)[]
            {
                (0, 1), (1, 2), (2, 3), (3, 0),  // Base
                (0, 4), (1, 4), (2, 4), (3, 4)   // Conexiones a la punta
            };

            Color Color = Color.White;

            return new FiguraTridimensional(Id, Vertices, Aristas, Color);

        }

        public static FiguraTridimensional CrearCilindro()
        {
            int segmentos = 10;
            float radio = 0.5f;
            Vector3[] Vertices = new Vector3[segmentos * 2];

            // Generar los vértices
            for (int i = 0; i < segmentos; i++)
            {
                float angulo = (float)(i * Math.PI * 2 / segmentos);
                float x = radio * (float)Math.Cos(angulo);
                float y = radio * (float)Math.Sin(angulo);

                Vertices[i * 2] = new Vector3(x, y, -0.5f); // Base inferior
                Vertices[i * 2 + 1] = new Vector3(x, y, 0.5f);  // Base superior
            }

            // Generar las aristas
            (int, int)[] Aristas = new (int, int)[segmentos * 3];

            for (int i = 0; i < segmentos; i++)
            {
                int next = (i + 1) % segmentos;

                Aristas[i * 3] = (i * 2, next * 2);          // Base inferior
                Aristas[i * 3 + 1] = (i * 2 + 1, next * 2 + 1); // Base superior
                Aristas[i * 3 + 2] = (i * 2, i * 2 + 1);     // Conexión entre bases
            }

            Color Color = Color.White;

            return new FiguraTridimensional(Id, Vertices, Aristas, Color);
        }

    }
}
