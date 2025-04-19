// <copyright file="RenderingSettingsViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Rendering;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Services;

public sealed class RenderingSettingsViewModel : ObservableObject, IRenderingSettingsViewModel
{
    private object? selectedView;

    public RenderingSettingsViewModel(
        IFactory<IPostRendererViewModel> postRendererViewModelFactory,
        IFactory<ILightRendererViewModel> lightRendererViewModelFactory)
    {
        ArgumentNullException.ThrowIfNull(postRendererViewModelFactory);
        ArgumentNullException.ThrowIfNull(lightRendererViewModelFactory);

        this.Views =
        [
            postRendererViewModelFactory.Create(),
            lightRendererViewModelFactory.Create(),
        ];
    }

    public object SelectedView
    {
        get { return this.selectedView ??= this.Views.First(); }
        set { this.SetProperty(ref this.selectedView, value); }
    }

    public ObservableCollection<INamedViewModel> Views { get; }
}
