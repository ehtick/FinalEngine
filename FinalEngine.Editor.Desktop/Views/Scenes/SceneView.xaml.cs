// <copyright file="SceneView.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop.Views.Scenes;

using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using FinalEngine.Editor.Desktop.Framework.Input;
using FinalEngine.Editor.ViewModels.Scenes;
using OpenTK.Windowing.Common;
using OpenTK.Wpf;

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

        this.glWpfControl.Start(new GLWpfControlSettings()
        {
            MajorVersion = 4,
            MinorVersion = 6,
            GraphicsProfile = ContextProfile.Core,
            GraphicsContextFlags = ContextFlags.ForwardCompatible,
            RenderContinuously = true,
            UseDeviceDpi = true,
        });

        KeyboardDevice.Initialize(this);
        MouseDevice.Initialize(this);
    }

    internal static WPFKeyboardDevice KeyboardDevice { get; } = new WPFKeyboardDevice();

    internal static WPFMouseDevice MouseDevice { get; } = new WPFMouseDevice();

    private void GlWpfControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (this.DataContext is SceneViewPaneViewModel vm)
        {
            int w = (int)e.NewSize.Width;
            int h = (int)e.NewSize.Height;

            vm.UpdateViewCommand.Execute(new Rectangle(0, 0, w, h));
        }
    }
}
