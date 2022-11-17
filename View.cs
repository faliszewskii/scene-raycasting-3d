using Assimp;
using scene_raycasting_3D.geometry;
using scene_raycasting_3D.utility;
using static scene_raycasting_3D.utility.Utility;

namespace scene_raycasting_3D
{
    internal class View
    {
        private DirectBitmap _bitmap;

        public DirectBitmap Bitmap
        {
            get => _bitmap;
            set
            {
                _bitmap = value;
                BitMapLength = _bitmap.Width > _bitmap.Height ? _bitmap.Width : _bitmap.Height;
            }
        }

        public SunAnimator sunAnimator;
        public PolygonFiller polygonFiller;
        public Utilities ut;
        private Scene _scene;
        public List<Polygon> _polygons;
        
        private Vector2D _offset;
        public Vector3D theSun;
        private Vector3D _camera;
        private float BitMapLength { get; set; }

        public View()
        {
            _bitmap = new DirectBitmap(1,1);
            _scene = new Scene();
            theSun = new Vector3D(0,0,1000);
            _camera = new Vector3D(0,0,1000);
            polygonFiller = new PolygonFiller();
            sunAnimator = new SunAnimator(new Vector2D(theSun.X, theSun.Y), 0.2, 20);
            ut = new Utilities();
            BitMapLength = 0;
        }
        
        public void LoadScene(String fileName)
        {
            _scene = new AssimpContext().ImportFile(fileName);

            var vertices = _scene.Meshes.SelectMany(m => m.Vertices).ToList();
            float xMin = vertices.Min(v => v.X);
            float xMax = vertices.Max(v => v.X);
            float yMin = vertices.Min(v => v.Y);
            float yMax = vertices.Max(v => v.Y);
            float xLength = xMax - xMin;
            float yLength = yMax - yMin;
            float objLength = xLength > yLength ? xLength : yLength;

            _polygons = _scene.Meshes.SelectMany(mesh => 
                mesh.Faces.Select(face => 
                    new Polygon(
                        face.Indices.Select(i => mesh.Vertices[i] * _bitmap.Height / objLength).ToList(),
                        face.Indices.Select(i => mesh.Normals[i]).ToList(),
                        face.Indices.Select(i => new Vector3D(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z)).ToList()
                        )
                    )
                ).ToList();

            Refresh();
        }
        public void Refresh()
        {
            ForRange(0, _bitmap.Width,  i =>
                ForRange(0, _bitmap.Height,  j =>
                    _bitmap.SetPixel(i, j, Color.MintCream)
                )
            );
            _polygons.ForEach( p =>
            {
                polygonFiller.Fill(p, theSun, _camera, _bitmap, _offset);
            });

            float sunX = Math.Max(0, Math.Min(theSun.X + _offset.X, _bitmap.Width-2)); 
            float sunY = Math.Max(0, Math.Min(theSun.Y + _offset.Y, _bitmap.Height-1));
            _bitmap.SetPixel(sunX, sunY, Color.White);
            _bitmap.SetPixel(sunX+1, sunY, Color.Black);
        }


        public void Move(Vector2D moveVector)
        {
            _offset += moveVector;
        }

        public void MoveTheSun(Vector2D moveVector)
        {
            theSun.X += moveVector.X;
            theSun.Y += moveVector.Y;
            sunAnimator.TheMiddle = new Vector2D(theSun.X, theSun.Y);
        }
        public void MoveTheSunTo(Vector2D destination)
        {
            theSun.X = destination.X;
            theSun.Y = destination.Y;
        }

        public void LoadNormal(string fileName)
        {
            Bitmap normalBitmap = new Bitmap(fileName);
            _polygons.ForEach(p => p.NormalTexture = normalBitmap);
        }

        

        public void LoadTexture(string fileName)
        {
            Bitmap textureBitmap = new Bitmap(fileName);
            _polygons.ForEach(p => p.Texture = textureBitmap);
        }

        public void AnimateSun()
        {
            MoveTheSunTo(sunAnimator.nextPosition());
        }
    }
}