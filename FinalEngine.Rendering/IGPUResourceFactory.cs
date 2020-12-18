﻿// <copyright file="IGPUResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.Pipeline;
    using FinalEngine.Rendering.Textures;

    public interface IGPUResourceFactory
    {
        IIndexBuffer CreateIndexBuffer<T>(T[] data, int sizeInBytes)
            where T : struct;

        IInputLayout CreateInputLayout(IEnumerable<InputElement> elements);

        IShader CreateShader(PipelineTarget target, string sourceCode);

        IShaderProgram CreateShaderProgram(IEnumerable<IShader> shaders);

        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1011:Closing square brackets should be spaced correctly", Justification = "Conflicting with SA1018")]
        ITexture2D CreateTexture2D<T>(Texture2DDescription description, T[]? data, PixelFormat format = PixelFormat.Rgba, PixelFormat internalFormat = PixelFormat.Rgba);

        IVertexBuffer CreateVertexBuffer<T>(T[] data, int sizeInBytes, int stride)
                            where T : struct;
    }
}