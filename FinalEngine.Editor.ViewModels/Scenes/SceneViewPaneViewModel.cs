// <copyright file="SceneViewPaneViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Scenes;

using System;
using System.Linq;
using System.Numerics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Components;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Editor.ViewModels.Docking.Panes;
using FinalEngine.Editor.ViewModels.Messages.Entities;
using FinalEngine.Editor.ViewModels.Services.Gizmos;
using FinalEngine.Maths;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Components;
using ImGuiNET;
using ImGuizmoNET;
using Microsoft.Extensions.Logging;

//// TODO: Remove reference to ImGuizmo from view models.
//// TODO: Replace the calls with an interface or something.

public sealed class SceneViewPaneViewModel : PaneViewModelBase, ISceneViewPaneViewModel
{
    private readonly IGizmoController gizmoController;

    private readonly IRenderDevice renderDevice;

    private readonly ISceneManager sceneManager;

    private bool isLoaded;

    private ICommand? loadCommand;

    private IRelayCommand? renderCommand;

    private Entity? selectedEntity;

    public SceneViewPaneViewModel(
        ILogger<SceneViewPaneViewModel> logger,
        IMessenger messenger,
        ISceneManager sceneManager,
        IGizmoController gizmoController,
        IRenderDevice renderDevice)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(messenger, nameof(messenger));

        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
        this.gizmoController = gizmoController ?? throw new ArgumentNullException(nameof(gizmoController));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));

        this.Title = "Scene View";
        this.ContentID = "SceneView";

        logger.LogInformation($"Initializing {this.Title}...");

        messenger.Register<EntitySelectedMessage>(this, this.ShowGizmos);
        messenger.Register<EntityDeletedMessage>(this, this.HideGizmos);
    }

    public ICommand LoadCommand
    {
        get { return this.loadCommand ??= new RelayCommand<int>(this.Load); }
    }

    public IRelayCommand RenderCommand
    {
        get { return this.renderCommand ??= new RelayCommand(this.Render, this.CanRender); }
    }

    private bool CanRender()
    {
        return this.isLoaded;
    }

    private void HideGizmos(object recipient, EntityDeletedMessage message)
    {
        this.selectedEntity = null;
    }

    private void Load(int defaultFrameBufferTarget)
    {
        if (!this.isLoaded)
        {
            this.renderDevice.Pipeline.SetDefaultFrameBufferTarget(defaultFrameBufferTarget);

            this.sceneManager.Initialize();
            this.gizmoController.Initialize();

            this.isLoaded = true;
        }

        this.RenderCommand.NotifyCanExecuteChanged();
    }

    private void Render()
    {
        if (!this.isLoaded)
        {
            return;
        }

        var viewport = this.renderDevice.Rasterizer.GetViewport();

        this.renderDevice.Rasterizer.SetViewport(viewport);
        this.gizmoController.Resize(viewport.Width, viewport.Height);

        this.gizmoController.Update();

        this.sceneManager.Update();
        this.sceneManager.Render();

        DebugUI.RenderFogWindow();
        DebugUI.RenderGammaWindow();

        if (this.selectedEntity != null)
        {
            ImGuizmo.BeginFrame();

            ImGuizmo.SetOrthographic(false);

            var mainViewport = ImGui.GetMainViewport();
            var backgroundDrawList = ImGui.GetBackgroundDrawList(mainViewport);

            ImGuizmo.SetDrawlist(backgroundDrawList);
            ImGuizmo.SetRect(0, 0, viewport.Width, viewport.Height);

            var camera = this.sceneManager.Scene.Entities.FirstOrDefault(x =>
            {
                return x.ContainsComponent<EditorComponent>() && x.ContainsComponent<CameraComponent>();
            });

            if (camera == null)
            {
                return;
            }

            var transform = camera.GetComponent<TransformComponent>();
            var perspective = camera.GetComponent<PerspectiveComponent>();

            var projection = perspective.CreateProjectionMatrix();
            var view = transform.CreateViewMatrix(Vector3.UnitY);
            var entityTransform = this.selectedEntity.GetComponent<TransformComponent>();

            static float[] Flatten(Matrix4x4 m)
            {
                return
                [
                    m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44,
            ];
            }

            float[] viewArr = Flatten(view);
            float[] projArr = Flatten(projection);
            float[] modelArr = Flatten(entityTransform.CreateTransformationMatrix());

            ImGuizmo.Manipulate(ref viewArr[0], ref projArr[0], OPERATION.SCALE, MODE.LOCAL, ref modelArr[0]);

            float[] translation = new float[3];
            float[] rotation = new float[3]; // Rotation in degrees
            float[] scale = new float[3];

            ImGuizmo.DecomposeMatrixToComponents(ref modelArr[0], ref translation[0], ref rotation[0], ref scale[0]);

            // Convert translation and scale arrays to Vector3
            var newPosition = new Vector3(translation[0], translation[1], translation[2]);
            var newScale = new Vector3(scale[0], scale[1], scale[2]);

            // Convert rotation from degrees to radians
            var rotationRadians = new Vector3(
                MathHelper.DegreesToRadians(rotation[0]),
                MathHelper.DegreesToRadians(rotation[1]),
                MathHelper.DegreesToRadians(rotation[2])
            );

            // Create a quaternion from the Euler angles
            var newRotation = Quaternion.CreateFromYawPitchRoll(
                rotationRadians.Y,
                rotationRadians.X,
                rotationRadians.Z
            );

            // Update the TransformComponent
            entityTransform.Position = newPosition;
            entityTransform.Rotation = newRotation;
            entityTransform.Scale = newScale;
        }

        this.gizmoController.Render();
    }

    private void ShowGizmos(object recipient, EntitySelectedMessage message)
    {
        this.selectedEntity = message.Entity;
    }
}
