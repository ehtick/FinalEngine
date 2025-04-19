// <copyright file="ColorExtensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths.Extensions;

using System.Drawing;
using System.Numerics;

public static class ColorExtensions
{
    public static Vector2 ToVector2(this Color color)
    {
        return new Vector2(
            x: NormalizeComponent(color.R),
            y: NormalizeComponent(color.G));
    }

    public static Vector3 ToVector3(this Color color)
    {
        return new Vector3(
            x: NormalizeComponent(color.R),
            y: NormalizeComponent(color.G),
            z: NormalizeComponent(color.B));
    }

    public static Vector4 ToVector4(this Color color)
    {
        return new Vector4(
            x: NormalizeComponent(color.R),
            y: NormalizeComponent(color.G),
            z: NormalizeComponent(color.B),
            w: NormalizeComponent(color.A));
    }

    private static float NormalizeComponent(byte component)
    {
        return component == 0 ? 0f : component / 255f;
    }
}
