// <copyright file="ILightRendererViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using ColorPicker.Models;
using FinalEngine.Editor.ViewModels.Services;

public interface ILightRendererViewModel : INamedViewModel
{
    ColorState AmbientColorState { get; set; }

    float AmbientStrength { get; set; }

    bool LightingEnabled { get; set; }
}
