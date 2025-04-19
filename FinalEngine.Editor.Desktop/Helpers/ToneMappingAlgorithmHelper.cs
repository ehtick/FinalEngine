// <copyright file="ToneMappingAlgorithmHelper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Helpers;

using System;
using FinalEngine.Rendering.Effects;

public static class ToneMappingAlgorithmHelper
{
    public static Array Values
    {
        get { return Enum.GetValues(typeof(ToneMappingAlgorithm)); }
    }
}
