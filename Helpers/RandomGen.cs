using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

namespace TerrariaDemo.Helpers;


public static class RandomGen
{
    private static Random rand = new();

    public static int NextInt(int min, int max)
    {
        return rand.Next(min, max);
    }
    
}