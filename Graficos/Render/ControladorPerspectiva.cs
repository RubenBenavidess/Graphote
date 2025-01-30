using System.Numerics;

namespace Graphote.Graficos.Renderizador.Renderizador
{
    internal class ControladorPerspectiva
    {
        public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 up)
        {
            Vector3 forward = Vector3.Normalize(cameraTarget - cameraPosition);

            // Si la cámara está alineada con el eje Y, usar UnitZ como "Up"
            if (Math.Abs(forward.Y) > 0.99f)
            {
                up = Vector3.UnitZ; // Cambiar el "Up" para evitar singularidad
            }

            Vector3 right = Vector3.Normalize(Vector3.Cross(up, forward));
            Vector3 correctedUp = Vector3.Cross(forward, right);

            return new Matrix4x4(
                right.X, correctedUp.X, forward.X, 0,
                right.Y, correctedUp.Y, forward.Y, 0,
                right.Z, correctedUp.Z, forward.Z, 0,
                -Vector3.Dot(right, cameraPosition),
                -Vector3.Dot(correctedUp, cameraPosition),
                -Vector3.Dot(forward, cameraPosition),
                1
            );
        }

        public static Matrix4x4 CreatePerspective(float fovRadians, float aspectRatio, float near, float far)
        {
            float tanFov = (float)Math.Tan(fovRadians / 2);
            float f = 1 / tanFov;
            float range = near - far;

            return new Matrix4x4(
                f / aspectRatio, 0, 0, 0,
                0, f, 0, 0,
                0, 0, (near + far) / range, -1,
                0, 0, 2 * near * far / range, 0
            );
        }

        public static Matrix4x4 CreateOrthographic(float size, float aspectRatio, float near, float far)
        {
            float halfWidth = size * aspectRatio * 0.125f;
            float halfHeight = size * 0.125f;

            return new Matrix4x4(
                1 / halfWidth, 0, 0, 0,
                0, 1 / halfHeight, 0, 0,
                0, 0, 1 / (far - near), 0,
                0, 0, -near / (far - near), 1
            );
        }
    }
}
