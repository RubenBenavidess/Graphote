namespace Graphote.Graficos.ControladoresMat
{
    internal class MathHelper
    {
        public static float ToRadians(float degrees)
        {
            return degrees * (MathF.PI / 180f);
        }

        public static float ToDegrees(float radians)
        {
            return radians * (180f / MathF.PI);
        }
    }
}
