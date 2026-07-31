using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;


struct GameData
{
    public Vector2 Position = new(0, 0);
    public float rotation = 0;
    public float Speed = 700;
    public float AngularSpeed = 200;

    public GameData(){}
}

public static class GameMain
{

    static GameData gameData;

    public static bool InitGame()
    {
        AssetManager.LoadAll();
        gameData = new();

        return true;
    }

    public static bool UpdateGame(float deltaTime)
    {

        deltaTime = MathF.Min(deltaTime, 1 / 5f);

        Vector2 direction = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) direction.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) direction.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A)) direction.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) direction.X += 1;

        if (Raylib.IsKeyDown(KeyboardKey.Q)) gameData.rotation -= gameData.AngularSpeed*deltaTime;
        if (Raylib.IsKeyDown(KeyboardKey.E)) gameData.rotation += gameData.AngularSpeed*deltaTime;


        if (Raylib.IsKeyPressed(KeyboardKey.Space)) gameData = new();

        if (direction.LengthSquared() != 0)
        {
            gameData.Position += Vector2.Normalize(direction) * gameData.Speed * deltaTime;
        }

        // Raylib.DrawRectangleV(gameData.Position, new Vector2(40, 40), Color.Red);

        Raylib.DrawTexturePro(AssetManager.dirt, new Rectangle(0, 0, AssetManager.dirt.Dimensions), new Rectangle(gameData.Position, 100, 100), new Vector2(50, 50), gameData.rotation, Color.White);

        return true;
    }

    public static void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}