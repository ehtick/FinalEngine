// <copyright file="Light.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Lighting;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.Maths;

public enum LightType
{
    Ambient,

    Directional,

    Point,

    Spot,
}

public sealed class Light
{
    public Light()
    {
        this.Color = Color.White;
        this.Intensity = 1.0f;
        this.Type = LightType.Point;
        this.Attenuation = new Attenuation();
        this.Radius = MathF.Cos(MathHelper.DegreesToRadians(12.5f));
        this.OuterRadius = MathF.Cos(MathHelper.DegreesToRadians(17.5f));
        this.Position = Vector3.Zero;
        this.Direction = Vector3.One;
    }

    public Attenuation Attenuation { get; set; }

    public Color Color { get; set; }

    public Vector3 Direction { get; set; }

    public float Intensity { get; set; }

    public float OuterRadius { get; set; }

    public Vector3 Position { get; set; }

    public float Radius { get; set; }

    public LightType Type { get; set; }
}
