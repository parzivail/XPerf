namespace XPerf.Utils
{
    internal static class Remapper
    {
        public static float Remap(this float x, float inMin, float inMax, float outMin, float outMax)
        {
            return (x - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
    }
}