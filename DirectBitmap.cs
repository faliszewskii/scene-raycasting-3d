using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace scene_raycasting_3D
{
    // https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f
    public class DirectBitmap : IDisposable 
    {
        public Bitmap Bitmap { get; set; }
        private int[] Bits { get; set; }
        private bool Disposed { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        private GCHandle BitsHandle { get; set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new int[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + y * Width;
            int col = colour.ToArgb();

            Bits[index] = col;
        }
        public void SetPixel(float xf, float yf, Color colour)
        {
            SetPixel((int)xf, (int)yf, colour);
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + y * Width;
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }

        public bool VectorInBound(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public bool VectorInBound(float x, float y)
        {
            return VectorInBound((int)x, (int)y);
        }
    }
}
