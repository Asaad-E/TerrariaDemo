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
        State.Map = new(700, 700);
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

        for (int x = 0; x < State.Map.Width; x++)
        {
            for (int y = 0; y < State.Map.Height; y++)
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
        State.Camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() /2);

        // Cursor
        Vector2 mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), State.Camera);
        State.CursorX = (int)MathF.Floor(mousePos.X);
        State.CursorY = (int)MathF.Floor(mousePos.Y);

        // Add Block

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Block? selectedBlock = State.Map.GetBlockSafe(State.CursorX, State.CursorY);
            if (selectedBlock is not null)
            {
                selectedBlock.Type = Block.Types.SandRuby;
            }
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Right))
        {
            Block? selectedBlock = State.Map.GetBlockSafe(State.CursorX, State.CursorY);
            if (selectedBlock is not null)
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

        Vector2 topLeft = Raylib.GetScreenToWorld2D(Vector2.Zero, State.Camera);
        Vector2 bootmRigth = Raylib.GetScreenToWorld2D(new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()), State.Camera);

        int topLeftX = (int)MathF.Floor(topLeft.X - 1);
        int topLeftY = (int)MathF.Floor(topLeft.Y - 1);
        int bootmRigthX = (int)MathF.Floor(bootmRigth.X + 1);
        int bootmRigthY = (int)MathF.Floor(bootmRigth.Y + 1);


        topLeftX = (int)Raymath.Clamp(topLeftX, 0, State.Map.Width - 1);
        topLeftY = (int)Raymath.Clamp(topLeftY, 0, State.Map.Height - 1);

        bootmRigthX = (int)Raymath.Clamp(bootmRigthX, 0, State.Map.Width - 1);
        bootmRigthY = (int)Raymath.Clamp(bootmRigthY, 0, State.Map.Height - 1);



        Raylib.BeginMode2D(State.Camera);


        // Draw map
        for (int i = topLeftX; i < bootmRigthX; i++)
        {
            for (int j = topLeftY; j < bootmRigthY; j++)
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