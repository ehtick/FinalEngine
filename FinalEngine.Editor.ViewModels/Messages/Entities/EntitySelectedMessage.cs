// <copyright file="EntitySelectedMessage.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Messages.Entities;

using System;
using FinalEngine.ECS;

public sealed class EntitySelectedMessage
{
    public EntitySelectedMessage(Entity entity)
    {
        this.Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Entity Entity { get; }
}
