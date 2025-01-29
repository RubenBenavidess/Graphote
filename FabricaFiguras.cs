using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphote
{
    internal class FabricaFiguras
    {
        static int Id = 0;

        public static FiguraTridimensional CrearCubo()
        {
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

            Color color = Color.White;

            return new FiguraTridimensional(Id, Vertices, Aristas, color);

        }



    }
}
