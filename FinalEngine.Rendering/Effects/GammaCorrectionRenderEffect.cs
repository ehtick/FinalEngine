// <copyright file="GammaCorrectionRenderEffect.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Effects;

using System;

public sealed class GammaCorrectionRenderEffect : IRenderEffect
{
    private static GammaCorrectionRenderEffect? instance;

    public GammaCorrectionRenderEffect()
    {
        //// TODO: GL_SRGB_ALPHA texture loading for diffuse maps before enabling this by default.
        this.Enabled = false;
        this.Amount = 2.2f;
    }

    public static GammaCorrectionRenderEffect Instance
    {
        get { return instance ??= new GammaCorrectionRenderEffect(); }
    }

    public float Amount { get; set; }

    public bool Enabled { get; set; }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline);

        pipeline.SetUniform("u_gamma.base.enabled", this.Enabled);
        pipeline.SetUniform("u_gamma.amount", this.Amount);
    }
}
