// <copyright file="Scene.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FinalEngine.ECS;
using FinalEngine.ECS.Blackboard;

internal sealed class Scene : IScene
{
    private readonly ObservableCollection<Entity> entities;

    private readonly IEntityWorld world;

    public Scene(IEntityWorld world)
    {
        this.world = world ?? throw new System.ArgumentNullException(nameof(world));
        this.entities = [];
    }

    public IReadOnlyCollection<Entity> Entities
    {
        get { return this.entities; }
    }

    public void AddEntity(Entity entity)
    {
        this.world.AddEntity(entity);
        this.entities.Add(entity);
    }

    public void AddResource<T>(T resource)
        where T : IBlackboardResource
    {
        this.world.AddResource<T>(resource);
    }

    public void AddSystem<TSystem>()
        where TSystem : EntitySystemBase
    {
        this.world.AddSystem<TSystem>();
    }

    public void AddSystem(EntitySystemBase system)
    {
        this.world.AddSystem(system);
    }

    public T GetResource<T>()
        where T : IBlackboardResource
    {
        return this.world.GetResource<T>();
    }

    public void ProcessAll(string eventName)
    {
        this.world.ProcessAll(eventName);
    }

    public void RemoveEntity(Entity entity)
    {
        this.world.RemoveEntity(entity);
        this.entities.Remove(entity);
    }

    public void RemoveSystem(Type type)
    {
        this.world.RemoveSystem(type);
    }
}
