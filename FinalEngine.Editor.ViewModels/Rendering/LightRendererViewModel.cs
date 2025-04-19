// <copyright file="LightRendererViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using System;
using System.Drawing;
using ColorPicker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Maths.Extensions;
using FinalEngine.Rendering.Renderers.Lighting;

public sealed class LightRendererViewModel : ObservableObject, ILightRendererViewModel
{
    private readonly ILightRenderer lightRenderer;

    private ColorState ambientColorState;

    public LightRendererViewModel(ILightRenderer lightRenderer)
    {
        this.lightRenderer = lightRenderer ?? throw new ArgumentNullException(nameof(lightRenderer));

        var color = this.lightRenderer.AmbientColor.ToVector4();

        this.ambientColorState.A = color.W;
        this.ambientColorState.RGB_R = color.X;
        this.ambientColorState.RGB_G = color.Y;
        this.ambientColorState.RGB_B = color.Z;

        this.OnPropertyChanged(nameof(this.AmbientColorState));
    }

    public ColorState AmbientColorState
    {
        get
        {
            return this.ambientColorState;
        }

        set
        {
            this.ambientColorState = value;

            this.lightRenderer.AmbientColor = Color.FromArgb(
                (int)(this.ambientColorState.A * 255),
                (int)(this.ambientColorState.RGB_R * 255),
                (int)(this.ambientColorState.RGB_G * 255),
                (int)(this.ambientColorState.RGB_B * 255));
        }
    }

    public float AmbientStrength
    {
        get
        {
            return this.lightRenderer.AmbientStrength;
        }

        set
        {
            this.lightRenderer.AmbientStrength = value;
            this.OnPropertyChanged(nameof(this.AmbientStrength));
        }
    }

    public string DisplayName
    {
        get { return "Light Renderer"; }
    }

    public bool LightingEnabled
    {
        get
        {
            return this.lightRenderer.Enabled;
        }

        set
        {
            this.lightRenderer.Enabled = value;
            this.OnPropertyChanged(nameof(this.LightingEnabled));
        }
    }
}
