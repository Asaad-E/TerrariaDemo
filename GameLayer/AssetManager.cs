using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public static class AssetManager
{
    public static Texture2D Dirt;
    public static Texture2D TextureAtlas;
    public static Texture2D Frame  ;


    public static void LoadAll()
    {
        Dirt = Raylib.LoadTexture(Path.Combine(AppContext.BaseDirectory, "Resources", "dirt.png"));
        TextureAtlas = Raylib.LoadTexture(Path.Combine(AppContext.BaseDirectory, "Resources", "textures2.png"));
        Frame = Raylib.LoadTexture(Path.Combine(AppContext.BaseDirectory, "Resources", "frame.png"));
    }


    public static Rectangle GetRectForAtlas(int x, int y, int SizeX, int SizeY)
    {
        return new Rectangle(x*SizeX, y*SizeY, SizeX, SizeY);
    }

    public static void UnloadAssets()
    {
        Raylib.UnloadTexture(Dirt);
                Raylib.UnloadTexture(TextureAtlas);

    }
}