// <copyright file="DirectionalLightEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Entities;

using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Lighting;

public sealed class DirectionalLightEntityFactory : IEntityFactory
{
    private static IEntityFactory? instance;

    public static IEntityFactory Instance
    {
        get { return instance ??= new DirectionalLightEntityFactory(); }
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = "Directional Light",
        });

        entity.AddComponent<TransformComponent>();
        entity.AddComponent(new LightComponent()
        {
            Type = LightType.Directional,
        });

        return entity;
    }
}
