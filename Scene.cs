namespace scene_raycasting_3D
{
    internal class Scene
    {
        DirectBitmap bitmap;
        public Scene(DirectBitmap bitmap)
        { 
            this.bitmap = bitmap;
        }
        public void refresh()
        {
            bitmap.SetPixel(100,100, Color.White);
        }
    }
}