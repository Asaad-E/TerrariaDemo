using ImGuiNET;
using System.Numerics;

using Raylib_cs;
using rlImGui_cs;

namespace TerrariaDemo.Core;

public static class UIManager
{
    public static void InitUI()
    {
        rlImGui.Setup(true);
        ImGuiIOPtr io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;
        SapphireTheme();
    }

    public static void DrawUI()
    {
        // init
        rlImGui.Begin();

        // Docking settings
        ImGui.PushStyleColor(ImGuiCol.WindowBg, Vector4.Zero);
        ImGui.PushStyleColor(ImGuiCol.DockingEmptyBg, Vector4.Zero);
        ImGui.DockSpaceOverViewport(ImGui.GetMainViewport().ID);
        ImGui.PopStyleColor(2);

        // Main UI
        ImGui.ShowDemoWindow();

        // end
        rlImGui.End();
    }
    public static void CloseUI()
    {
        rlImGui.Shutdown();

    }
    static void SapphireTheme()
    {
        ImGuiStylePtr style = ImGui.GetStyle();

        // --- 1. Sizing and Spacing ---
        style.WindowPadding = new Vector2(10.0f, 10.0f);
        style.FramePadding = new Vector2(6.0f, 4.0f);
        style.ItemSpacing = new Vector2(8.0f, 4.0f);
        style.ScrollbarSize = 15.0f;
        style.GrabMinSize = 10.0f;

        // --- 2. Borders & Rounding ---
        style.WindowRounding = 5.0f;
        style.FrameRounding = 4.0f;
        style.PopupRounding = 4.0f;
        style.ScrollbarRounding = 12.0f;
        style.GrabRounding = 3.0f;
        style.TabRounding = 4.0f;

        style.WindowBorderSize = 1.0f;
        style.FrameBorderSize = 1.0f;

        // --- 3. Color Palette ---

        // Text
        style.Colors[(int)ImGuiCol.Text] = new Vector4(0.90f, 0.93f, 0.97f, 1.00f);
        style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.40f, 0.50f, 0.65f, 1.00f);

