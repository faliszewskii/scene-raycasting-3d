using Assimp;

namespace scene_raycasting_3D.geometry;

public class Polygon
{
    public List<Vector3D> Vertices { get; }
    public List<Vector3D> Normals { get; }
    
    public Polygon(List<Vector3D> vertices, List<Vector3D> normals)
    {
        Vertices = vertices;
        Normals = normals;
    }
}