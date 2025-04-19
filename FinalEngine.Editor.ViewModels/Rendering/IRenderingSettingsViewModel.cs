// <copyright file="IRenderingSettingsViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using System.Collections.ObjectModel;
using FinalEngine.Editor.ViewModels.Services;

public interface IRenderingSettingsViewModel
{
    object SelectedView { get; set; }

    ObservableCollection<INamedViewModel> Views { get; }
}
