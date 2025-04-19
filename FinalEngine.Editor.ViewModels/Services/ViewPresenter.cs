// <copyright file="ViewPresenter.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Services;

using System;
using FinalEngine.Editor.Common.Services.Factories;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using Microsoft.Extensions.Logging;

public sealed class ViewPresenter : IViewPresenter
{
    private readonly ILogger<ViewPresenter> logger;

    private readonly IServiceProvider provider;

    public ViewPresenter(ILogger<ViewPresenter> logger, IServiceProvider provider)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public void ShowDialog<TViewModel>()
    {
        if (this.provider.GetService(typeof(IFactory<TViewModel>)) is not IFactory<TViewModel> factory)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} has not been registered with an {nameof(IFactory<TViewModel>)}.");
        }

        this.ShowDialog(factory.Create());
    }

    public void ShowDialog<TViewModel>(TViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        this.logger.LogInformation($"Showing dialog view for {typeof(TViewModel)}...");

        if (this.provider.GetService(typeof(IDialogable<TViewModel>)) is not IDialogable<TViewModel> view)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} couldn't be converted to an {nameof(IDialogable<TViewModel>)}.");
        }

        view.DataContext = viewModel;
        view.ShowDialog();
    }

    public void ShowView<TViewModel>()
    {
        if (this.provider.GetService(typeof(IFactory<TViewModel>)) is not IFactory<TViewModel> factory)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} has not been registered with an {nameof(IFactory<TViewModel>)}.");
        }

        this.ShowView(factory.Create());
    }

    public void ShowView<TViewModel>(TViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        this.logger.LogInformation($"Showing dialog view for {typeof(TViewModel)}...");

        if (this.provider.GetService(typeof(IShowable<TViewModel>)) is not IShowable<TViewModel> view)
        {
            throw new ArgumentException($"The specified {nameof(TViewModel)} couldn't be converted to an {nameof(IDialogable<TViewModel>)}.");
        }

        view.DataContext = viewModel;
        view.Show();
    }
}
