using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public static class AssetManager
{
    public static Texture2D Dirt;

    public static void LoadAll()
    {
        Dirt = Raylib.LoadTexture(Path.Combine(AppContext.BaseDirectory, "Resources", "dirt.png"));
    }

    public static void ClearAll()
    {
        Raylib.UnloadTexture(Dirt);
    }
}