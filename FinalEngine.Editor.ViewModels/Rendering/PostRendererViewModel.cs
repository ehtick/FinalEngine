// <copyright file="PostRendererViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using System;
using System.Drawing;
using ColorPicker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Maths.Extensions;
using FinalEngine.Rendering.Effects;
using FinalEngine.Rendering.Renderers.Effects;

public sealed class PostRendererViewModel : ObservableObject, IPostRendererViewModel
{
    private readonly IPostRenderer postRenderer;

    private ColorState fogColorState;

    public PostRendererViewModel(IPostRenderer postRenderer)
    {
        this.postRenderer = postRenderer ?? throw new ArgumentNullException(nameof(postRenderer));

        var color = FogRenderEffect.Instance.Color.ToVector4();

        this.fogColorState.A = color.W;
        this.fogColorState.RGB_R = color.X;
        this.fogColorState.RGB_G = color.Y;
        this.fogColorState.RGB_B = color.Z;

        this.OnPropertyChanged(nameof(this.FogColor));
    }

    public string DisplayName
    {
        get { return "Post Renderer"; }
    }

    public bool EffectsEnabled
    {
        get
        {
            return this.postRenderer.Enabled;
        }

        set
        {
            this.postRenderer.Enabled = value;
            this.OnPropertyChanged(nameof(this.EffectsEnabled));
        }
    }

    public ColorState FogColor
    {
        get
        {
            return this.fogColorState;
        }

        set
        {
            this.fogColorState = value;

            FogRenderEffect.Instance.Color = Color.FromArgb(
                (int)(this.fogColorState.A * 255),
                (int)(this.fogColorState.RGB_R * 255),
                (int)(this.fogColorState.RGB_G * 255),
                (int)(this.fogColorState.RGB_B * 255));
        }
    }

    public float FogDensity
    {
        get
        {
            return FogRenderEffect.Instance.Density;
        }

        set
        {
            FogRenderEffect.Instance.Density = value;
            this.OnPropertyChanged(nameof(this.FogDensity));
        }
    }

    public bool FogEnabled
    {
        get
        {
            return FogRenderEffect.Instance.Enabled;
        }

        set
        {
            FogRenderEffect.Instance.Enabled = value;
            this.OnPropertyChanged(nameof(this.FogEnabled));
        }
    }

    public float FogEnd
    {
        get
        {
            return FogRenderEffect.Instance.End;
        }

        set
        {
            FogRenderEffect.Instance.End = value;
            this.OnPropertyChanged(nameof(this.FogEnd));
        }
    }

    public float FogStart
    {
        get
        {
            return FogRenderEffect.Instance.Start;
        }

        set
        {
            FogRenderEffect.Instance.Start = value;
            this.OnPropertyChanged(nameof(this.FogStart));
        }
    }

    public FogType FogType
    {
        get
        {
            return FogRenderEffect.Instance.Type;
        }

        set
        {
            FogRenderEffect.Instance.Type = value;
            this.OnPropertyChanged(nameof(this.FogType));
            this.OnPropertyChanged(nameof(this.IsFogExponential));
        }
    }

    public float GammaAmount
    {
        get
        {
            return GammaCorrectionRenderEffect.Instance.Amount;
        }

        set
        {
            GammaCorrectionRenderEffect.Instance.Amount = value;
            this.OnPropertyChanged(nameof(this.GammaAmount));
        }
    }

    public bool GammaEnabled
    {
        get
        {
            return GammaCorrectionRenderEffect.Instance.Enabled;
        }

        set
        {
            GammaCorrectionRenderEffect.Instance.Enabled = value;
            this.OnPropertyChanged(nameof(this.GammaEnabled));
        }
    }

    public bool InversionEnabled
    {
        get
        {
            return InversionRenderEffect.Instance.Enabled;
        }

        set
        {
            InversionRenderEffect.Instance.Enabled = value;
            this.OnPropertyChanged(nameof(this.InversionEnabled));
        }
    }

    public bool IsExposureEnabled
    {
        get { return this.ToneMappingAlgorithm != ToneMappingAlgorithm.Reinhard; }
    }

    public bool IsFogExponential
    {
        get { return FogRenderEffect.Instance.Type > FogType.Linear; }
    }

    public ToneMappingAlgorithm ToneMappingAlgorithm
    {
        get
        {
            return ToneMappingRenderEffect.Instance.Algorithm;
        }

        set
        {
            ToneMappingRenderEffect.Instance.Algorithm = value;
            this.OnPropertyChanged(nameof(this.ToneMappingAlgorithm));
            this.OnPropertyChanged(nameof(this.IsExposureEnabled));
        }
    }

    public bool ToneMappingEnabled
    {
        get
        {
            return ToneMappingRenderEffect.Instance.Enabled;
        }

        set
        {
            ToneMappingRenderEffect.Instance.Enabled = value;
            this.OnPropertyChanged(nameof(this.ToneMappingEnabled));
        }
    }

    public float ToneMappingExposure
    {
        get
        {
            return ToneMappingRenderEffect.Instance.Exposure;
        }

        set
        {
            ToneMappingRenderEffect.Instance.Exposure = value;
            this.OnPropertyChanged(nameof(this.ToneMappingExposure));
        }
    }

    public bool UsePowerOfTwo
    {
        get
        {
            return this.postRenderer.UsePowerOfTwo;
        }

        set
        {
            this.postRenderer.UsePowerOfTwo = value;
            this.OnPropertyChanged(nameof(this.UsePowerOfTwo));
        }
    }
}
