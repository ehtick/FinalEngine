// <copyright file="SphereEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Entities;

using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Resources;

public sealed class SphereEntityFactory : IEntityFactory
{
    private static IEntityFactory? instance;

    public static IEntityFactory Instance
    {
        get { return instance ??= new SphereEntityFactory(); }
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = "Sphere",
        });

        entity.AddComponent<TransformComponent>();
        entity.AddComponent(new MeshComponent()
        {
            Mesh = ResourceManager.Instance.LoadResource<Model>("Resources\\Models\\Sphere\\sphere.obj").RenderModel!.Mesh,
        });

        return entity;
    }
}
