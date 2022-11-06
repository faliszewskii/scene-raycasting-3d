using Assimp;
using scene_raycasting_3D.geometry;
using static scene_raycasting_3D.utility.Utility;

namespace scene_raycasting_3D
{
    internal class View
    {
        public DirectBitmap Bitmap { get; set; }
        private Scene _scene;
        private List<Polygon> _polygons;
        
        private Vector2D _offset;
        private Vector3D _theSun;
        private Vector3D _camera;

        public View()
        {
            Bitmap = new DirectBitmap(1,1);
            _scene = new Scene();
            _theSun = new Vector3D(0,1000,0);
            _camera = new Vector3D(0,1000,0);
        }
        
        public void LoadScene(String fileName)
        {
            _scene = new AssimpContext().ImportFile(fileName);

            var vertices = _scene.Meshes.SelectMany(m => m.Vertices).ToList();
            float xMin = vertices.Min(v => v.X);
            float xMax = vertices.Max(v => v.X);
            float yMin = vertices.Min(v => v.Z);
            float yMax = vertices.Max(v => v.Z);
            float xLength = xMax - xMin;
            float yLength = yMax - yMin;
            float objLength = xLength > yLength ? xLength : yLength;
            
            
            
            _polygons = _scene.Meshes.SelectMany(mesh => 
                mesh.Faces.Select(face => 
                    new Polygon(
                        face.Indices.Select(i => new Vector3D(
                            mesh.Vertices[i].X * Bitmap.Height / objLength,
                            mesh.Vertices[i].Z * Bitmap.Height / objLength,
                            mesh.Vertices[i].Y * Bitmap.Height / objLength
                            )).ToList(),
                        face.Indices.Select(i => mesh.Normals[i]).ToList()
                        )
                    )
                ).ToList();
            
            Refresh();
        }
        public void Refresh()
        {
            var r = new Random(123);
            ForRange(0, Bitmap.Width,  i =>
                ForRange(0, Bitmap.Height,  j =>
                    Bitmap.SetPixel(i, j, Color.Black)
                )
            );
            ForRange(0, _polygons.Count, i =>
                PolygonFillAlgorithm.Fill(_polygons[i], _theSun, _camera, Bitmap, Color.White, _offset)
            );
            
            float sunX = Math.Max(0, Math.Min(_theSun.X + _offset.X, Bitmap.Width-1)); 
            float sunY = Math.Max(0, Math.Min(_theSun.Z + _offset.Y, Bitmap.Height-1));
            Bitmap.SetPixel(sunX, sunY, Color.Yellow);
        }


        public void Move(Vector2D moveVector)
        {
            _offset += moveVector;
        }

        public void MoveTheSun(Vector2D moveVector)
        {
            _theSun.X += moveVector.X;
            _theSun.Z += moveVector.Y;
        }
    }
}