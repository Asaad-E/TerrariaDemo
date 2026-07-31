using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public static class AssetManager
{
    public static Texture2D dirt;

    public static void LoadAll()
    {
        dirt = Raylib.LoadTexture(Path.Combine(AppContext.BaseDirectory, "Resources", "dirt.png"));
    }

    public static void ClearAll()
    {
        Raylib.UnloadTexture(dirt);
    }
}