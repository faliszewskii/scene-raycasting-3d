using scene_raycasting_3D.FormsWidgets;

namespace scene_raycasting_3D
{
    partial class SceneRaycasting3D
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(View view)
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPictureBox = new ViewPictureBox(components, view);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1496, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSceneToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSceneToolStripMenuItem
            // 
            this.loadSceneToolStripMenuItem.Name = "loadSceneToolStripMenuItem";
            this.loadSceneToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.loadSceneToolStripMenuItem.Text = "Load Scene";
            this.loadSceneToolStripMenuItem.Click += new System.EventHandler(this.loadSceneToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.viewPictureBox.BackColor = System.Drawing.Color.Black;
            this.viewPictureBox.Location = new System.Drawing.Point(0, 27);
            this.viewPictureBox.Name = "viewPictureBox";
            this.viewPictureBox.Size = new System.Drawing.Size(800, 600);
            this.viewPictureBox.TabIndex = 1;
            this.viewPictureBox.TabStop = false;
            // 
            // SceneRaycasting3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 789);
            this.Controls.Add(this.viewPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SceneRaycasting3D";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ViewPictureBox viewPictureBox;
        private ToolStripMenuItem loadSceneToolStripMenuItem;
        private OpenFileDialog openFileDialog;
    }
}