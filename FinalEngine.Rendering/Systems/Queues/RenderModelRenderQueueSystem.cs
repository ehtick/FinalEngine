// <copyright file="RenderModelRenderQueueSystem.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Systems.Queues;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Renderers;

[EntitySystemProcess(ExecutionType = GameLoopType.Render)]
public sealed class RenderModelRenderQueueSystem : EntitySystemBase
{
    private readonly IRenderQueue<RenderModel> renderQueue;

    public RenderModelRenderQueueSystem(IRenderQueue<RenderModel> renderQueue)
    {
        this.renderQueue = renderQueue ?? throw new ArgumentNullException(nameof(renderQueue));
    }

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() && entity.ContainsComponent<MeshComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            var transform = entity.GetComponent<TransformComponent>();
            var mesh = entity.GetComponent<MeshComponent>();

            var model = new RenderModel()
            {
                Mesh = mesh.Mesh,
                Material = mesh.Material,
                Transform = transform,
            };

            this.renderQueue.Enqueue(model);
        }
    }
}
