namespace TerrariaDemo.GameLayer;

public class RandomGenerator
{
    public int Seed;
    private readonly Random Rand;
    public RandomGenerator(int seed)
    {
        Rand = new(seed);
    }

    public int NextIntRange(int min, int max)
    {
        return Rand.Next(min, max);
    }

    public float NextFloatRange(float min, float max)
    {
        return min + (max - min) * Rand.NextSingle();
    }

    public float NextFloat()
    {
        return Rand.NextSingle();
    }

    public static float NextFloatTrue()
    {
        return Random.Shared.NextSingle();
    }
}