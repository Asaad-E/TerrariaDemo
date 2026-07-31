using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.GameLayer;

public class Block
{
    
    public enum Types : uint
    {
        Air,
        Dirt,
        BlockCount
    }

    public Types Type = Types.Air;

    public Block(){}
}