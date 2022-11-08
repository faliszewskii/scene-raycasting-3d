using Assimp;
using scene_raycasting_3D.geometry;
using static scene_raycasting_3D.utility.Utility;

namespace scene_raycasting_3D
{
    internal class View
    {
        public DirectBitmap Bitmap { get; set; }
        public PolygonFiller polygonFiller;
        private Scene _scene;
        private List<Polygon> _polygons;
        
        private Vector2D _offset;
        public Vector3D theSun;
        private Vector3D _camera;

        public View()
        {
            Bitmap = new DirectBitmap(1,1);
            _scene = new Scene();
            theSun = new Vector3D(0,0,1000);
            _camera = new Vector3D(0,0,1000);
            polygonFiller = new PolygonFiller();
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
                        face.Indices.Select(i => new Vector3D(
                            mesh.Vertices[i].X * Bitmap.Height / objLength,
                            mesh.Vertices[i].Y * Bitmap.Height / objLength,
                            mesh.Vertices[i].Z * Bitmap.Height / objLength
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
                polygonFiller.Fill(_polygons[i], theSun, _camera, Bitmap, _offset)
            );
            
            float sunX = Math.Max(0, Math.Min(theSun.X + _offset.X, Bitmap.Width-1)); 
            float sunY = Math.Max(0, Math.Min(theSun.Y + _offset.Y, Bitmap.Height-1));
            Bitmap.SetPixel(sunX, sunY, Color.White);
        }


        public void Move(Vector2D moveVector)
        {
            _offset += moveVector;
        }

        public void MoveTheSun(Vector2D moveVector)
        {
            theSun.X += moveVector.X;
            theSun.Y += moveVector.Y;
        }

        public void LoadNormal(string fileName)
        {
            Bitmap bitmap = new Bitmap(fileName);
            float bitMapLength = bitmap.Width > bitmap.Height ? bitmap.Width : bitmap.Height;
            
            var vertexNormalMap = _polygons.SelectMany(p => p.Vertices.Zip(p.Normals, (v, n) => new { v, n }))
                .ToDictionary(x => x.v, x => x.n);

            
        }
    }
}