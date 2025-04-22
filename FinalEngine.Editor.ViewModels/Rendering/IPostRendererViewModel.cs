// <copyright file="IPostRendererViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using ColorPicker.Models;
using FinalEngine.Editor.ViewModels.Services;
using FinalEngine.Rendering.Effects;

public interface IPostRendererViewModel : INamedViewModel
{
    bool EffectsEnabled { get; set; }

    ColorState FogColor { get; set; }

    float FogDensity { get; set; }

    bool FogEnabled { get; set; }

    float FogEnd { get; set; }

    float FogStart { get; set; }

    FogType FogType { get; set; }

    float GammaAmount { get; set; }

    bool GammaEnabled { get; set; }

    bool InversionEnabled { get; set; }

    bool IsExposureEnabled { get; }

    bool IsFogExponential { get; }

    ToneMappingAlgorithm ToneMappingAlgorithm { get; set; }

    bool ToneMappingEnabled { get; set; }

    float ToneMappingExposure { get; set; }

    bool UsePowerOfTwo { get; set; }
}
