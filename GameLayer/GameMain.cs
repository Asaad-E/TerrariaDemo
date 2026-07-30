using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;


struct GameData
{
    public Vector2 Position = new(200, 200);
    public float Speed = 700;
    public GameData(){}
}

public static class GameMain
{

    static GameData gameData;


    public static bool InitGame()
    {

        gameData = new();

        return true;
    }

    public static bool UpdateGame(float deltaTime)
    {

        deltaTime = MathF.Min(deltaTime, 1/5f);

        Vector2 direction = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) direction.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) direction.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A)) direction.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) direction.X += 1;

        TerrariaDemo.Core.CustomAssert.Assert(gameData.Position.X < 500, "Player it is the left side of the sceeen");
 

        if (Raylib.IsKeyPressed(KeyboardKey.Space)) gameData = new();

        if (direction.LengthSquared() != 0)
        {
            gameData.Position += Vector2.Normalize(direction) * gameData.Speed * deltaTime;
            Console.WriteLine(gameData.Speed);
        }

        Raylib.DrawRectangleV(gameData.Position, new Vector2(40, 40), Color.Red);


        return true;
    }

    public static void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}