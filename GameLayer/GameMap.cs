using System.Numerics;
using Raylib_cs;

using TerrariaDemo.Core;

namespace TerrariaDemo.GameLayer;


public struct GameMap
{
    public int Width = 0;
    public int Height = 0;

    public Block[] mapData;

    public GameMap(int w, int h)
    {
        Width = w;
        Height = h;

        mapData = new Block[w * h];

        // Initialize each block to deafult
        for (int i = 0; i < Width * Height; i++)
        {
            mapData[i] = new();
        }
    }

    public readonly Block? GetBlockSafe(int x, int y)
    {
        CustomAssert.Check(mapData is not null, "Map data not initialized");
        CustomAssert.Check(mapData?.Length == Width * Height, "Map data not initialized");

        if (x < 0 || y < 0 || x >= Width || y >= Height) return null;

        return mapData?[x + y * Width];
    }
    public readonly Block GetBlockUnsafe(int x, int y)
    {
        CustomAssert.Check(mapData is not null, "Map data not initialized");
        CustomAssert.Check(mapData.Length == Width * Height, "Map data not initialized");

        CustomAssert.Check(x >= 0 && x < Width && y >= 0 && y < Height, "getBlockUnsafe out of bounds error");

        return mapData[x + y * Width];
    }
}