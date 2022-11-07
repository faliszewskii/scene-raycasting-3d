using Assimp;

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
            _view.Move(new Vector2D((float)viewPictureBox.Width/2, (float)viewPictureBox.Height/2));
        }

        private void loadSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _view.LoadScene(openFileDialog.FileName);
            }
            viewPictureBox.Refresh();
        }

        private void kdTrackBar_Scroll(object sender, EventArgs e)
        {
            _view.polygonFiller.Kd = (float)kdTrackBar.Value/100;
            ksTrackBar.Value = 100 - kdTrackBar.Value;
            _view.polygonFiller.Ks = 1 - _view.polygonFiller.Kd;
            _view.Refresh();
            viewPictureBox.Refresh();
        }

        private void ksTrackBar_Scroll(object sender, EventArgs e)
        {
            _view.polygonFiller.Ks = (float)ksTrackBar.Value/100;
            kdTrackBar.Value = 100 - ksTrackBar.Value;
            _view.polygonFiller.Kd = 1 - _view.polygonFiller.Ks;
            _view.Refresh();
            viewPictureBox.Refresh();
        }

        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            _view.polygonFiller.M = mTrackBar.Value;
            _view.Refresh();
            viewPictureBox.Refresh();
        }

        private void sunZTrackBar_Scroll(object sender, EventArgs e)
        {
            _view.theSun = new Vector3D(_view.theSun.X, _view.theSun.Y, sunZTrackBar.Value);
            _view.Refresh();
            viewPictureBox.Refresh();
        }

        private void colorPickButton_Click(object sender, EventArgs e)
        {

        }

        private void modifyNormalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void colorInterpolationRB_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void normalInterpolationRB_CheckedChanged(object sender, EventArgs e)
        {
            _view.polygonFiller.NormalInterpolation = (sender as RadioButton).Checked;
            _view.Refresh();
            viewPictureBox.Refresh();
        }
    }

    
    
    
}