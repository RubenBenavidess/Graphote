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

    }
}
