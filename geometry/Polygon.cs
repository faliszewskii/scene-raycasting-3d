using Assimp;

namespace scene_raycasting_3D.geometry;

public class Polygon
{
    public List<Vector3D> Vertices { get; }
    public List<Vector3D> Normals { get; }
    public List<Vector3D> ModifiedNormals { get; set; }
    public Bitmap? Texture { get; set; }
    public Bitmap? NormalTexture { get; set; }
    
    public Polygon(List<Vector3D> vertices, List<Vector3D> normals, List<Vector3D> modifiedNormals)
    {
        Vertices = vertices;
        Normals = normals;
        ModifiedNormals = modifiedNormals;
        Texture = null;
        NormalTexture = null;
    }
}