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
            _view.Refresh();
            viewPictureBox.Refresh();
        }

        private void ksTrackBar_Scroll(object sender, EventArgs e)
        {
            _view.polygonFiller.Ks = (float)ksTrackBar.Value/100;
            _view.Refresh();
            viewPictureBox.Refresh();
        }

        private void mTrackBar_Scroll(object sender, EventArgs e)
        {
            _view.polygonFiller.M = mTrackBar.Value/100f + 1;
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
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _view.polygonFiller.ObjectColor = colorDialog1.Color;
                //_view._polygons.ForEach(p => p.Texture = null);
                viewPictureBox.Refresh();
            }
        }
        private void normalPickButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _view.LoadNormal(openFileDialog.FileName);
            }
            viewPictureBox.Refresh();
        }
        private void texturePickButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _view.LoadTexture(openFileDialog.FileName);
            }
            viewPictureBox.Refresh();
        }

        private void modifyNormalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _view.polygonFiller.UseModifiedNormals = (sender as CheckBox).Checked;
            _view.Refresh();
            viewPictureBox.Refresh();
            
        }

        private void colorInterpolationRB_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void normalInterpolationRB_CheckedChanged(object sender, EventArgs e)
        {
            _view.polygonFiller.UseNormalInterpolation = (sender as RadioButton).Checked;
            _view.Refresh();
            viewPictureBox.Refresh();
        }
    }

    
    
    
}