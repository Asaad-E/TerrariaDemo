
using Raylib_cs;

namespace TerrariaDemo;

class Program
{
    
    static void Main()
    {
        Raylib.InitWindow(720, 480, "Test");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            // New Frame
            float deltaTime = Raylib.GetFrameTime();


            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);


            Raylib.DrawText($"FPS: {1/deltaTime:F2}", 20, 20, 20, Color.Black);

            Raylib.EndDrawing();
        }
    }
}
