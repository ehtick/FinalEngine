// <copyright file="ISceneManager.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

public interface ISceneManager
{
    IScene Scene { get; }

    void Initialize();

    void Render();

    void Update();
}
