// <copyright file="PlaneEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Entities;

using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Resources;

public sealed class PlaneEntityFactory : IEntityFactory
{
    private static IEntityFactory? instance;

    public static IEntityFactory Instance
    {
        get { return instance ??= new PlaneEntityFactory(); }
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = "Plane",
        });

        entity.AddComponent<TransformComponent>();
        entity.AddComponent(new MeshComponent()
        {
            Mesh = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Plane\\plane.obj").RenderModel!.Mesh,
        });

        return entity;
    }
}
