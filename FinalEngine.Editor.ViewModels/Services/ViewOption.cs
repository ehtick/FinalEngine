// <copyright file="ViewOption.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services;

using System;

public interface INamedViewModel
{
    string DisplayName { get; }
}

public sealed class ViewOption
{
    public ViewOption(string name, INamedViewModel viewModel)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        this.Name = name;
        this.ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public string Name { get; set; }

    public INamedViewModel ViewModel { get; set; }
}
