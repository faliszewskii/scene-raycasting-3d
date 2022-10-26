using scene_raycasting_3D.model;

namespace scene_raycasting_3D
{
    internal class Scene
    {
        private readonly DirectBitmap _bitmap;
        public Data Data { get; set; }
        public Scene(DirectBitmap bitmap)
        { 
            _bitmap = bitmap;
            Data = new Data();
        }
        public void Refresh()
        {
            Data.Polygons.ForEach(p => p.Fill(_bitmap, Color.SlateGray));
        }
    }
}