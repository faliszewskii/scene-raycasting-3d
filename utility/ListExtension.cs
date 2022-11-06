namespace scene_raycasting_3D.utility;

public static class ListExtension
{
    public static T GetModInd<T>(this IList<T> list, int i)
    {
        int size = list.Count;
        if (i < 0)
            return list[i + size * (-(i + 1) / size + 1)];
        return i >= size ? list[i % size] : list[i];
    }
}