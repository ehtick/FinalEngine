// <copyright file="RenderingSettingsView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Rendering;

using FinalEngine.Editor.ViewModels.Rendering;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using MahApps.Metro.Controls;

public partial class RenderingSettingsView : MetroWindow, IShowable<IRenderingSettingsViewModel>
{
    public RenderingSettingsView()
    {
        this.InitializeComponent();
    }
}
