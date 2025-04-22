// <copyright file="DebugGUI.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.ViewModels;

using System;
using System.Drawing;
using FinalEngine.Maths.Extensions;
using FinalEngine.Rendering.Effects;
using ImGuiNET;

public static class DebugUI
{
    public static void RenderFogWindow()
    {
        // Ensure the singleton instance is initialized
        var fog = FogRenderEffect.Instance;
        if (fog == null)
        {
            return;
        }

        if (!ImGui.Begin("Fog Settings"))
        {
            ImGui.End();
            return;
        }

        // Enabled checkbox
        bool enabled = fog.Enabled;
        if (ImGui.Checkbox("##FogEnabled", ref enabled))
        {
            fog.Enabled = enabled;
        }

        ImGui.SameLine();
        ImGui.Text("Fog Enabled");

        // Fog type combo box
        int typeIdx = (int)fog.Type;
        string[] typeNames = Enum.GetNames(typeof(FogType));
        if (ImGui.Combo("Fog Type", ref typeIdx, typeNames, typeNames.Length))
        {
            fog.Type = (FogType)typeIdx;
        }

        // Color picker (RGBA)
        var colorVec = fog.Color.ToVector4(); // RGBA
        if (ImGui.ColorEdit4("Fog Color", ref colorVec))
        {
            fog.Color = Color.FromArgb((int)(colorVec.X * 255.0f), (int)(colorVec.Y * 255.0f), (int)(colorVec.Z * 255.0f));
        }

        // Parameters based on fog type
        if (fog.Type == FogType.Linear)
        {
            // Linear requires start and end
            float startVal = fog.Start;
            if (ImGui.SliderFloat("Start", ref startVal, 0.0f, fog.End))
            {
                fog.Start = startVal;
            }

            float endVal = fog.End;
            if (ImGui.SliderFloat("End", ref endVal, fog.Start, 1000.0f))
            {
                fog.End = endVal;
            }
        }
        else
        {
            float densityVal = fog.Density;
            if (ImGui.DragFloat("Density", ref densityVal, 0.0001f, 0.0f, 10.0f, "%.6e"))
            {
                fog.Density = densityVal;
            }
        }

        ImGui.End();
    }

    public static void RenderGammaWindow()
    {
        var gamma = GammaCorrectionRenderEffect.Instance;

        if (gamma == null)
        {
            return;
        }

        if (!ImGui.Begin("Gamma Settings"))
        {
            ImGui.End();
            return;
        }

        bool enabled = GammaCorrectionRenderEffect.Instance.Enabled;

        if (ImGui.Checkbox("Enabled", ref enabled))
        {
            gamma.Enabled = enabled;
        }

        ImGui.End();
    }
}
