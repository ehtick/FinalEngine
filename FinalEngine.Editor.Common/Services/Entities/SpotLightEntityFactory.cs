// <copyright file="SpotLightEntityFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Entities;

using FinalEngine.ECS;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Lighting;

public sealed class SpotLightEntityFactory : IEntityFactory
{
    private static IEntityFactory? instance;

    public static IEntityFactory Instance
    {
        get { return instance ??= new SpotLightEntityFactory(); }
    }

    public Entity CreateEntity()
    {
        var entity = new Entity();

        entity.AddComponent(new TagComponent()
        {
            Name = "Spot Light",
        });

        entity.AddComponent<TransformComponent>();
        entity.AddComponent(new LightComponent()
        {
            Type = LightType.Spot,
        });

        return entity;
    }
}
