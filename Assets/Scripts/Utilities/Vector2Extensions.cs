using UnityEngine;

namespace Utilities
{
    public static class Vector2Extensions
    {
        public static float GetRandomInVector(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }
    }
}
