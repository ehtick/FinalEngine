// <copyright file="SizeChangedEventArgs.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Events;

using System;
using System.Drawing;

public sealed class SizeChangedEventArgs : EventArgs
{
    public SizeChangedEventArgs(Rectangle bounds)
    {
        this.Bounds = bounds;
    }

    public Rectangle Bounds { get; }
}
