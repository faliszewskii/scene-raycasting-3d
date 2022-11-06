namespace scene_raycasting_3D.utility;

public static class Utility
{
    public static void ForRange(int init, int stopCond, Action<int> action)
    {
        for (int i = init; i < stopCond; i++)
            action(i);
    }
}