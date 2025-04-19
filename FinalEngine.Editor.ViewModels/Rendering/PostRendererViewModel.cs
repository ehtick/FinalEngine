// <copyright file="PostRendererViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Rendering.Effects;
using FinalEngine.Rendering.Renderers.Effects;

public sealed class PostRendererViewModel : ObservableObject, IPostRendererViewModel
{
    private readonly IPostRenderer postRenderer;

    public PostRendererViewModel(IPostRenderer postRenderer)
    {
        this.postRenderer = postRenderer ?? throw new ArgumentNullException(nameof(postRenderer));
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
