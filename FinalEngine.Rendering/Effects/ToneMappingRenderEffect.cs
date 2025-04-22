// <copyright file="ToneMappingRenderEffect.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Effects;

using System;

public enum ToneMappingAlgorithm
{
    Reinhard = 0,

    Exposure = 1,
}

public sealed class ToneMappingRenderEffect : IRenderEffect
{
    private static ToneMappingRenderEffect? instance;

    private ToneMappingRenderEffect()
    {
        this.Enabled = true;
        this.Algorithm = ToneMappingAlgorithm.Exposure;
        this.Exposure = 1.0f;
    }

    public static ToneMappingRenderEffect Instance
    {
        get { return instance ??= new ToneMappingRenderEffect(); }
    }

    public ToneMappingAlgorithm Algorithm { get; set; }

    public bool Enabled { get; set; }

    public float Exposure { get; set; }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline, nameof(pipeline));

        pipeline.SetUniform("u_toneMapping.base.enabled", this.Enabled);
        pipeline.SetUniform("u_toneMapping.type", (int)this.Algorithm);

        if (this.Algorithm == ToneMappingAlgorithm.Exposure)
        {
            pipeline.SetUniform("u_toneMapping.exposure", this.Exposure);
        }
    }
}
