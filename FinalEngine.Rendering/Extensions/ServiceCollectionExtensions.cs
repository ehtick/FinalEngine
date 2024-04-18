// <copyright file="ServiceCollectionExtensions.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Extensions;

using System;
using FinalEngine.Rendering.Batching;
using FinalEngine.Rendering.Geometry;
using FinalEngine.Rendering.Lighting;
using FinalEngine.Rendering.Loaders.Models;
using FinalEngine.Rendering.Loaders.Shaders;
using FinalEngine.Rendering.Loaders.Textures;
using FinalEngine.Rendering.Pipeline;
using FinalEngine.Rendering.Renderers;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources.Extensionss;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRendering(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSingleton<ISpriteBatcher, SpriteBatcher>();
        services.AddSingleton<ITextureBinder, TextureBinder>();
        services.AddSingleton<ISpriteDrawer, SpriteDrawer>();

        services.AddSingleton<IGeometryRenderer, GeometryRenderer>();
        services.AddSingleton<ILightRenderer, LightRenderer>();
        services.AddSingleton<ISkyboxRenderer, SkyboxRenderer>();
        services.AddSingleton<ISceneRenderer, SceneRenderer>();

        services.AddSingleton<IRenderQueue<RenderModel>>(x =>
        {
            return (IRenderQueue<RenderModel>)x.GetRequiredService<IGeometryRenderer>();
        });

        services.AddSingleton<IRenderQueue<Light>>(x =>
        {
            return (IRenderQueue<Light>)x.GetRequiredService<ILightRenderer>();
        });

        services.AddSingleton<IRenderCoordinator, RenderCoordinator>();
        services.AddSingleton<IRenderingEngine, RenderingEngine>();

        // TODO: Model should be IModel? Think about a better solution to the whole thing tbh.
        services.AddResourceLoader<ITexture2D, Texture2DResourceLoader>();
        services.AddResourceLoader<IShader, ShaderResourceLoader>();
        services.AddResourceLoader<IShaderProgram, ShaderProgramResourceLoader>();
        services.AddResourceLoader<Model, ModelResourceLoader>();

        return services;
    }
}
