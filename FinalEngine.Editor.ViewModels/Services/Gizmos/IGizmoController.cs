// <copyright file="IGizmoController.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Gizmos;

public interface IGizmoController
{
    void Initialize();

    void Render();

    void Resize(int width, int height);

    void ShowDemo();

    void Update();
}
