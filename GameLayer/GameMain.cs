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

    public GameData() { }
}

public static class GameMain
{

    static GameData gameData;

    static GameMap gameMap;

    public static bool InitGame()
    {
        AssetManager.LoadAll();
        gameData = new();
        gameMap = new(40, 40);

        for (int x = 3; x < gameMap.Width - 3; x++)
        {
            for (int y = 5; y < 10; y++)
            {
                gameMap.GetBlockUnsafe(x, y).Type = Block.Types.Dirt;
            }
        }

        return true;
    }

    public static bool UpdateGame(float deltaTime)
    {
        // Fixed a min delta time equal of 5 frames
        deltaTime = MathF.Min(deltaTime, 1 / 5f);

        Vector2 direction = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) direction.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) direction.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A)) direction.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) direction.X += 1;

        if (Raylib.IsKeyDown(KeyboardKey.Q)) gameData.rotation -= gameData.AngularSpeed * deltaTime;
        if (Raylib.IsKeyDown(KeyboardKey.E)) gameData.rotation += gameData.AngularSpeed * deltaTime;

        if (Raylib.IsKeyPressed(KeyboardKey.Space)) gameData = new();

        if (direction.LengthSquared() != 0)
        {
            gameData.Position += Vector2.Normalize(direction) * gameData.Speed * deltaTime;
        }

        // Raylib.DrawRectangleV(gameData.Position, new Vector2(40, 40), Color.Red);
        float size = 32;
        for (int i = 0; i < gameMap.Width; i++)
        {
            for (int j = 0; j < gameMap.Height; j++)
            {
                // SKip all except Dirt
                if (gameMap.GetBlockUnsafe(i, j).Type != Block.Types.Dirt) continue;

                float x = i * size;
                float y = j * size;

                Raylib.DrawTexturePro(
                    AssetManager.Dirt,
                    new Rectangle(Vector2.Zero, AssetManager.Dirt.Dimensions),
                    new Rectangle(x, y, size, size),
                    Vector2.Zero,
                    0,
                    Color.White);
            }
        }

        return true;
    }

    public static void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}