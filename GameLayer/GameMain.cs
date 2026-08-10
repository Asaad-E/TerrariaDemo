using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public class GameState
{
    public Camera2D Camera;
    public GameMap Map;
    public int Seed;
    public int CursorX, CursorY;
    public RandomGenerator RGN;

    public GameState(int seed)
    {
        RGN = new(seed);
    }
}

public class GameMain
{
    readonly GameState State;
    public GameMain()
    {
        State = new(8);
    }

    public bool InitGame()
    {
        // load asset
        AssetManager.LoadAll();

        State.Camera.Target = Vector2.Zero;
        State.Camera.Rotation = 0f;
        State.Camera.Zoom = 8;

        // init word

        State.Map = WorldGenerator.GenerateWorld(1000, 60, State.RGN);

        // State.Map = new(700, 700, State.RGN);
        State.Map.GetBlockUnsafe(0, 0).SetType(Block.Types.SandRuby);
        // State.Map.GetBlockUnsafe(0, 1).SetType(Block.Types.GrassBlock);
        // State.Map.GetBlockUnsafe(0, 2).SetType(Block.Types.Stone);
        // State.Map.GetBlockUnsafe(0, 3).SetType(Block.Types.Stone);

        // for (int x = 3; x < State.Map.Width - 3; x++)
        // {
        //     State.Map.GetBlockUnsafe(x, 9).SetType(Block.Types.GrassBlock);
        // }

        // for (int x = 3; x < State.Map.Width - 3; x++)
        // {
        //     for (int y = 10; y < 15; y++)
        //     {
        //         State.Map.GetBlockUnsafe(x, y).SetType(Block.Types.Dirt);
        //     }
        // }

        return true;
    }
    public bool Update(float deltaTime)
    {
        // Fixed a min delta time equal of 5 frames
        deltaTime = MathF.Min(deltaTime, 1 / 5f);

        // To do: change only when resize
        State.Camera.Offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

        // Cursor
        Vector2 mousePos = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), State.Camera);
        State.CursorX = (int)MathF.Floor(mousePos.X);
        State.CursorY = (int)MathF.Floor(mousePos.Y);

        // Add Block

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            // Block? selectedBlock = State.Map.GetBlockSafe(State.CursorX, State.CursorY);
            // if (selectedBlock is not null)
            // {
            //     selectedBlock.Type = Block.Types.SandRuby;
            // }

            State.Map.SpawnTree(State.CursorX, State.CursorY);
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Middle))
        {
            Block? selectedBlock = State.Map.GetWallUnsafe(State.CursorX, State.CursorY);
            if (selectedBlock is not null)
            {
                selectedBlock.SetType(Block.Types.StoneBricksWall);
            }
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.Right))
        {
            Block? selectedBlock = State.Map.GetBlockSafe(State.CursorX, State.CursorY);
            if (selectedBlock is not null && selectedBlock?.Type != Block.Types.Air)
            {
                State.Map.ClearBlock(State.CursorX, State.CursorY);
            }
            else
            {
                Block? selectedWall = State.Map.GetWallSafe(State.CursorX, State.CursorY);
                if (selectedWall is not null && selectedWall?.Type != Block.Types.Air)
                {
                    State.Map.ClearWalk(State.CursorX, State.CursorY);
                }
            }
        }


        Vector2 direction = Vector2.Zero;
        float cameraSpeed= 80;
        if (Raylib.IsKeyDown(KeyboardKey.W)) State.Camera.Target.Y -= deltaTime * cameraSpeed;
        if (Raylib.IsKeyDown(KeyboardKey.A)) State.Camera.Target.X -= deltaTime * cameraSpeed;
        if (Raylib.IsKeyDown(KeyboardKey.S)) State.Camera.Target.Y += deltaTime * cameraSpeed;
        if (Raylib.IsKeyDown(KeyboardKey.D)) State.Camera.Target.X += deltaTime * cameraSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.Q)) State.Camera.Zoom -= 1f;
        if (Raylib.IsKeyDown(KeyboardKey.E)) State.Camera.Zoom += 1f;
        // State.Camera.Zoom = Math.Clamp(State.Camera.Zoom, 20, 60);

        return true;
    }
    public bool Draw()
    {
        // Get coord of the screen rectangle
        Vector2 topLeft = Raylib.GetScreenToWorld2D(Vector2.Zero, State.Camera);
        Vector2 bootmRigth = Raylib.GetScreenToWorld2D(new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()), State.Camera);

        int topLeftX = (int)MathF.Floor(topLeft.X - 1);
        int topLeftY = (int)MathF.Floor(topLeft.Y - 1);
        int bootmRigthX = (int)MathF.Floor(bootmRigth.X + 1);
        int bootmRigthY = (int)MathF.Floor(bootmRigth.Y + 1);


        topLeftX = (int)Math.Clamp(topLeftX, 0, State.Map.Width - 1);
        topLeftY = (int)Math.Clamp(topLeftY, 0, State.Map.Height - 1);

        bootmRigthX = (int)Math.Clamp(bootmRigthX, 0, State.Map.Width - 1);
        bootmRigthY = (int)Math.Clamp(bootmRigthY, 0, State.Map.Height - 1);

        Raylib.BeginMode2D(State.Camera);

        // Draw walls
        for (int x = topLeftX; x < bootmRigthX; x++)
        {
            for (int y = topLeftY; y < bootmRigthY; y++)
            {
                Block CurrentWall = State.Map.GetWallUnsafe(x, y);

                // SKip air
                if (CurrentWall.Type == Block.Types.Air) continue;

                Raylib.DrawTexturePro(
                        AssetManager.TextureAtlas,
                        AssetManager.GetRectForAtlas((int)CurrentWall.Type, CurrentWall.Variation, Block.Size, Block.Size),
                        new Rectangle(x, y, 1, 1),
                        Vector2.Zero,
                        0,
                        Color.White);
            }
        }


        // Draw map
        for (int x = topLeftX; x < bootmRigthX; x++)
        {
            for (int y = topLeftY; y < bootmRigthY; y++)
            {
                Block CurrentBlock = State.Map.GetBlockUnsafe(x, y);

                // SKip air
                if (CurrentBlock.Type == Block.Types.Air) continue;


                if (CurrentBlock.Type == Block.Types.WoodLog)
                {
                    DrawCustomWoodLog(x, y, CurrentBlock.Variation);
                }
                else
                {
                    Raylib.DrawTexturePro(
                        AssetManager.TextureAtlas,
                        AssetManager.GetRectForAtlas((int)CurrentBlock.Type, CurrentBlock.Variation, Block.Size, Block.Size),
                        new Rectangle(x, y, 1, 1),
                        Vector2.Zero,
                        0,
                        Color.White);
                }
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

    public void DrawCustomWoodLog(int x, int y, int variationH)
    {
        int variation = 0;

        // If the log it is a border of the map, draw the default variation
        if (x - 1 < 0 || x + 1 > State.Map.Width || y - 1 < 0 || y + 1 > State.Map.Height)
        {
        }
        // The base
        else if (State.Map.GetBlockUnsafe(x, y + 1).Type != Block.Types.WoodLog && State.Map.GetBlockUnsafe(x, y + 1).Type != Block.Types.Air)
        {
            // Base sprite;

            if (State.Map.GetBlockUnsafe(x, y - 1).Type == Block.Types.Air)
            {
                variation = 7;
            }
            else
            {
                variation = 4;
            }
        }
        // Main body
        else
        {
            bool top = false;
            bool left = false;
            bool rigth = false;

            if (State.Map.GetBlockUnsafe(x - 1, y).Type == Block.Types.Leaves) left = true;
            if (State.Map.GetBlockUnsafe(x + 1, y).Type == Block.Types.Leaves) rigth = true;
            if (State.Map.GetBlockUnsafe(x, y - 1).Type == Block.Types.Leaves) top = true;


            variation = (top, left, rigth) switch
            {
                (true, true, true) => 5,
                (false, false, true) => 2,
                (true, false, true) => 2,
                (false, true, false) => 3,
                (true, true, false) => 3,
                (false, false, false) => 0,
                _ => 0
            };
        }

        Raylib.DrawTexturePro(
            AssetManager.TreeAtlas,
            AssetManager.GetRectForAtlas(variation, variationH, Block.Size, Block.Size),
            new Rectangle(x, y, 1, 1),
            Vector2.Zero,
            0,
            Color.White);
    }

    public void CloseGame()
    {
        Console.WriteLine("---------------- Game Closed ----------------");
    }
}