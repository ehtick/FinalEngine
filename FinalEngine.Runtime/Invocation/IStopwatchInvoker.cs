// <copyright file="IStopwatchInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Runtime.Invocation;

using System;

internal interface IStopwatchInvoker
{
    TimeSpan Elapsed { get; }

    bool IsRunning { get; }

    void Restart();
}
