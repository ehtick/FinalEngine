// <copyright file="ILightRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Lighting;

using System;
using System.Drawing;
using FinalEngine.Rendering.Lighting;

public interface ILightRenderer : IRenderQueue<Light>
{
    Color AmbientColor { get; set; }

    float AmbientStrength { get; set; }

    bool Enabled { get; set; }

    void Render(Action renderScene);
}
