// <copyright file="SceneRenderer.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Renderers;

using System;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Resources;

internal sealed class SceneRenderer : ISceneRenderer
{
    private readonly IGeometryRenderer geometryRenderer;

    private readonly IRenderDevice renderDevice;

    private IShaderProgram? geometryProgram;

    public SceneRenderer(IRenderDevice renderDevice, IGeometryRenderer geometryRenderer)
    {
        this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice));
        this.geometryRenderer = geometryRenderer ?? throw new ArgumentNullException(nameof(geometryRenderer));
    }

    private IShaderProgram GeometryProgram
    {
        get { return this.geometryProgram ??= ResourceManager.Instance.LoadResource<IShaderProgram>("Resources\\Shaders\\standard-geometry.fesp"); }
    }

    public void Render(ICamera camera, bool useBuiltInShader)
    {
        ArgumentNullException.ThrowIfNull(camera, nameof(camera));

        if (useBuiltInShader)
        {
            this.renderDevice.Pipeline.SetShaderProgram(this.GeometryProgram);
        }

        this.UpdateUniforms(camera);
        this.geometryRenderer.Render();
    }

    private void UpdateUniforms(ICamera camera)
    {
        this.renderDevice.Pipeline.SetUniform("u_projection", camera.Projection);
        this.renderDevice.Pipeline.SetUniform("u_view", camera.View);
        this.renderDevice.Pipeline.SetUniform("u_viewPosition", camera.Transform.Position);
    }
}
