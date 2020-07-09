using UnityEngine;

namespace Utilities
{
    public static class ScreenUtility
    {
        public static Vector2 GetScreenSize()
        {
            return new Vector2(Screen.width, Screen.height);
        }
    }
}