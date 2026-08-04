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

    public GameMap(int w, int h)
    {
        Width = w;
        Height = h;

        Data = new Block[w * h];

        // Initialize each block to deafult
        for (int i = 0; i < Width * Height; i++)
        {
            Data[i] = new();
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

    public readonly void ClearBlock(int x, int y)
    {
        Data[x + y * Width] = new();
    }
}