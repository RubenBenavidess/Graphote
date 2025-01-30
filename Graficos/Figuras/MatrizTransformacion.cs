namespace Graphote.Graficos.Figuras
{
    internal class MatrizTransformacion
    {
        private float[][] Matriz = new float[4][];

        public MatrizTransformacion()
        {
            IniciarMatriz();
        }

        //----------------------------------------------------------------

        // Iniciar Matriz
        private void IniciarMatriz()
        {
            for (int i = 0; i < 4; i++)
            {
                Matriz[i] = new float[4];
            }
        }

        public void EncerarMatriz()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Matriz[i][j] = 0;
                }
            }
        }

        //----------------------------------------------------------------

        // Transformaciones

        public void MatrizTraslacion(float distancia, char eje)
        {
            EncerarMatriz();

            Matriz[0][0] = 1;
            Matriz[1][1] = 1;
            Matriz[2][2] = 1;
            Matriz[3][3] = 1;

            switch (char.ToUpper(eje))
            {
                case 'X': Matriz[0][3] = distancia; break;
                case 'Y': Matriz[1][3] = distancia; break;
                case 'Z': Matriz[2][3] = distancia; break;
            }
        }

        public void MatrizEscalado(float escala)
        {
            EncerarMatriz();

            Matriz[0][0] = escala;
            Matriz[1][1] = escala;
            Matriz[2][2] = escala;
            Matriz[3][3] = 1;
        }

        public void MatrizRotacion(float angulo, char eje)
        {
            EncerarMatriz();
            float rad = (float)(angulo * Math.PI / 180.0);

            Matriz[3][3] = 1;

            switch (char.ToUpper(eje))
            {
                case 'X':
                    Matriz[0][0] = 1;
                    Matriz[1][1] = (float)Math.Cos(rad);
                    Matriz[1][2] = (float)-Math.Sin(rad);
                    Matriz[2][1] = (float)Math.Sin(rad);
                    Matriz[2][2] = (float)Math.Cos(rad);
                    break;
                case 'Y':
                    Matriz[0][0] = (float)Math.Cos(rad);
                    Matriz[0][2] = (float)Math.Sin(rad);
                    Matriz[1][1] = 1;
                    Matriz[2][0] = (float)-Math.Sin(rad);
                    Matriz[2][2] = (float)Math.Cos(rad);
                    break;
                case 'Z':
                    Matriz[0][0] = (float)Math.Cos(rad);
                    Matriz[0][1] = (float)-Math.Sin(rad);
                    Matriz[1][0] = (float)Math.Sin(rad);
                    Matriz[1][1] = (float)Math.Cos(rad);
                    Matriz[2][2] = 1;
                    break;
            }
        }

        //----------------------------------------------------------------

        // Aplicar transformación a un vector 3D (con coordenadas homogéneas)
        public float[] Transformar(float[] vector)
        {
            if (vector.Length != 3)
                throw new ArgumentException("El vector debe tener 3 componentes (x, y, z)");

            float[] VectorTransformado = new float[4];
            float[] VectorHomogeneo = { vector[0], vector[1], vector[2], 1 };

            for (int i = 0; i < 4; i++)
            {
                float resultado = 0;
                for (int j = 0; j < 4; j++)
                {
                    resultado += Matriz[i][j] * VectorHomogeneo[j];
                }
                VectorTransformado[i] = resultado;
            }

            return new float[] { VectorTransformado[0], VectorTransformado[1], VectorTransformado[2] };
        }
    }
}
