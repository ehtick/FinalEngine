// <copyright file="IMainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System.Windows.Input;
using FinalEngine.Editor.ViewModels.Docking;

public interface IMainViewModel
{
    ICommand CreateConeCommand { get; }

    ICommand CreateCubeCommand { get; }

    ICommand CreateCylinderCommand { get; }

    ICommand CreateDirectionalLightCommand { get; }

    ICommand CreateEntityCommand { get; }

    ICommand CreatePlaneCommand { get; }

    ICommand CreatePointLightCommand { get; }

    ICommand CreateSphereCommand { get; }

    ICommand CreateSpotLightCommand { get; }

    ICommand CreateTorusCommand { get; }

    IDockViewModel DockViewModel { get; }

    ICommand ExitCommand { get; }

    ICommand ManageWindowLayoutsCommand { get; }

    ICommand ResetWindowLayoutCommand { get; }

    ICommand SaveWindowLayoutCommand { get; }

    ICommand ShowRenderingSettingsCommand { get; }

    string Title { get; }

    ICommand ToggleToolWindowCommand { get; }
}
