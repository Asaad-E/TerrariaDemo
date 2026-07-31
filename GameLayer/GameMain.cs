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

    static Camera2D camera;


    public static bool InitGame()
    {
        // load asset
        AssetManager.LoadAll();

        gameData = new();

        camera.Target = Vector2.Zero;
        camera.Rotation = 0f;
        camera.Zoom = 95;


        // init word
        gameMap = new(40, 40);
        gameMap.GetBlockUnsafe(0, 0).Type = Block.Types.Dirt;
        gameMap.GetBlockUnsafe(1, 1).Type = Block.Types.Dirt;

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

        camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);


        Vector2 direction = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) camera.Target.Y -= deltaTime * 7;
        if (Raylib.IsKeyDown(KeyboardKey.A)) camera.Target.X -= deltaTime * 7;
        if (Raylib.IsKeyDown(KeyboardKey.S)) camera.Target.Y += deltaTime * 7;
        if (Raylib.IsKeyDown(KeyboardKey.D)) camera.Target.X += deltaTime * 7;

        if (Raylib.IsKeyDown(KeyboardKey.Q)) camera.Zoom += 0.1f;
        if (Raylib.IsKeyDown(KeyboardKey.E)) camera.Zoom -= 0.1f;

        if (Raylib.IsKeyPressed(KeyboardKey.Space)) gameData = new();

        if (direction.LengthSquared() != 0)
        {
            gameData.Position += Vector2.Normalize(direction) * gameData.Speed * deltaTime;
        }


        Raylib.BeginMode2D(camera);
        // Raylib.DrawRectangleV(gameData.Position, new Vector2(40, 40), Color.Red);
        for (int i = 0; i < gameMap.Width; i++)
        {
            for (int j = 0; j < gameMap.Height; j++)
            {
                // SKip all except Dirt
                if (gameMap.GetBlockUnsafe(i, j).Type != Block.Types.Dirt) continue;

                float x = i;
                float y = j;

                Raylib.DrawTexturePro(
                    AssetManager.Dirt,
                    new Rectangle(Vector2.Zero, AssetManager.Dirt.Dimensions),
                    new Rectangle(x, y, 1, 1),
                    Vector2.Zero,
                    0,
                    Color.White);
            }
        }
        Raylib.EndMode2D();


        return true;
    }

    public static void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}