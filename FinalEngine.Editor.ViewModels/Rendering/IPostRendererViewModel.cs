// <copyright file="IPostRendererViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using FinalEngine.Editor.ViewModels.Services;
using FinalEngine.Rendering.Effects;

public interface IPostRendererViewModel : INamedViewModel
{
    bool EffectsEnabled { get; set; }

    bool InversionEnabled { get; set; }

    bool IsExposureEnabled { get; }

    ToneMappingAlgorithm ToneMappingAlgorithm { get; set; }

    bool ToneMappingEnabled { get; set; }

    float ToneMappingExposure { get; set; }

    bool UsePowerOfTwo { get; set; }
}
