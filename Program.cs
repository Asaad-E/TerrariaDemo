<<<<<<< HEAD
﻿using System;
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
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1280, 720, "Test");
        Raylib.SetExitKey(KeyboardKey.Null);
        Raylib.SetTargetFPS(60);

        // Imgui init
        UIManager.InitUI();

        GameMain game = new();

        // Init game
        if (!game.InitGame())
        {
            CloseWindow();
            return;
        }

        // Main Loop
=======
﻿
using Raylib_cs;

namespace TerrariaDemo;

class Program
{
    
    static void Main()
    {
        Raylib.InitWindow(720, 480, "Test");
        Raylib.SetTargetFPS(60);

>>>>>>> 8891344 (first commint)
        while (!Raylib.WindowShouldClose())
        {
            // New Frame
            float deltaTime = Raylib.GetFrameTime();

<<<<<<< HEAD
            // Update Game
            if (!game.Update(deltaTime))
            {
                break;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkBlue);

            // Update Game
            if (!game.Draw())
            {
                break;
            }

            // Text
            // Raylib.DrawText($"FPS: {1 / deltaTime:F2}", 20, 20, 20, Color.White);
            Raylib.DrawFPS(20, 20);

            // Imgui
            UIManager.DrawUI();

            Raylib.EndDrawing();
        }

        game.CloseGame();
        CloseWindow();
    }

    public static void CloseWindow()
    {
        AssetManager.UnloadAssets();
        UIManager.CloseUI();
        Raylib.CloseWindow();
=======

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);


            Raylib.DrawText($"FPS: {1/deltaTime:F2}", 20, 20, 20, Color.Black);

            Raylib.EndDrawing();
        }
>>>>>>> 8891344 (first commint)
    }
}
