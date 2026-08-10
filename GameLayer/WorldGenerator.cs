namespace TerrariaDemo.GameLayer;

public static class WorldGenerator
{
    public static GameMap GenerateWorld(int w, int h, RandomGenerator rng)
    {
        GameMap map = new(w, h, rng);

        int grassLevel = (int)(h * 0.4f);
        int stoneLevel = (int)(h * 0.9f);

        for (int x = 0; x < w; x++)
        {
            grassLevel += rng.NextIntRange(-1, 2);
            grassLevel = Math.Min(grassLevel, stoneLevel -1);
            stoneLevel += rng.NextIntRange(-1, 2);


            for (int y = 0; y < h; y++)
            {
                if (y < grassLevel)
                {
                    map.GetBlockUnsafe(x, y).SetType(Block.Types.Air);
                }
                else if (y == grassLevel)
                {
                    map.GetBlockUnsafe(x, y).SetType(Block.Types.GrassBlock);
                }
                else if (y < stoneLevel)
                {
                    map.GetBlockUnsafe(x, y).SetType(Block.Types.Dirt);
                }
                else
                {
                    if (rng.NextFloat() < 0.05)
                    {
                        map.GetBlockUnsafe(x, y).SetType(Block.Types.Gold);

                    }
                    else
                    {

                        map.GetBlockUnsafe(x, y).SetType(Block.Types.Stone);
                    }
                }
            }
        }


        return map;
    }
}