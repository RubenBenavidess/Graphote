using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Graphote
{
    internal class Renderizador
    {
        public WriteableBitmap renderTarget { get; set; }
        private int[] pixelBuffer;
        private float[] zBuffer;
        private int Width, Height;
        private Matrix4x4 MatrizProyeccion;

        public Renderizador(VistaTridimensional vista)
        {
            Width = (int)vista.Width;
            Height = (int)vista.Height;
            renderTarget = new WriteableBitmap(
                Width,
                Height,
                96,
                96,
                PixelFormats.Bgra32,
                null
            );
            pixelBuffer = new int[Width * Height];
            zBuffer = new float[Width * Height];
            MatrizProyeccion = ControladorPerspectiva.CreatePerspective(
                MathHelper.ToRadians(45),
                (float)Width / Height,
                0.1f,
                100f
            );
        }

        public void Renderizar(List<FiguraTridimensional> Figuras, Camara Camara)
        {
            Array.Fill<int>(pixelBuffer, unchecked((int)0xFF000000)); // Fondo negro
            Array.Fill<float>(zBuffer, float.MaxValue);

            // Obtener matrices
            Matrix4x4 MatrizVista = ControladorPerspectiva.CreateLookAt(
                Camara.Posicion, 
                Vector3.Zero, 
                Vector3.One
                );

            // Renderizar cada figura
            foreach (FiguraTridimensional Figura in Figuras)
            {
                foreach ((int, int) Arista in Figura.Aristas)
                {
                    Vector3 inicio = Figura.Vertices[Arista.Item1];
                    Vector3 fin = Figura.Vertices[Arista.Item2];

                    // Aplicar transformaciones del modelo (si existen)
                    Vector3 inicioProyectado = ProyectarVertice(inicio, MatrizVista, MatrizProyeccion);
                    Vector3 finProyectado = ProyectarVertice(fin, MatrizVista, MatrizProyeccion);

                    // Dibujar línea con z-buffer
                    DibujarLinea(inicioProyectado, finProyectado, Figura.Color.ToArgb());
                }
            }

            DibujarEjes(5, MatrizVista);

            // Copiar pixelBuffer al WriteableBitmap
            ActualizarRenderTarget();
        }

        private void DibujarEjes(int LongitudEje, Matrix4x4 MatrizVista)
        {
            // Eje X (Rojo)
            Vector3 ejeXInicio = Vector3.Zero;
            Vector3 ejeXFin = new Vector3(LongitudEje, 0, 0);
            Vector3 ejeXInicioProyectado = ProyectarVertice(ejeXInicio, MatrizVista, MatrizProyeccion);
            Vector3 ejeXFinProyectado = ProyectarVertice(ejeXFin, MatrizVista, MatrizProyeccion);
            DibujarLinea(ejeXInicioProyectado, ejeXFinProyectado, unchecked((int)0xFF8E1919)); // ARGB: Rojo
            ejeXFin = new Vector3(-LongitudEje, 0, 0);
            ejeXInicioProyectado = ProyectarVertice(ejeXInicio, MatrizVista, MatrizProyeccion);
            ejeXFinProyectado = ProyectarVertice(ejeXFin, MatrizVista, MatrizProyeccion);
            DibujarLinea(ejeXInicioProyectado, ejeXFinProyectado, unchecked((int)0xFF8E1919)); // ARGB: Rojo

            // Eje Y (Verde)
            Vector3 ejeYInicio = Vector3.Zero;
            Vector3 ejeYFin = new Vector3(0, LongitudEje, 0);
            Vector3 ejeYInicioProyectado = ProyectarVertice(ejeYInicio, MatrizVista, MatrizProyeccion);
            Vector3 ejeYFinProyectado = ProyectarVertice(ejeYFin, MatrizVista, MatrizProyeccion);
            DibujarLinea(ejeYInicioProyectado, ejeYFinProyectado, unchecked((int)0xFF1B8E19)); // ARGB: Verde
            ejeYFin = new Vector3(0, -LongitudEje, 0);
            ejeYInicioProyectado = ProyectarVertice(ejeYInicio, MatrizVista, MatrizProyeccion);
            ejeYFinProyectado = ProyectarVertice(ejeYFin, MatrizVista, MatrizProyeccion);
            DibujarLinea(ejeYInicioProyectado, ejeYFinProyectado, unchecked((int)0xFF1B8E19)); // ARGB: Verde

            // Eje Z (Azul)
            Vector3 ejeZInicio = Vector3.Zero;
            Vector3 ejeZFin = new Vector3(0, 0, LongitudEje);
            Vector3 ejeZInicioProyectado = ProyectarVertice(ejeZInicio, MatrizVista, MatrizProyeccion);
            Vector3 ejeZFinProyectado = ProyectarVertice(ejeZFin, MatrizVista, MatrizProyeccion);
            DibujarLinea(ejeZInicioProyectado, ejeZFinProyectado, unchecked((int)0xFF19398E)); // ARGB: Azul
            ejeZFin = new Vector3(0, 0, -LongitudEje);
            ejeZInicioProyectado = ProyectarVertice(ejeZInicio, MatrizVista, MatrizProyeccion);
            ejeZFinProyectado = ProyectarVertice(ejeZFin, MatrizVista, MatrizProyeccion);
            DibujarLinea(ejeZInicioProyectado, ejeZFinProyectado, unchecked((int)0xFF19398E)); // ARGB: Azul
        }

        private Vector3 ProyectarVertice(Vector3 vertice, Matrix4x4 vista, Matrix4x4 proyeccion)
        {
            Vector4 v = Vector4.Transform(new Vector4(vertice, 1), vista);
            v = Vector4.Transform(v, proyeccion);
            v /= v.W;

            // Mapear a coordenadas de pantalla
            float x = (v.X + 1) * Width * 0.5f;
            float y = (1 - v.Y) * Height * 0.5f;
            float z = v.Z;

            return new Vector3(x, y, z);
        }

        private void DibujarLinea(Vector3 a, Vector3 b, int color)
        {
            int x0 = (int)a.X, y0 = (int)a.Y;
            int x1 = (int)b.X, y1 = (int)b.Y;
            float z0 = a.Z, z1 = b.Z;

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2;
            int currentX = x0, currentY = y0;

            // Interpolación de Z
            float zStep = (z1 - z0) / Math.Max(dx, Math.Abs(dy));
            float currentZ = z0;

            while (true)
            {
                if (currentX >= 0 && currentX < Width && currentY >= 0 && currentY < Height)
                {
                    int index = currentY * Width + currentX;
                    if (currentZ < zBuffer[index])
                    {
                        pixelBuffer[index] = color;
                        zBuffer[index] = currentZ;
                    }
                }

                if (currentX == x1 && currentY == y1) break;
                e2 = 2 * err;
                if (e2 >= dy)
                {
                    err += dy;
                    currentX += sx;
                    currentZ += zStep;
                }
                if (e2 <= dx)
                {
                    err += dx;
                    currentY += sy;
                }
            }
        }

        private unsafe void ActualizarRenderTarget()
        {
            renderTarget.Lock();
            IntPtr pBackBuffer = renderTarget.BackBuffer;
            int* pBuffer = (int*)pBackBuffer.ToPointer();

            for (int i = 0; i < pixelBuffer.Length; i++)
            {
                pBuffer[i] = pixelBuffer[i];
            }

            renderTarget.AddDirtyRect(new Int32Rect(0, 0, Width, Height));
            renderTarget.Unlock();
        }
    }

}
