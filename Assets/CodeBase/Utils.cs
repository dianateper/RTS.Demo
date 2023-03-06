using UnityEngine;

namespace CodeBase
{
    public static class Utils
    {
        public static float InRange(this float value, float min, float max) => Mathf.Clamp(value, min, max);
    }
}