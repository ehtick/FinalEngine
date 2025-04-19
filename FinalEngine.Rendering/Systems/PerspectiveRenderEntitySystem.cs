// <copyright file="PerspectiveRenderEntitySystem.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FinalEngine.ECS;
using FinalEngine.ECS.Attributes;
using FinalEngine.ECS.Components;
using FinalEngine.Rendering.Cameras;
using FinalEngine.Rendering.Components;
using FinalEngine.Rendering.Effects;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

[EntitySystemProcess(EventName = "Render")]
public sealed class PerspectiveRenderEntitySystem : EntitySystemBase
{
    private readonly IRenderDevice renderDevice;

    private readonly IRenderQueue<IRenderEffect> renderEffectQueue;

    private readonly IRenderingEngine renderingEngine;

    public PerspectiveRenderEntitySystem(
        IRenderingEngine renderingEngine,
        IRenderQueue<IRenderEffect> postRenderer,
        IRenderDevice renderDevice)
    {
        this.renderingEngine = renderingEngine ?? throw new ArgumentNullException(nameof(renderingEngine));
        this.renderEffectQueue = postRenderer ?? throw new ArgumentNullException(nameof(postRenderer));
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
    }

    public ITextureCube SkyboxTexture { get; set; } = ResourceManager.Instance.LoadResource<ITextureCube>("Resources\\Textures\\Skybox\\skybox.fesk");

    protected override bool IsMatch([NotNull] IReadOnlyEntity entity)
    {
        return entity.ContainsComponent<TransformComponent>() &&
               entity.ContainsComponent<PerspectiveComponent>() &&
               entity.ContainsComponent<CameraComponent>();
    }

    protected override void Process([NotNull] IEnumerable<Entity> entities)
    {
        this.renderingEngine.SetSkybox(this.SkyboxTexture);

        foreach (var entity in entities)
        {
            this.renderEffectQueue.Enqueue(ToneMappingRenderEffect.Instance);
            this.renderEffectQueue.Enqueue(InversionRenderEffect.Instance);

            var transform = entity.GetComponent<TransformComponent>();
            var perspective = entity.GetComponent<PerspectiveComponent>();
            var camera = entity.GetComponent<CameraComponent>();

            if (camera.IsViewportDynamic)
            {
                var viewport = this.renderDevice.Rasterizer.GetViewport();

                camera.Viewport = viewport;
                perspective.AspectRatio = viewport.Width / viewport.Height;
            }

            this.renderingEngine.Render(new Camera()
            {
                Projection = perspective.CreateProjectionMatrix(),
                View = transform.CreateViewMatrix(Vector3.UnitY),
                Transform = transform.CreateTransformationMatrix(),
                Viewport = camera.Viewport,
            });
        }
    }
}
