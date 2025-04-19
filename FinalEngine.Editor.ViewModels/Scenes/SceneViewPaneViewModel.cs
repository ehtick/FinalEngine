// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Drawing;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Rendering;
using Microsoft.Extensions.Logging;

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private static bool isInitialized;

    private readonly IRenderDevice renderDevice;

    private readonly ISceneManager sceneManager;

    private ICommand? renderCommand;

    private ICommand? resizeCommand;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        ISceneManager sceneManager,
        IRenderDevice renderDevice)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.Title = "Scene View";
        this.ContentID = "SceneView";
        logger.LogInformation($"Initializing {this.Title}...");
    }

    public ICommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand<int>(this.Render); }
    }

    public ICommand ResizeCommand
    {
        get { return this.resizeCommand ??= new RelayCommand<Rectangle>(this.Resize); }
    }

    private void Render(int defaultFrameBuffer)
    {
        if (!isInitialized)
        {
            this.renderDevice.Pipeline.SetDefaultFrameBufferTarget(defaultFrameBuffer);

            this.sceneManager.Initialize();
            isInitialized = true;
        }

        this.sceneManager.Update();
        this.sceneManager.Render();
    }

    private void Resize(Rectangle viewport)
    {
        this.renderDevice.Rasterizer.SetViewport(viewport);
    }
}
