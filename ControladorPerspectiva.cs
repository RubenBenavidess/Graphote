using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphote
{
    internal class ControladorPerspectiva
    {
        public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 up)
        {
            Vector3 zAxis = Vector3.Normalize(cameraPosition - cameraTarget); // Dirección hacia adelante
            Vector3 xAxis = Vector3.Normalize(Vector3.Cross(up, zAxis));      // Eje X (derecha)
            Vector3 yAxis = Vector3.Cross(zAxis, xAxis);                      // Eje Y (arriba)

            Matrix4x4 viewMatrix = new Matrix4x4(
                xAxis.X, yAxis.X, zAxis.X, 0,
                xAxis.Y, yAxis.Y, zAxis.Y, 0,
                xAxis.Z, yAxis.Z, zAxis.Z, 0,
                -Vector3.Dot(xAxis, cameraPosition),
                -Vector3.Dot(yAxis, cameraPosition),
                -Vector3.Dot(zAxis, cameraPosition),
                1
            );

            return viewMatrix;
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
                0, 0, (2 * near * far) / range, 0
            );
        }
    }
}
