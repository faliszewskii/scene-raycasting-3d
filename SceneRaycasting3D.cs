namespace scene_raycasting_3D
{
    internal partial class SceneRaycasting3D : Form
    {
        private readonly View _view;
        public SceneRaycasting3D()
        {

            _view = new View();
            InitializeComponent(_view);
            InitializeView();
        }

        private void InitializeView()
        {
            var directBitmap = new DirectBitmap(viewPictureBox.Width, viewPictureBox.Height);
            viewPictureBox.Image = directBitmap.Bitmap;
            _view.Bitmap = directBitmap;
        }

        private void loadSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _view.LoadScene(openFileDialog.FileName);
            }
            viewPictureBox.Refresh();
        }
    }

    
    
    
}