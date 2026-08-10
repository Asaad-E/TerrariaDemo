using System.Numerics;
using Raylib_cs;

using TerrariaDemo.Core;
using TerrariaDemo.Helpers;

namespace TerrariaDemo.GameLayer;


public struct GameMap
{
    public int Width = 0;
    public int Height = 0;

    public Block[] Data;
    public Block[] WallData;

    public RandomGenerator RGN;

    public GameMap(int w, int h, RandomGenerator RGN)
    {
        Width = w;
        Height = h;
        this.RGN = RGN;

        Data = new Block[w * h];
        WallData = new Block[w * h];


        // Initialize each block to deafult
        for (int i = 0; i < Width * Height; i++)
        {
            Data[i] = new(RGN);
            WallData[i] = new(RGN);
        }
    }

    public readonly Block? GetBlockSafe(int x, int y)
    {
        CustomAssert.Check(Data?.Length == Width * Height, "Map data not initialized");

        if (x < 0 || y < 0 || x >= Width || y >= Height) return null;

        return Data?[x + y * Width];
    }
    public readonly Block GetBlockUnsafe(int x, int y)
    {
        CustomAssert.Check(Data.Length == Width * Height, "Map data not initialized");

        CustomAssert.Check(x >= 0 && x < Width && y >= 0 && y < Height, "getBlockUnsafe out of bounds error");

        return Data[x + y * Width];
    }
    public readonly Block GetWallUnsafe(int x, int y)
    {
        CustomAssert.Check(WallData.Length == Width * Height, "Wall Map data not initialized");

        CustomAssert.Check(x >= 0 && x < Width && y >= 0 && y < Height, "getBlockUnsafe out of bounds error");

        return WallData[x + y * Width];
    }
    public readonly Block? GetWallSafe(int x, int y)
    {
        CustomAssert.Check(WallData.Length == Width * Height, "Wall Map data not initialized");
        CustomAssert.Check(x >= 0 && x < Width && y >= 0 && y < Height, "getBlockUnsafe out of bounds error");

        if (x < 0 || y < 0 || x >= Width || y >= Height) return null;


        return WallData?[x + y * Width];
    }

    public readonly void ClearBlock(int x, int y)
    {
        Data[x + y * Width] = new(RGN);
    }

    public readonly void ClearWalk(int x, int y)
    {
        WallData[x + y * Width] = new(RGN);

    }

    public void SpawnTree(int x, int y)
    {

        int treeHeight = 3;
        int leafHeigth = 4;

        int leftTopX = x - 1;
        int leftTopY = y - (treeHeight + leafHeigth);

        int rigthBottomX = x + 1;
        int rigthBottomY = y + 1;

        Console.WriteLine("TRee");

        // Check if the rectangle where the tree spawn it is inside the world map
        if (!(leftTopX >= 0 && leftTopY >= 0 && rigthBottomX < Width && rigthBottomY < Height)) return;

        // Check is the blocks where the tree spawn are free
        if (GetBlockUnsafe(x, y).Type != Block.Types.Air) return;

        // Below the base must not be air
        if (GetBlockUnsafe(x, y + 1).Type == Block.Types.Air) return;

        // The tree body must be empty 
        for (int i = 1; i <= treeHeight; i++) if (GetBlockUnsafe(x, y - i).Type != Block.Types.Air) return;

        // The leafs space must be empty
        for (int i = leftTopX; i <= rigthBottomX; i++)
        {
            for (int j = leftTopY + 1; j <= leftTopY + leafHeigth; j++)
            {
                if (GetBlockUnsafe(i, j).Type != Block.Types.Air) return;
            }
        }

        // Built the tree

        // Base
        GetBlockUnsafe(x, y).SetType(Block.Types.WoodLog);

        // leaf
        for (int i = leftTopX; i <= rigthBottomX; i++)
        {
            for (int j = leftTopY + 1; j <= leftTopY + leafHeigth; j++)
            {
                GetBlockUnsafe(i, j).SetType(Block.Types.Leaves);
            }
        }

        // body
        for (int i = 1; i <= treeHeight; i++) GetBlockUnsafe(x, y - i).SetType(Block.Types.WoodLog);

        // Top
        GetBlockUnsafe(x, y - treeHeight - leafHeigth).SetType(Block.Types.Leaves);

    }
}