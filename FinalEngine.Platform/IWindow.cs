// <copyright file="IWindow.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform;

using System;
using System.Drawing;
using FinalEngine.Platform.Events;

public interface IWindow : IDisposable
{
    event EventHandler<SizeChangedEventArgs>? SizeChanged;

    Rectangle ClientBounds { get; }

    Size ClientSize { get; }

    bool IsExiting { get; }

    bool IsFocused { get; }

    Size Size { get; set; }

    string Title { get; set; }

    bool Visible { get; set; }

    void Close();
}
