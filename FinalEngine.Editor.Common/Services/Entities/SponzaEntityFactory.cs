// <copyright file="SponzaEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Entities;

using System;
using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Editor.Common.Services.Scenes;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Resources;

public sealed class SponzaEntityFactory : IEntityFactory
{
    private readonly ISceneManager sceneManager;

    public SponzaEntityFactory(ISceneManager sceneManager)
    {
        this.sceneManager = sceneManager ?? throw new ArgumentNullException(nameof(sceneManager));
    }

    public Entity CreateEntity()
    {
        var root = new Entity();

        this.Create(ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Sponza\\sponza.obj"));

        return root;
    }

    private void Create(Model model, bool rotate = false)
    {
        var renderModel = model.RenderModel;

        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = model.Name,
        });

        entity.AddComponent(new MeshComponent()
        {
            Mesh = renderModel.Mesh,
            Material = renderModel.Material,
        });

        entity.AddComponent(renderModel.Transform);

        this.sceneManager.Scene.AddEntity(entity);

        foreach (var child in model.Children)
        {
            this.Create(child);
        }
    }
}
