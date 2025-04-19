// <copyright file="IScene.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Scenes;

using System.Collections.Generic;
using FinalEngine.ECS;

public interface IScene : IEntityWorld
{
    IReadOnlyCollection<Entity> Entities { get; }
}
