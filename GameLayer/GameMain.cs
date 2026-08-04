using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public class GameState()
{
    public Camera2D Camera;
    public GameMap Map;

    public int CursorX, CursorY;
}

public class GameMain
{
    readonly GameState State = new();
    public GameMain() { }
    public bool InitGame()
    {
        // load asset
        AssetManager.LoadAll();

        State.Camera.Target = Vector2.Zero;
        State.Camera.Rotation = 0f;
        State.Camera.Zoom = 40;


        // init word
        State.Map = new(40, 40);
        State.Map.GetBlockUnsafe(0, 0).Type = Block.Types.Dirt;
        State.Map.GetBlockUnsafe(1, 1).Type = Block.Types.GrassBlock;
        State.Map.GetBlockUnsafe(2, 2).Type = Block.Types.Stone;
        State.Map.GetBlockUnsafe(3, 3).Type = Block.Types.PoisonBlock;

        for (int x = 3; x < State.Map.Width - 3; x++)
        {
            State.Map.GetBlockUnsafe(x, 4).Type = Block.Types.GrassBlock;
        }

        for (int x = 3; x < State.Map.Width - 3; x++)
        {
            for (int y = 5; y < 10; y++)
            {
                State.Map.GetBlockUnsafe(x, y).Type = Block.Types.Dirt;
            }
        }

        return true;
    }
    public bool Update(float deltaTime)
    {
        // Fixed a min delta time equal of 5 frames
        deltaTime = MathF.Min(deltaTime, 1 / 5f);

        // To do: change only when resize
        State.Camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() * 0.4f);

        // Cursor
        Vector2 mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), State.Camera);
        State.CursorX = (int)MathF.Floor(mousePos.X);
        State.CursorY = (int)MathF.Floor(mousePos.Y);

        // Add Block

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Block? selectedBlock = State.Map.GetBlockSafe(State.CursorX, State.CursorY);
            if(selectedBlock is not null)
            {
                selectedBlock.Type = Block.Types.SandRuby;
            }
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Right))
        {
            Block? selectedBlock = State.Map.GetBlockSafe(State.CursorX, State.CursorY);
            if(selectedBlock is not null)
            {
                State.Map.ClearBlock(State.CursorX, State.CursorY);
            }
        }


        Vector2 direction = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.W)) State.Camera.Target.Y -= deltaTime * 10;
        if (Raylib.IsKeyDown(KeyboardKey.A)) State.Camera.Target.X -= deltaTime * 10;
        if (Raylib.IsKeyDown(KeyboardKey.S)) State.Camera.Target.Y += deltaTime * 10;
        if (Raylib.IsKeyDown(KeyboardKey.D)) State.Camera.Target.X += deltaTime * 10;

        if (Raylib.IsKeyDown(KeyboardKey.Q)) State.Camera.Zoom -= 1f;
        if (Raylib.IsKeyDown(KeyboardKey.E)) State.Camera.Zoom += 1f;

        return true;
    }
    public bool Draw()
    {
        Raylib.BeginMode2D(State.Camera);


        // Draw map
        for (int i = 0; i < State.Map.Width; i++)
        {
            for (int j = 0; j < State.Map.Height; j++)
            {
                Block CurrentBlock = State.Map.GetBlockUnsafe(i, j);

                // SKip air
                if (CurrentBlock.Type == Block.Types.Air) continue;

                float x = i;
                float y = j;

                Raylib.DrawTexturePro(
                    AssetManager.TextureAtlas,
                    AssetManager.GetRectForAtlas((int)CurrentBlock.Type, CurrentBlock.Variation, Block.Size, Block.Size),
                    new Rectangle(x, y, 1, 1),
                    Vector2.Zero,
                    0,
                    Color.White);
            }
        }

        // Draw cursor
        Raylib.DrawTexturePro(
            AssetManager.Frame,
            new Rectangle(0, 0, Block.Size, Block.Size),
            new Rectangle(State.CursorX, State.CursorY, 1, 1),
            Vector2.Zero,
                0,
            Color.White);
        Raylib.EndMode2D();

        return true;
    }
    public void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}