        // Backgrounds
        style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.07f, 0.09f, 0.12f, 1.00f);
        style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.09f, 0.12f, 0.16f, 1.00f);
        style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.07f, 0.09f, 0.12f, 0.95f);

        // Borders
        style.Colors[(int)ImGuiCol.Border] = new Vector4(0.15f, 0.25f, 0.35f, 0.70f);
        style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

        // Frames (Inputs, Checkboxes, etc.)
        style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.12f, 0.18f, 0.26f, 1.00f);
        style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.18f, 0.28f, 0.40f, 1.00f);
        style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.25f, 0.38f, 0.55f, 1.00f);

        // Title Bars
        style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.09f, 0.12f, 0.18f, 1.00f);
        style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.14f, 0.22f, 0.35f, 1.00f);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.05f, 0.08f, 0.12f, 1.00f);

        // Menus
        style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.12f, 0.16f, 0.22f, 1.00f);

        // Scrollbars
        style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.06f, 0.08f, 0.11f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.20f, 0.32f, 0.48f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.28f, 0.42f, 0.60f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.35f, 0.50f, 0.75f, 1.00f);

        // Interactables
        style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.40f, 0.70f, 1.00f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.30f, 0.55f, 0.85f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.45f, 0.75f, 1.00f, 1.00f);
        style.Colors[(int)ImGuiCol.Button] = new Vector4(0.18f, 0.35f, 0.55f, 1.00f);
        style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.25f, 0.48f, 0.75f, 1.00f);
        style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.35f, 0.60f, 0.90f, 1.00f);
        style.Colors[(int)ImGuiCol.Header] = new Vector4(0.18f, 0.35f, 0.55f, 1.00f);
        style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.25f, 0.48f, 0.75f, 1.00f);
        style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.35f, 0.60f, 0.90f, 1.00f);

        // Tabs
        style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.12f, 0.20f, 0.32f, 1.00f);
        style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.25f, 0.45f, 0.70f, 1.00f);
        style.Colors[(int)ImGuiCol.TabSelected] = new Vector4(0.18f, 0.35f, 0.55f, 1.00f);
        style.Colors[(int)ImGuiCol.TabDimmedSelected] = new Vector4(0.08f, 0.12f, 0.18f, 1.00f);
        style.Colors[(int)ImGuiCol.TabDimmed] = new Vector4(0.12f, 0.20f, 0.32f, 1.00f);

        // Tables
        style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.15f, 0.25f, 0.40f, 1.00f);
        style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.20f, 0.35f, 0.55f, 1.00f);
        style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.15f, 0.25f, 0.40f, 1.00f);
        style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.00f, 1.00f, 1.00f, 0.05f);

        // Misc
        style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.30f, 0.55f, 0.85f, 0.40f);
        style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.50f, 0.80f, 1.00f, 0.90f);
        style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.40f, 0.70f, 1.00f, 1.00f);

        // Docking
        style.Colors[(int)ImGuiCol.DockingPreview] = new Vector4(0.25f, 0.50f, 0.80f, 0.50f);
        style.Colors[(int)ImGuiCol.DockingEmptyBg] = new Vector4(0.07f, 0.09f, 0.12f, 1.00f);
    }

    static void AmethystTheme()
    {
        ImGuiStylePtr style = ImGui.GetStyle();

        // --- 1. Sizing and Spacing (Modern & Tight) ---
        style.WindowPadding = new Vector2(8.0f, 8.0f);
        style.FramePadding = new Vector2(5.0f, 3.0f);
        style.CellPadding = new Vector2(6.0f, 4.0f);
        style.ItemSpacing = new Vector2(6.0f, 4.0f);
        style.ItemInnerSpacing = new Vector2(6.0f, 4.0f);
        style.ScrollbarSize = 13.0f;
        style.GrabMinSize = 10.0f;

        // --- 2. Borders & Rounding ---
        style.WindowBorderSize = 1.0f;
        style.ChildBorderSize = 1.0f;
        style.PopupBorderSize = 1.0f;
        style.FrameBorderSize = 1.0f;

        style.WindowRounding = 4.0f;
        style.ChildRounding = 3.0f;
        style.FrameRounding = 3.0f;
        style.PopupRounding = 3.0f;
        style.ScrollbarRounding = 9.0f;
        style.GrabRounding = 3.0f;
        style.TabRounding = 3.0f;

        // --- 3. Color Palette ---

        // Text
        style.Colors[(int)ImGuiCol.Text] = new Vector4(0.92f, 0.90f, 0.95f, 1.00f);
        style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.55f, 0.50f, 0.60f, 1.00f);

        // Backgrounds
        style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.09f, 0.07f, 0.12f, 1.00f);
        style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.11f, 0.09f, 0.14f, 1.00f);
        style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.09f, 0.07f, 0.12f, 0.96f);

        // Borders
        style.Colors[(int)ImGuiCol.Border] = new Vector4(0.25f, 0.20f, 0.35f, 0.80f);
        style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);

        // Frames (Inputs, Checkboxes, etc.)
        style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.15f, 0.12f, 0.22f, 1.00f);
        style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.25f, 0.20f, 0.38f, 1.00f);
        style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.35f, 0.25f, 0.55f, 1.00f);

        // Title Bars
        style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.12f, 0.09f, 0.18f, 1.00f);
        style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.20f, 0.14f, 0.32f, 1.00f);
        style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.07f, 0.05f, 0.10f, 1.00f);

        // Menus
        style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.12f, 0.09f, 0.18f, 1.00f);

        // Scrollbars
        style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.07f, 0.05f, 0.10f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.25f, 0.20f, 0.35f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.35f, 0.30f, 0.50f, 1.00f);
        style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.45f, 0.40f, 0.65f, 1.00f);

        // Interactables
        style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.65f, 0.45f, 0.95f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.50f, 0.35f, 0.75f, 1.00f);
        style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.65f, 0.45f, 0.95f, 1.00f);
        style.Colors[(int)ImGuiCol.Button] = new Vector4(0.25f, 0.20f, 0.40f, 1.00f);
        style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.38f, 0.28f, 0.62f, 1.00f);
        style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.50f, 0.35f, 0.80f, 1.00f);
        style.Colors[(int)ImGuiCol.Header] = new Vector4(0.25f, 0.20f, 0.40f, 1.00f);
        style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.38f, 0.28f, 0.62f, 1.00f);
        style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.50f, 0.35f, 0.80f, 1.00f);

        // Tabs
        style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.15f, 0.12f, 0.25f, 1.00f);
        style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.38f, 0.28f, 0.62f, 1.00f);
        style.Colors[(int)ImGuiCol.TabSelected] = new Vector4(0.28f, 0.20f, 0.45f, 1.00f);
        style.Colors[(int)ImGuiCol.TabDimmed] = new Vector4(0.10f, 0.08f, 0.15f, 1.00f);
        style.Colors[(int)ImGuiCol.TabDimmedSelected] = new Vector4(0.15f, 0.12f, 0.25f, 1.00f);

        // Tables
        style.Colors[(int)ImGuiCol.TableHeaderBg] = new Vector4(0.18f, 0.15f, 0.28f, 1.00f);
        style.Colors[(int)ImGuiCol.TableBorderStrong] = new Vector4(0.25f, 0.20f, 0.40f, 1.00f);
        style.Colors[(int)ImGuiCol.TableBorderLight] = new Vector4(0.20f, 0.15f, 0.30f, 1.00f);
        style.Colors[(int)ImGuiCol.TableRowBgAlt] = new Vector4(1.00f, 1.00f, 1.00f, 0.04f);

        // Misc
        style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.50f, 0.35f, 0.80f, 0.35f);
        style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.80f, 0.65f, 1.00f, 0.95f);
        style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(0.60f, 0.45f, 0.90f, 1.00f);

        // Docking
        style.Colors[(int)ImGuiCol.DockingPreview] = new Vector4(0.50f, 0.35f, 0.80f, 0.50f);
        style.Colors[(int)ImGuiCol.DockingEmptyBg] = new Vector4(0.09f, 0.07f, 0.12f, 1.00f);
    }
}