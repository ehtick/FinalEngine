// <copyright file="IEngineDriver.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime;

using System;

public interface IEngineDriver : IDisposable
{
    void Start();
}
