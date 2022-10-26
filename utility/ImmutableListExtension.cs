using System.Collections.Immutable;
using System.IO.Packaging;

namespace scene_raycasting_3D.utility;

public static class ImmutableListExtension
{
    public static T GetModInd<T>(this ImmutableList<T> list, int i)
    {
        var size = list.Count;
        if (i < 0)
            return list[i + size * (-(i + 1) / size + 1)];
        if (i >= size)
            return list[i % size];
        return list[i];
    }
}