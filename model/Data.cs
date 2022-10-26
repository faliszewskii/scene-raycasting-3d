using scene_raycasting_3D.geometry;

namespace scene_raycasting_3D.model;

internal class Data
{
    public Data()
    {
        Polygons = new List<Polygon>();
    }

    public List<Polygon> Polygons { get; set; }
    public void LoadData(Stream stream)
    {
            
    }
}