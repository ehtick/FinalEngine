// <copyright file="MainViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Services.Application;
using FinalEngine.Editor.Common.Services.Entities;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Rendering;
using FinalEngine.Editor.ViewModels.Services;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Resources;
using Microsoft.Extensions.Logging;

public sealed class MainViewModel : ObservableObject, IMainViewModel
{
    private readonly ILayoutManager layoutManager;

    private readonly ILogger<MainViewModel> logger;

    private readonly ISceneManager sceneManager;

    private readonly IViewPresenter viewPresenter;

    private ICommand? createConeCommand;

    private ICommand? createCubeCommand;

    private ICommand? createCylinderCommand;

    private ICommand? createDirectionalLightCommand;

    private ICommand? createEntityCommand;

    private ICommand? createPlaneCommand;

    private ICommand? createPointLightCommand;

    private ICommand? createSphereCommand;

    private ICommand? createSpotLightCommand;

    private ICommand? createTorusCommand;

    private ICommand? exitCommand;

    private ICommand? manageWindowLayoutsCommand;

    private ICommand? resetWindowLayoutCommand;

    private ICommand? saveWindowLayoutCommand;

    private ICommand? showRenderingSettingsCommand;

    private ICommand? toggleToolWindowCommand;

    public MainViewModel(
        ILogger<MainViewModel> logger,
        IViewPresenter viewPresenter,
        IApplicationContext applicationContext,
        ILayoutManager layoutManager,
        IFactory<IDockViewModel> dockViewModelFactory,
        IResourceManager resourceManager,
        IResourceLoaderFetcher fetcher,
        ISceneManager sceneManager)
    {
        ArgumentNullException.ThrowIfNull(applicationContext, nameof(applicationContext));
        ArgumentNullException.ThrowIfNull(dockViewModelFactory, nameof(dockViewModelFactory));
        ArgumentNullException.ThrowIfNull(resourceManager, nameof(resourceManager));
        ArgumentNullException.ThrowIfNull(fetcher, nameof(fetcher));

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.viewPresenter = viewPresenter ?? throw new ArgumentNullException(nameof(viewPresenter));
        this.layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));

        this.DockViewModel = dockViewModelFactory.Create();
        this.Title = applicationContext.Title;

        var loaders = fetcher.GetResourceLoaders();

        foreach (var loader in loaders)
        {
            resourceManager.RegisterLoader(loader.GetResourceType(), loader);
        }
    }

    public ICommand CreateConeCommand
    {
        get { return this.createConeCommand ??= new RelayCommand(this.CreateCone); }
    }

    public ICommand CreateCubeCommand
    {
        get { return this.createCubeCommand ??= new RelayCommand(this.CreateCube); }
    }

    public ICommand CreateCylinderCommand
    {
        get { return this.createCylinderCommand ??= new RelayCommand(this.CreateCylinder); }
    }

    public ICommand CreateDirectionalLightCommand
    {
        get { return this.createDirectionalLightCommand ??= new RelayCommand(this.CreateDirectionalLight); }
    }

    public ICommand CreateEntityCommand
    {
        get { return this.createEntityCommand ??= new RelayCommand(this.CreateEntity); }
    }

    public ICommand CreatePlaneCommand
    {
        get { return this.createPlaneCommand ??= new RelayCommand(this.CreatePlane); }
    }

    public ICommand CreatePointLightCommand
    {
        get { return this.createPointLightCommand ??= new RelayCommand(this.CreatePointLight); }
    }

    public ICommand CreateSphereCommand
    {
        get { return this.createSphereCommand ??= new RelayCommand(this.CreateSphere); }
    }

    public ICommand CreateSpotLightCommand
    {
        get { return this.createSpotLightCommand ??= new RelayCommand(this.CreateSpotLight); }
    }

    public ICommand CreateTorusCommand
    {
        get { return this.createTorusCommand ??= new RelayCommand(this.CreateTorus); }
    }

    public IDockViewModel DockViewModel { get; }

    public ICommand ExitCommand
    {
        get { return this.exitCommand ??= new RelayCommand<ICloseable>(this.Close); }
    }

    public ICommand ManageWindowLayoutsCommand
    {
        get { return this.manageWindowLayoutsCommand ??= new RelayCommand(this.ShowManageWindowLayoutsView); }
    }

    public ICommand ResetWindowLayoutCommand
    {
        get { return this.resetWindowLayoutCommand ??= new RelayCommand(this.ResetWindowLayout); }
    }

    public ICommand SaveWindowLayoutCommand
    {
        get { return this.saveWindowLayoutCommand ??= new RelayCommand(this.ShowSaveWindowLayoutView); }
    }

    public ICommand ShowRenderingSettingsCommand
    {
        get { return this.showRenderingSettingsCommand ??= new RelayCommand(this.ShowRenderingSettingsView); }
    }

    public string Title { get; }

    public ICommand ToggleToolWindowCommand
    {
        get { return this.toggleToolWindowCommand ??= new RelayCommand<string>(this.ToggleToolWindow); }
    }

    private void Close(ICloseable? closeable)
    {
        ArgumentNullException.ThrowIfNull(closeable, nameof(closeable));

        this.logger.LogInformation($"Closing {nameof(MainViewModel)}...");

        closeable.Close();
    }

    private void CreateCone()
    {
        this.sceneManager.Scene.AddEntity(ConeEntityFactory.Instance.CreateEntity());
    }

    private void CreateCube()
    {
        this.sceneManager.Scene.AddEntity(CubeEntityFactory.Instance.CreateEntity());
    }

    private void CreateCylinder()
    {
        this.sceneManager.Scene.AddEntity(CylinderEntityFactory.Instance.CreateEntity());
    }

    private void CreateDirectionalLight()
    {
        this.sceneManager.Scene.AddEntity(DirectionalLightEntityFactory.Instance.CreateEntity());
    }

    private void CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = "Empty",
        });

        entity.AddComponent<TransformComponent>();

        this.sceneManager.Scene.AddEntity(entity);
    }

    private void CreatePlane()
    {
        this.sceneManager.Scene.AddEntity(PlaneEntityFactory.Instance.CreateEntity());
    }

    private void CreatePointLight()
    {
        this.sceneManager.Scene.AddEntity(PointLightEntityFactory.Instance.CreateEntity());
    }

    private void CreateSphere()
    {
        this.sceneManager.Scene.AddEntity(SphereEntityFactory.Instance.CreateEntity());
    }

    private void CreateSpotLight()
    {
        this.sceneManager.Scene.AddEntity(SpotLightEntityFactory.Instance.CreateEntity());
    }

    private void CreateTorus()
    {
        this.sceneManager.Scene.AddEntity(TorusEntityFactory.Instance.CreateEntity());
    }

    private void ResetWindowLayout()
    {
        this.layoutManager.ResetLayout();
    }

    private void ShowManageWindowLayoutsView()
    {
        this.viewPresenter.ShowDialog<IManageWindowLayoutsViewModel>();
    }

    private void ShowRenderingSettingsView()
    {
        this.viewPresenter.ShowView<IRenderingSettingsViewModel>();
    }

    private void ShowSaveWindowLayoutView()
    {
        this.viewPresenter.ShowDialog<ISaveWindowLayoutViewModel>();
    }

    private void ToggleToolWindow(string? contentID)
    {
        this.logger.LogInformation($"Toggling tool view with ID: '{contentID}'...");

        if (string.IsNullOrWhiteSpace(contentID))
        {
            throw new ArgumentException($"'{nameof(contentID)}' cannot be null or whitespace.", nameof(contentID));
        }

        this.layoutManager.ToggleToolWindow(contentID);
    }
}
