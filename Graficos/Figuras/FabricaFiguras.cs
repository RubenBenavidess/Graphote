using System.Numerics;
using Color = System.Drawing.Color;

namespace Graphote.Graficos.Figuras
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

            Color Color = generateColorId();

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

            Color Color = generateColorId();

            return new FiguraTridimensional(Id, Vertices, Aristas, Color);

        }

        public static FiguraTridimensional CrearCilindro()
        {
            Id++;

            int segmentos = 30;
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

            Color Color = generateColorId();

            return new FiguraTridimensional(Id, Vertices, Aristas, Color);
        }

        public static FiguraTridimensional CrearEsfera()
        {
            Id++;

            int subdivisiones = 10;
            float radio = 1.0f;

            // Crear los 12 vértices base del icosaedro
            float t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0); // Número áureo
            Vector3[] baseVertices = new Vector3[]
            {
            new Vector3(-1,  t,  0), new Vector3( 1,  t,  0), new Vector3(-1, -t,  0), new Vector3( 1, -t,  0),
            new Vector3( 0, -1,  t), new Vector3( 0,  1,  t), new Vector3( 0, -1, -t), new Vector3( 0,  1, -t),
            new Vector3( t,  0, -1), new Vector3( t,  0,  1), new Vector3(-t,  0, -1), new Vector3(-t,  0,  1)
            };

            // Normalizar los vértices para que queden sobre la esfera
            for (int i = 0; i < baseVertices.Length; i++)
                baseVertices[i] = Vector3.Normalize(baseVertices[i]) * radio;

            // Subdividir los triángulos para mayor precisión
            int numVertices = baseVertices.Length;
            int numAristas = IcosaedroCaras.Length * 3;
            Vector3[] tempVertices = new Vector3[numVertices * (1 << subdivisiones * 2)];
            Array.Copy(baseVertices, tempVertices, numVertices);
            (int, int)[] tempAristas = new (int, int)[numAristas * (1 << subdivisiones * 2)];

            int indiceVertice = numVertices;
            int indiceArista = 0;

            for (int s = 0; s < subdivisiones; s++)
            {
                for (int i = 0; i < IcosaedroCaras.Length; i++)
                {
                    int v1 = IcosaedroCaras[i][0];
                    int v2 = IcosaedroCaras[i][1];
                    int v3 = IcosaedroCaras[i][2];

                    // Calcular los puntos medios y normalizarlos
                    Vector3 m1 = Vector3.Normalize((tempVertices[v1] + tempVertices[v2]) / 2) * radio;
                    Vector3 m2 = Vector3.Normalize((tempVertices[v2] + tempVertices[v3]) / 2) * radio;
                    Vector3 m3 = Vector3.Normalize((tempVertices[v3] + tempVertices[v1]) / 2) * radio;

                    int i1 = indiceVertice++;
                    int i2 = indiceVertice++;
                    int i3 = indiceVertice++;

                    tempVertices[i1] = m1;
                    tempVertices[i2] = m2;
                    tempVertices[i3] = m3;

                    // Agregar nuevas aristas
                    tempAristas[indiceArista++] = (v1, i1);
                    tempAristas[indiceArista++] = (v2, i1);
                    tempAristas[indiceArista++] = (v2, i2);
                    tempAristas[indiceArista++] = (v3, i2);
                    tempAristas[indiceArista++] = (v3, i3);
                    tempAristas[indiceArista++] = (v1, i3);
                    tempAristas[indiceArista++] = (i1, i2);
                    tempAristas[indiceArista++] = (i2, i3);
                    tempAristas[indiceArista++] = (i3, i1);
                }
            }

            // Copiar a los arreglos finales
            Vector3[] Vertices = new Vector3[indiceVertice];
            Array.Copy(tempVertices, Vertices, indiceVertice);

            (int, int)[] Aristas = new (int, int)[indiceArista];
            Array.Copy(tempAristas, Aristas, indiceArista);

            Color Color = generateColorId();

            return new FiguraTridimensional(Id, Vertices, Aristas, Color);
        }

        private static readonly int[][] IcosaedroCaras = new int[][]
        {
            new int[] { 0, 11, 5 }, new int[] { 0, 5, 1 }, new int[] { 0, 1, 7 }, new int[] { 0, 7, 10 }, new int[] { 0, 10, 11 },
            new int[] { 1, 5, 9 }, new int[] { 5, 11, 4 }, new int[] { 11, 10, 2 }, new int[] { 10, 7, 6 }, new int[] { 7, 1, 8 },
            new int[] { 3, 9, 4 }, new int[] { 3, 4, 2 }, new int[] { 3, 2, 6 }, new int[] { 3, 6, 8 }, new int[] { 3, 8, 9 },
            new int[] { 4, 9, 5 }, new int[] { 2, 4, 11 }, new int[] { 6, 2, 10 }, new int[] { 8, 6, 7 }, new int[] { 9, 8, 1 }
        };

        private static Color generateColorId()
        {
            byte r = (byte)(255 - Id * 10 % 255);
            byte g = (byte)(255 - Id * 20 % 255);
            byte b = (byte)(255 - Id * 30 % 255);
            return Color.FromArgb(0xFF, r, g, b);
        }
    }
}
