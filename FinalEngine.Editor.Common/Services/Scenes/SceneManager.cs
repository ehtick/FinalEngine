// <copyright file="SceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Drawing;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Components;
using FinalEngine.Input;
using FinalEngine.Physics.Components;
using FinalEngine.Physics.Systems;
using FinalEngine.Rendering;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Systems;

internal sealed class SceneManager : ISceneManager
{
    private readonly IInputDriver inputDriver;

    private readonly IRenderDevice renderDevice;

    private readonly IRenderPipeline renderPipeline;

    public SceneManager(IScene scene, IInputDriver inputDriver, IRenderDevice renderDevice, IRenderPipeline renderPipeline)
    {
        this.Scene = scene ?? throw new ArgumentNullException(nameof(scene));
        this.inputDriver = inputDriver ?? throw new ArgumentNullException(nameof(inputDriver));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.renderPipeline = renderPipeline ?? throw new ArgumentNullException(nameof(renderPipeline));
    }

    public IScene Scene { get; }

    public void Initialize()
    {
        this.renderPipeline.Initialize();

        this.Scene.AddSystem<CameraUpdateEntitySystem>();
        this.Scene.AddSystem<SpinUpdateEntitySystem>();

        this.Scene.AddSystem<MeshRenderEntitySystem>();
        this.Scene.AddSystem<LightRenderEntitySystem>();
        this.Scene.AddSystem<PerspectiveRenderEntitySystem>();
        this.Scene.AddSystem<SpriteRenderEntitySystem>();

        var camera = new Entity();

        camera.AddComponent<EditorComponent>();
        camera.AddComponent<TransformComponent>();
        camera.AddComponent<CameraComponent>();
        camera.AddComponent<PerspectiveComponent>();
        camera.AddComponent<VelocityComponent>();

        this.Scene.AddEntity(camera);

        var floor = new Entity();

        floor.AddComponent(new TagComponent()
        {
            Name = "Floor",
        });

        floor.AddComponent<MeshComponent>();
        floor.AddComponent(new TransformComponent()
        {
            Scale = new Vector3(100, 1, 100),
        });

        this.Scene.AddEntity(floor);

        var cube = new Entity();

        cube.AddComponent(new TagComponent()
        {
            Name = "Cube",
        });

        cube.AddComponent<VelocityComponent>();
        cube.AddComponent<SpinComponent>();
        cube.AddComponent<MeshComponent>();
        cube.AddComponent(new TransformComponent()
        {
            Position = new Vector3(0, 40, 0),
            Scale = new Vector3(20, 20, 20),
        });

        this.Scene.AddEntity(cube);

        var light = new Entity();

        light.AddComponent(new TagComponent()
        {
            Name = "Light",
        });

        light.AddComponent(new LightComponent()
        {
            Intensity = 4,
        });

        light.AddComponent(new TransformComponent()
        {
            Position = new Vector3(0, 10, 0),
        });

        this.Scene.AddEntity(light);

    }

    public void Render()
    {
        this.renderDevice.Clear(Color.Black);
        this.Scene.ProcessAll("Render");
    }

    public void Update()
    {
        this.Scene.ProcessAll("Update");
        this.inputDriver.Update();
    }
}
