// <copyright file="FogTypeHelper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Helpers;

using System;
using FinalEngine.Rendering.Effects;

internal static class FogTypeHelper
{
    public static Array Values
    {
        get { return Enum.GetValues(typeof(FogType)); }
    }
}
