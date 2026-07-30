using System.Diagnostics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public static class GameMain
{

    public static bool InitGame()
    {
        return true;
    }

    public static bool UpdateGame(float deltaTime)
    {
        // Text
        Raylib.DrawText($"FPS: {1 / deltaTime:F2}", 20, 20, 20, Color.Black);

        return true;
    }

    public static void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}