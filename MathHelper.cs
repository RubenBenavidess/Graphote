using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphote
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
