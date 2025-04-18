// <copyright file="InversionRenderEffect.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Effects;

using System;

public sealed class InversionRenderEffect : IRenderEffect
{
    private static InversionRenderEffect? instance;

    public static InversionRenderEffect Instance
    {
        get { return instance ??= new InversionRenderEffect(); }
    }

    public bool Enabled { get; set; }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline, nameof(pipeline));
        pipeline.SetUniform("u_inversion.base.enabled", this.Enabled);
    }
}
