namespace scene_raycasting_3D
{
    partial class SceneRaycasting3D : Form
    {
        Scene scene;
        public SceneRaycasting3D()
        {
            InitializeComponent();
            scene = InitializeScene();
            scene.Refresh();
        }

        private Scene InitializeScene()
        {
            DirectBitmap directBitmap = new DirectBitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = directBitmap.Bitmap;
            return new Scene(directBitmap);
        }
    }
}