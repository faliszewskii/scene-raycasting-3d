namespace scene_raycasting_3D
{
    internal class Scene
    {
        private readonly DirectBitmap _bitmap;
        public Scene(DirectBitmap bitmap)
        { 
            this._bitmap = bitmap;
        }
        public void Refresh()
        {
            _bitmap.SetPixel(100,100, Color.White);
        }
    }
}