// <copyright file="NativeWindowInvoker.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Platform.Desktop.OpenTK.Invocation;

using System.Diagnostics.CodeAnalysis;
using global::OpenTK.Windowing.Desktop;

[ExcludeFromCodeCoverage(Justification = "Invocation")]
internal sealed class NativeWindowInvoker : NativeWindow, INativeWindowInvoker
{
    internal NativeWindowInvoker(NativeWindowSettings settings)
        : base(settings)
    {
    }

    public bool IsDisposed { get; private set; }

    IMouseStateInvoker INativeWindowInvoker.MouseState
    {
        get { return new MouseStateInvoker(this.MouseState); }
    }

    public void ProcessEvents()
    {
        base.ProcessEvents(0);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        this.IsDisposed = true;
    }
}
