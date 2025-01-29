using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Graphote
{
    internal class MatrizTransformacion
    {
        private float[][] Matriz = new float[3][];
        public MatrizTransformacion()
        {
            IniciarMatriz();
        }

        //----------------------------------------------------------------

        // Iniciar Matriz
        private void IniciarMatriz()
        {
            for (int i = 0; i < 3; i++)
            {
                Matriz[i] = new float[3];
            }
        }

        public void EncerarMatriz()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Matriz[i][j] = 0;
                }
            }
        }

        //----------------------------------------------------------------

        // Transformaciones

        public void MatrizTraslacion(float a, float b)
        {
            EncerarMatriz();

            Matriz[0][2] = a;
            Matriz[1][2] = b;

            Matriz[0][0] = 1;
            Matriz[1][1] = 1;
            Matriz[2][2] = 1;
        }

        public void MatrizEscalado(float kx, float ky)
        {
            EncerarMatriz();

            Matriz[0][0] = kx;
            Matriz[1][1] = ky;
            Matriz[2][2] = 1;
        }

        public void MatrizRotacion(float angulo)
        {
            EncerarMatriz();

            Matriz[0][0] = (float)Math.Cos(angulo);
            Matriz[0][1] = (float)(-1 * Math.Sin(angulo));
            Matriz[1][0] = (float)Math.Sin(angulo);
            Matriz[1][1] = (float)(1 * Math.Cos(angulo));
            Matriz[2][2] = 1;
        }

        //----------------------------------------------------------------

        // Producir el vector

        public float[] Transformar(float[] vector)
        {
            float[] Vector = new float[3];
            float Resultado;

            for (int i = 0; i < 3; i++)
            {

                Resultado = 0;

                for (int j = 0; j < 3; j++)
                {
                    Resultado += Matriz[i][j] * vector[j];

                }
                Vector[i] = Resultado;
            }

            return Vector;

        }


    }
}
