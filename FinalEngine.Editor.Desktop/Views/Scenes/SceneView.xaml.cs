// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System;
using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.Desktop.Input;
using FinalEngine.Editor.ViewModels.Scenes;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;
using MouseButtonEventArgs = System.Windows.Input.MouseButtonEventArgs;

public partial class SceneView : UserControl
{
    static SceneView()
    {
        FocusableProperty.OverrideMetadata(typeof(GLWpfControl), new FrameworkPropertyMetadata(true));
    }

    public SceneView()
    {
        this.InitializeComponent();

        this.glWpfControl.CanInvokeOnHandledEvents = false;
        this.glWpfControl.RegisterToEventsDirectly = false;

        this.glWpfControl.Focusable = true;

        this.glWpfControl.MouseDown += this.GlWpfControl_MouseDown;
        this.glWpfControl.Loaded += this.GlWpfControl_Loaded;
        this.glWpfControl.Render += this.GlWpfControl_Render;

        this.glWpfControl.Start(new GLWpfControlSettings()
        {
            MajorVersion = 4,
            MinorVersion = 6,
            GraphicsProfile = ContextProfile.Core,
            GraphicsContextFlags = ContextFlags.ForwardCompatible,
            RenderContinuously = true,
            UseDeviceDpi = true,
        });

        KeyboardDevice.Initialize(this.glWpfControl);
        MouseDevice.Initialize(this.glWpfControl);
    }

    internal static WPFKeyboardDevice KeyboardDevice { get; } = new WPFKeyboardDevice();

    internal static WPFMouseDevice MouseDevice { get; } = new WPFMouseDevice();

    private void GlWpfControl_Loaded(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is ISceneViewPaneViewModel viewModel)
        {
            viewModel.LoadCommand.Execute(this.glWpfControl.Framebuffer);
        }
    }

    private void GlWpfControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.glWpfControl.Focus();
    }

    private void GlWpfControl_Render(TimeSpan obj)
    {
        if (this.DataContext is ISceneViewPaneViewModel vm)
        {
            vm.RenderCommand.Execute(this.glWpfControl.Framebuffer);
        }
    }
}
