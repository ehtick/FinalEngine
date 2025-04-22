// <copyright file="Matrix4x4Extensions.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths.Extensions;

using System.Numerics;

public static class Matrix4x4Extensions
{
    public static Matrix4x4 Inverted(this Matrix4x4 matrix)
    {
        return Matrix4x4.Invert(matrix, out var inverse)
            ? inverse
            : Matrix4x4.Identity;
    }
}
