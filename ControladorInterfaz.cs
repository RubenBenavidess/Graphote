using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Graphote
{
    internal class ControladorInterfaz
    {
        public static Vector3? ObtenerRayoDesdeClic(Point posicionClic, Camara camara, Matrix4x4 proyeccion, int Width, int Height)
        {
            // Convertir clic a coordenadas normalizadas (NDC)
            float x = (2.0f * (float)posicionClic.X / Width) - 1.0f;
            float y = 1.0f - (2.0f * (float)posicionClic.Y / Height);
            Vector4 rayoClip = new Vector4(x, y, -1.0f, 1.0f);

            // Convertir a espacio de la cámara (inversa de la proyección)
            Matrix4x4.Invert(proyeccion, out Matrix4x4 inversaProyeccion);
            Vector4 rayoCamara = Vector4.Transform(rayoClip, inversaProyeccion);
            rayoCamara = new Vector4(rayoCamara.X, rayoCamara.Y, -1.0f, 0.0f);

            // Convertir a espacio del mundo (inversa de la vista)
            Matrix4x4.Invert(ControladorPerspectiva.CreateLookAt(
                camara.Posicion,
                Vector3.Zero,
                Vector3.UnitY
            ), out Matrix4x4 inversaVista);
            Vector4 rayoMundo = Vector4.Transform(rayoCamara, inversaVista);
            Vector3 direccionRayo = Vector3.Normalize(new Vector3(rayoMundo.X, rayoMundo.Y, rayoMundo.Z));

            return direccionRayo;
        }
        private static float CalcularDistanciaRayoLinea(Vector3 origenRayo, Vector3 direccionRayo, Vector3 puntoA, Vector3 puntoB)
        {
            // Algoritmo de distancia mínima entre rayo y segmento de línea
            Vector3 u = direccionRayo;
            Vector3 v = puntoB - puntoA;
            Vector3 w = origenRayo - puntoA;

            float a = Vector3.Dot(u, u);
            float b = Vector3.Dot(u, v);
            float c = Vector3.Dot(v, v);
            float d = Vector3.Dot(u, w);
            float e = Vector3.Dot(v, w);
            float denom = a * c - b * b;

            if (denom < 1e-6f) return float.MaxValue; // Línea paralela

            float t = (b * e - c * d) / denom;
            t = Math.Clamp(t, 0, 1);

            Vector3 puntoCercano = puntoA + t * v;
            return Vector3.Distance(origenRayo, puntoCercano);
        }

        public static FiguraTridimensional? SeleccionarFigura(List<FiguraTridimensional> figuras, Vector3 origenCamara, Vector3 direccionRayo)
        {
            FiguraTridimensional? figuraSeleccionada = null;
            float distanciaMinima = float.MaxValue;

            foreach (var figura in figuras)
            {
                foreach (var arista in figura.Aristas)
                {
                    Vector3 inicio = figura.Vertices[arista.Item1];
                    Vector3 fin = figura.Vertices[arista.Item2];

                    // Calcular intersección rayo-arista (simplificado)
                    float distancia = CalcularDistanciaRayoLinea(origenCamara, direccionRayo, inicio, fin);

                    if (distancia < 0.1f && distancia < distanciaMinima) // Umbral de proximidad
                    {
                        distanciaMinima = distancia;
                        figuraSeleccionada = figura;
                    }
                }
            }

            return figuraSeleccionada;
        }
    }
}
