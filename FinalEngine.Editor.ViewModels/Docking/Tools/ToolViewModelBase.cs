// <copyright file="ToolViewModelBase.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Docking.Tools;

using FinalEngine.Editor.ViewModels.Docking.Panes;

public abstract class ToolViewModelBase : PaneViewModelBase, IToolViewModel
{
    private bool isVisible;

    protected ToolViewModelBase()
    {
        this.IsVisible = true;
    }

    public bool IsVisible
    {
        get { return this.isVisible; }
        set { this.SetProperty(ref this.isVisible, value); }
    }
}
