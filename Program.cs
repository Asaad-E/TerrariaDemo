using System;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

namespace TerrariaDemo;

internal static class Program
{
    static int value = 0;
    static String name = "";

    [System.STAThread]
    static void Main()
    {
        // Raylib init
        Raylib.InitWindow(1280, 720, "Test");
        Raylib.SetTargetFPS(60);

        // Imgui init
        rlImGui.Setup(true);
        ImGuiIOPtr io = ImGui.GetIO();
        io.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

        // Main Loop
        while (!Raylib.WindowShouldClose())
        {
            // New Frame
            float deltaTime = Raylib.GetFrameTime();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);


            Raylib.DrawText($"FPS: {1 / deltaTime:F2}", 20, 20, 20, Color.Black);

            // Imgui
            DrawUI();

            Raylib.EndDrawing();
        }

        rlImGui.Shutdown();
        Raylib.CloseWindow();
    }

    static void DrawUI()
    {
        // init
        rlImGui.Begin();

        // Docking settings
        ImGui.PushStyleColor(ImGuiCol.WindowBg, Vector4.Zero);
        ImGui.PushStyleColor(ImGuiCol.DockingEmptyBg, Vector4.Zero);
        ImGui.DockSpaceOverViewport(ImGui.GetMainViewport().ID);
        ImGui.PopStyleColor(2);

        // Main UI
        if (ImGui.Begin("Test"))
        {
            ImGui.SeparatorText("Game options");
            ImGui.SliderInt("Speed", ref value, 1, 10);
        }
        ImGui.End();
        // ImGui.ShowDemoWindow();

        // Debug
        if (ImGui.Begin("Debug"))
        {
            ImGui.Text("Speed");
            ImGui.SameLine();
            ImGui.TextDisabled("(?)");

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.Text("Control how fast the player moves.");
                ImGui.EndTooltip();
            }

            ImGuiInputTextFlags flags = ImGuiInputTextFlags.CharsNoBlank | ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.AutoSelectAll;
            if(ImGui.InputText("Player Name: ", ref name, 128, flags))
            {
                Console.WriteLine(name);
            }
        }

        ImGui.End();

        // end
        rlImGui.End();
    }
}
