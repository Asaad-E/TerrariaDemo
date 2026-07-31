using System;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

using TerrariaDemo.GameLayer;
using TerrariaDemo.Core;

namespace TerrariaDemo;

internal static class Program
{
    [System.STAThread]
    static void Main()
    {
        // Raylib init
        Raylib.InitWindow(1280, 720, "Test");
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(60);

        // Imgui init
        UIManager.InitUI();

        // Init game
        if (!GameMain.InitGame())
        {
            CloseWindow();
            return;
        }

        // Main Loop
        while (!Raylib.WindowShouldClose())
        {
            // New Frame
            float deltaTime = Raylib.GetFrameTime();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkBlue);

            // Update Game
            if (!GameMain.UpdateGame(deltaTime))
            {
                break;
            }

            // Text
            Raylib.DrawText($"FPS: {1 / deltaTime:F2}", 20, 20, 20, Color.White);

            // Imgui
            UIManager.DrawUI();

            Raylib.EndDrawing();
        }

        CloseWindow();
    }

    public static void CloseWindow()
    {
        GameMain.CloseGame();
        UIManager.CloseUI();
        Raylib.CloseWindow();
    }
}
