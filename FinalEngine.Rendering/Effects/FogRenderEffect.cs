// <copyright file="FogRenderEffect.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Effects;

using System;
using System.Drawing;
using FinalEngine.Maths.Extensions;

public enum FogType
{
    Linear = 0,

    Exponential = 1,

    ExponentialSquared = 2,
}

//// TODO: Add support for attributes to hide density, etc if using Linear.

public sealed class FogRenderEffect : IRenderEffect
{
    private static FogRenderEffect? instance;

    private readonly float time;

    public FogRenderEffect()
    {
        this.Type = FogType.Exponential;
        this.Color = Color.LightGray;

        this.Start = 0;
        this.End = 1000.0f;
    }

    public static FogRenderEffect Instance
    {
        get { return instance ??= new FogRenderEffect(); }
    }

    public Color Color { get; set; }

    public float Density { get; set; }

    public bool Enabled { get; set; }

    public float End { get; set; }

    public float Start { get; set; }

    public FogType Type { get; set; }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline);

        pipeline.SetUniform("u_fog.base.enabled", this.Enabled);

        pipeline.SetUniform("u_fog.type", (int)this.Type);
        pipeline.SetUniform("u_fog.start", this.Start);
        pipeline.SetUniform("u_fog.end", this.End);
        pipeline.SetUniform("u_fog.color", this.Color.ToVector3());

        if (this.Type != FogType.Linear)
        {
            pipeline.SetUniform("u_fog.density", this.Density);
        }
    }
}
