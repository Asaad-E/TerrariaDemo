using System.Numerics;
using System.Security.Cryptography;
using Raylib_cs;
using TerrariaDemo.Helpers;

namespace TerrariaDemo.GameLayer;

public class Block
{

    public enum Types : uint
    {
        Air = 0,
        Dirt,
        GrassBlock,
        Stone,
        Grass,
        Sand,
        SandRuby,
        SandStone,
        WoodPlank,
        StoneBricks,
        Clay,
        WoodLog,
        Leaves,
        Copper,
        Iron,
        Gold,
        CopperBlock,
        IronBlock,
        GoldBlock,
        Bricks,
        Snow,
        Ice,
        RubyBlock,
        Platform,
        WorkBench,
        Glass,
        Furnace,
        Painting,
        Sappling,
        SnowBlueRuby,
        BlueRubyBlock,
        Door,
        Jar,
        Table,
        Wordrobe,
        BookShelf,
        SnowBricks,
        IceTable,
        IceWordrobe,
        IceBookShelf,
        IcePlatform,
        SandTable,
        SandWordrobe,
        SandBookShelf,
        SandPlatform,
        WoodenChest,
        IceChest,
        SandChest,
        BoneChest,
        BoneBricks,
        BoneBench,
        BoneWordrobe,
        BoneBookShelf,
        BonePlatform,
        PoisonBlock,
        BlockCount
    }

    public const int Size = 32;
    public Types Type = Types.Air;
    public int Variation = 0;

    public Block()
    {
        Variation = RandomGen.NextInt(0, 4);
    }
}