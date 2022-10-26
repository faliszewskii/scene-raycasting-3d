using System.Collections.Immutable;
using scene_raycasting_3D.geometry;
using scene_raycasting_3D.model;

namespace scene_raycasting_3D
{
    partial class SceneRaycasting3D : Form
    {
        private readonly Scene _scene;
        public SceneRaycasting3D()
        {
            InitializeComponent();
            _scene = InitializeScene();
            _scene.Refresh();
        }

        
        private Scene InitializeMockScene(DirectBitmap directBitmap)
        {
            var mockData = new List<Polygon>
            {
                new(ImmutableList<Vertex>.Empty.AddRange(new List<Vertex>
                {
                    new(50, 50),
                    new(200, 50),
                    new(300, 150),
                    new(300, 300)
                }))
            };
            
            var data = new Data { Polygons = mockData };
            var scene = new Scene(directBitmap) { Data = data };
            return scene;
        }

        private Scene InitializeScene()
        {
            var directBitmap = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = directBitmap.Bitmap;

            return InitializeMockScene(directBitmap);
        }
    }

    
    
    
}