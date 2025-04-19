// <copyright file="MathHelper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths;

using System;
using System.Drawing;
using System.Numerics;

/// <summary>
///   Provides helper methods for mathematical operations.
/// </summary>
public static class MathHelper
{
    /// <summary>
    ///   Converts the specified <paramref name="angle"/> from degrees to radians.
    /// </summary>
    /// <param name="angle">
    ///   The angle in degrees.
    /// </param>
    /// <returns>
    ///   The equivalent angle in radians.
    /// </returns>
    /// <remarks>
    ///   This method is useful for converting angles from the more commonly used degrees measurement to radians, which is often required by trigonometric functions.
    /// </remarks>
    public static float DegreesToRadians(float angle)
    {
        return (float)Math.PI / 180.0f * angle;
    }

    public static Size GetNearestPowerOfTwo(Size size)
    {
        return new Size(
            GetNearestPowerOfTwo(size.Width),
            GetNearestPowerOfTwo(size.Height));
    }

    public static int GetNearestPowerOfTwo(int value)
    {
        if (value < 1)
        {
            return 1;
        }

        int up = (int)BitOperations.RoundUpToPowerOf2((uint)value);
        int down = up >> 1;

        return (value - down) < (up - value) ? down : up;
    }

    /// <summary>
    ///   Converts the specified <paramref name="angle"/> from radians to degrees.
    /// </summary>
    /// <param name="angle">
    ///   The angle in radians.
    /// </param>
    /// <returns>
    ///   The equivalent angle in degrees.
    /// </returns>
    /// <remarks>
    ///   This method is useful for converting angles from radians back to degrees, which can be more intuitive for human interpretation.
    /// </remarks>
    public static float RadiansToDegrees(float angle)
    {
        return 180.0f / (float)Math.PI * angle;
    }
}
