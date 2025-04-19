// <copyright file="IPostRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers.Effects;

using System;
using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Effects;

public interface IPostRenderer : IRenderQueue<IRenderEffect>
{
    bool Enabled { get; set; }

    bool UsePowerOfTwo { get; set; }

    void Render(Camera camera, Action renderScene);
}
