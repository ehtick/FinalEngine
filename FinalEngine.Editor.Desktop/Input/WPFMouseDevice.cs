// <copyright file="WPFMouseDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Input;

using System;
using System.Drawing;
using FinalEngine.Editor.Desktop.External;
using FinalEngine.Input.Mouses;
using OpenTK.Wpf;
using Point = System.Windows.Point;
using WCursors = System.Windows.Input.Cursors;
using WMouseButton = System.Windows.Input.MouseButton;
using WMouseButtonEventArgs = System.Windows.Input.MouseButtonEventArgs;
using WMouseEventArgs = System.Windows.Input.MouseEventArgs;
using WMouseWheelEventArgs = System.Windows.Input.MouseWheelEventArgs;

/// <summary>
///   Provides an implementation of an <see cref="IMouseDevice"/> that wraps a WPF <see cref="GLWpfControl"/>.
/// </summary>
internal sealed class WPFMouseDevice : IMouseDevice
{
    /// <summary>
    ///   Specifies the underlying WPF control to wrap.
    /// </summary>
    private GLWpfControl? control;

    private bool suppressNextMove;

    /// <inheritdoc/>
    public event EventHandler<MouseButtonEventArgs>? ButtonDown;

    /// <inheritdoc/>
    public event EventHandler<MouseButtonEventArgs>? ButtonUp;

    /// <inheritdoc/>
    public event EventHandler<MouseMoveEventArgs>? Move;

    /// <inheritdoc/>
    public event EventHandler<MouseScrollEventArgs>? Scroll;

    /// <inheritdoc/>
    public bool ShowCursor
    {
        get
        {
            return this.control?.Cursor != WCursors.None;
        }

        set
        {
            if (this.control == null)
            {
                return;
            }

            this.control.Cursor = value ? WCursors.Arrow : WCursors.None;
        }
    }

    /// <summary>
    ///   Initializes the device by attaching to the specified control.
    /// </summary>
    /// <param name="control">
    ///   Specifies a <see cref="GLWpfControl"/> to capture input from.
    /// </param>
    public void Initialize(GLWpfControl control)
    {
        this.control = control ?? throw new ArgumentNullException(nameof(control));

        control.PreviewMouseUp += this.Control_PreviewMouseUp;
        control.PreviewMouseDown += this.Control_PreviewMouseDown;
        control.PreviewMouseMove += this.Control_PreviewMouseMove;
        control.PreviewMouseWheel += this.Control_PreviewMouseWheel;
    }

    /// <inheritdoc/>
    public void SetCursorLocation(PointF location)
    {
        var relativePoint = new Point(location.X, location.Y);
        var screenPoint = this.control.PointToScreen(relativePoint);

        Win32Native.SetCursorPos(
            (int)Math.Round(screenPoint.X),
            (int)Math.Round(screenPoint.Y));

        this.suppressNextMove = true;
    }

    private static MouseButton ConvertButton(WMouseButton button)
    {
        return button switch
        {
            WMouseButton.Left => MouseButton.Left,
            WMouseButton.Right => MouseButton.Right,
            WMouseButton.Middle => MouseButton.Middle,
            WMouseButton.XButton1 => MouseButton.Button4,
            WMouseButton.XButton2 => MouseButton.Button5,
            _ => MouseButton.Unknown,
        };
    }

    private void Control_PreviewMouseDown(object sender, WMouseButtonEventArgs e)
    {
        this.ButtonDown?.Invoke(this, new MouseButtonEventArgs
        {
            Button = ConvertButton(e.ChangedButton),
        });
    }

    private void Control_PreviewMouseMove(object sender, WMouseEventArgs e)
    {
        if (this.control == null)
        {
            return;
        }

        // Drop the synthetic move after a programmatic warp
        if (this.suppressNextMove)
        {
            this.suppressNextMove = false;
            return;
        }

        // Get position relative to the wrapped control
        var position = e.GetPosition(this.control);

        // Raise move event
        this.Move?.Invoke(this, new MouseMoveEventArgs
        {
            Location = new PointF((float)position.X, (float)position.Y),
        });
    }

    private void Control_PreviewMouseUp(object sender, WMouseButtonEventArgs e)
    {
        this.ButtonUp?.Invoke(this, new MouseButtonEventArgs
        {
            Button = ConvertButton(e.ChangedButton),
        });
    }

    private void Control_PreviewMouseWheel(object sender, WMouseWheelEventArgs e)
    {
        this.Scroll?.Invoke(this, new MouseScrollEventArgs
        {
            Offset = e.Delta,
        });
    }
}
