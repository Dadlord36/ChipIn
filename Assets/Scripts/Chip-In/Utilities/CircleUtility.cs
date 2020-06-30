using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities
{
    public static class CircleUtility
    {
        /// <summary>
        /// Finds position on circle
        /// </summary>
        /// <param name="center">circle origin point</param>
        /// <param name="distance">distance to point from center</param>
        /// <param name="angle">angle in degrees from 0 to 360</param>
        /// <returns></returns>
        public static Vector2 FindAnglePosition(in Vector2 center, in float distance, in float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            return new Vector2(center.x + Mathf.Cos(rad) * distance, center.y + Mathf.Sin(rad) * distance);
        }

        public static float GetDegreesAngleFromMovementDirection(MoveDirection moveDirection)
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    return 180f;
                case MoveDirection.Up:
                    return 270f;
                case MoveDirection.Right:
                case MoveDirection.None:
                    return 0f;
                case MoveDirection.Down:
                    return 90f;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
            }
        }
    }
}