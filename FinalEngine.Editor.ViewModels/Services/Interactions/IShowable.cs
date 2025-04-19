// <copyright file="IShowable.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services.Interactions;

public interface IShowable<TViewModel>
{
    object DataContext { get; set; }

    void Show();
}
