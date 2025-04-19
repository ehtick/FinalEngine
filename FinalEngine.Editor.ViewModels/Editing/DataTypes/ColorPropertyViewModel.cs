// <copyright file="ColorPropertyViewModel.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels.Editing.DataTypes;

using System.Drawing;
using System.Reflection;
using ColorPicker.Models;
using FinalEngine.Maths.Extensions;

public sealed class ColorPropertyViewModel : PropertyViewModel<Color>
{
    private ColorState state;

    public ColorPropertyViewModel(object component, PropertyInfo property)
            : base(component, property)
    {
        var color = this.Value.ToVector4();

        this.state.A = color.W;
        this.state.RGB_R = color.X;
        this.state.RGB_G = color.Y;
        this.state.RGB_B = color.Z;

        this.OnPropertyChanged(nameof(this.State));
    }

    public ColorState State
    {
        get
        {
            return this.state;
        }

        set
        {
            this.SetProperty(ref this.state, value);

            this.Value = Color.FromArgb(
                (int)(this.state.A * 255),
                (int)(this.state.RGB_R * 255),
                (int)(this.state.RGB_G * 255),
                (int)(this.state.RGB_B * 255));
        }
    }
}
