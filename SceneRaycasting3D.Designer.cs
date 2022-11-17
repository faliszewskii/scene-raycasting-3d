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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPictureBox = new ViewPictureBox(components, view);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.normalInterpolationRB = new System.Windows.Forms.RadioButton();
            this.colorInterpolationRB = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sunZTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mTrackBar = new System.Windows.Forms.TrackBar();
            this.ksTrackBar = new System.Windows.Forms.TrackBar();
            this.kdTrackBar = new System.Windows.Forms.TrackBar();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorPickButton = new System.Windows.Forms.Button();
            this.lightPickButton = new System.Windows.Forms.Button();
            this.normalPickButton = new System.Windows.Forms.Button();
            this.texturePickButton = new System.Windows.Forms.Button();
            this.modifyNormalCheckBox = new System.Windows.Forms.CheckBox();
            this.animateSunCheckBox = new System.Windows.Forms.CheckBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sunZTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdTrackBar)).BeginInit();
            this.SuspendLayout();
            //
            // timer
            //
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            this.timer.Enabled = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1066, 24);
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
            // viewPictureBox
            // 
            this.viewPictureBox.BackColor = System.Drawing.Color.MintCream;
            this.viewPictureBox.Location = new System.Drawing.Point(0, 27);
            this.viewPictureBox.Name = "viewPictureBox";
            this.viewPictureBox.Size = new System.Drawing.Size(800, 512);
            this.viewPictureBox.TabIndex = 1;
            this.viewPictureBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.normalInterpolationRB);
            this.groupBox1.Controls.Add(this.colorInterpolationRB);
            this.groupBox1.Location = new System.Drawing.Point(829, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color deriving method";
            // 
            // normalInterpolationRB
            // 
            this.normalInterpolationRB.AutoSize = true;
            this.normalInterpolationRB.Location = new System.Drawing.Point(22, 59);
            this.normalInterpolationRB.Name = "normalInterpolationRB";
            this.normalInterpolationRB.Size = new System.Drawing.Size(172, 19);
            this.normalInterpolationRB.TabIndex = 1;
            this.normalInterpolationRB.TabStop = true;
            this.normalInterpolationRB.Text = "Normal Vector Interpolation";
            this.normalInterpolationRB.UseVisualStyleBackColor = true;
            this.normalInterpolationRB.CheckedChanged += new System.EventHandler(this.normalInterpolationRB_CheckedChanged);
            // 
            // colorInterpolationRB
            // 
            this.colorInterpolationRB.AutoSize = true;
            this.colorInterpolationRB.Location = new System.Drawing.Point(22, 34);
            this.colorInterpolationRB.Name = "colorInterpolationRB";
            this.colorInterpolationRB.Size = new System.Drawing.Size(125, 19);
            this.colorInterpolationRB.TabIndex = 0;
            this.colorInterpolationRB.TabStop = true;
            this.colorInterpolationRB.Text = "Color Interpolation";
            this.colorInterpolationRB.UseVisualStyleBackColor = true;
            this.colorInterpolationRB.CheckedChanged += new System.EventHandler(this.colorInterpolationRB_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.sunZTrackBar);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.mTrackBar);
            this.groupBox2.Controls.Add(this.ksTrackBar);
            this.groupBox2.Controls.Add(this.kdTrackBar);
            this.groupBox2.Location = new System.Drawing.Point(829, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 252);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Properties";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Sun Z";
            // 
            // sunZTrackBar
            // 
            this.sunZTrackBar.Location = new System.Drawing.Point(48, 175);
            this.sunZTrackBar.Maximum = 1000;
            this.sunZTrackBar.Minimum = 100;
            this.sunZTrackBar.Name = "sunZTrackBar";
            this.sunZTrackBar.Size = new System.Drawing.Size(134, 45);
            this.sunZTrackBar.TabIndex = 6;
            this.sunZTrackBar.Value = 500;
            this.sunZTrackBar.Scroll += new System.EventHandler(this.sunZTrackBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "m";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "ks";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "kd";
            // 
            // mTrackBar
            // 
            this.mTrackBar.Location = new System.Drawing.Point(48, 124);
            this.mTrackBar.Maximum = 100;
            this.mTrackBar.Minimum = 1;
            this.mTrackBar.Name = "mTrackBar";
            this.mTrackBar.Size = new System.Drawing.Size(134, 45);
            this.mTrackBar.TabIndex = 2;
            this.mTrackBar.Value = 50;
            this.mTrackBar.Scroll += new System.EventHandler(this.mTrackBar_Scroll);
            // 
            // ksTrackBar
            // 
            this.ksTrackBar.Location = new System.Drawing.Point(48, 73);
            this.ksTrackBar.Maximum = 100;
            this.ksTrackBar.Name = "ksTrackBar";
            this.ksTrackBar.Size = new System.Drawing.Size(134, 45);
            this.ksTrackBar.TabIndex = 1;
            this.ksTrackBar.Value = 50;
            this.ksTrackBar.Scroll += new System.EventHandler(this.ksTrackBar_Scroll);
            // 
            // kdTrackBar
            // 
            this.kdTrackBar.Location = new System.Drawing.Point(48, 22);
            this.kdTrackBar.Maximum = 100;
            this.kdTrackBar.Name = "kdTrackBar";
            this.kdTrackBar.Size = new System.Drawing.Size(134, 45);
            this.kdTrackBar.TabIndex = 0;
            this.kdTrackBar.Value = 50;
            this.kdTrackBar.Scroll += new System.EventHandler(this.kdTrackBar_Scroll);
            // 
            // colorPickButton
            // 
            this.colorPickButton.Location = new System.Drawing.Point(829, 413);
            this.colorPickButton.Name = "colorPickButton";
            this.colorPickButton.Size = new System.Drawing.Size(110, 33);
            this.colorPickButton.TabIndex = 4;
            this.colorPickButton.Text = "Pick Object Color";
            this.colorPickButton.UseVisualStyleBackColor = true;
            this.colorPickButton.Click += new System.EventHandler(this.colorPickButton_Click);
            // 
            // lightPickButton
            // 
            this.lightPickButton.Location = new System.Drawing.Point(949, 413);
            this.lightPickButton.Name = "lightPickButton";
            this.lightPickButton.Size = new System.Drawing.Size(110, 33);
            this.lightPickButton.TabIndex = 4;
            this.lightPickButton.Text = "Pick Light Color";
            this.lightPickButton.UseVisualStyleBackColor = true;
            this.lightPickButton.Click += new System.EventHandler(this.lightPickButton_Click);
            // 
            // normalPickButton
            // 
            this.normalPickButton.Location = new System.Drawing.Point(829, 453);
            this.normalPickButton.Name = "normalPickButton";
            this.normalPickButton.Size = new System.Drawing.Size(110, 33);
            this.normalPickButton.TabIndex = 4;
            this.normalPickButton.Text = "Pick Normal Map";
            this.normalPickButton.UseVisualStyleBackColor = true;
            this.normalPickButton.Click += new System.EventHandler(this.normalPickButton_Click);
            // 
            // texturePickButton
            // 
            this.texturePickButton.Location = new System.Drawing.Point(949, 453);
            this.texturePickButton.Name = "texturePickButton";
            this.texturePickButton.Size = new System.Drawing.Size(110, 33);
            this.texturePickButton.TabIndex = 4;
            this.texturePickButton.Text = "Pick Texture Map";
            this.texturePickButton.UseVisualStyleBackColor = true;
            this.texturePickButton.Click += new System.EventHandler(this.texturePickButton_Click);
            // 
            // modifyNormalCheckBox
            // 
            this.modifyNormalCheckBox.AutoSize = true;
            this.modifyNormalCheckBox.Location = new System.Drawing.Point(829, 518);
            this.modifyNormalCheckBox.Name = "modifyNormalCheckBox";
            this.modifyNormalCheckBox.Size = new System.Drawing.Size(143, 19);
            this.modifyNormalCheckBox.TabIndex = 5;
            this.modifyNormalCheckBox.Text = "Modify Normal Vector";
            this.modifyNormalCheckBox.UseVisualStyleBackColor = true;
            this.modifyNormalCheckBox.CheckedChanged += new System.EventHandler(this.modifyNormalCheckBox_CheckedChanged);
            // 
            // animateSunCheckBox
            // 
            this.animateSunCheckBox.AutoSize = true;
            this.animateSunCheckBox.Location = new System.Drawing.Point(829, 538);
            this.animateSunCheckBox.Name = "modifyNormalCheckBox";
            this.animateSunCheckBox.Size = new System.Drawing.Size(143, 19);
            this.animateSunCheckBox.TabIndex = 5;
            this.animateSunCheckBox.Text = "Animate Sun";
            this.animateSunCheckBox.UseVisualStyleBackColor = true;
            this.animateSunCheckBox.CheckedChanged += new System.EventHandler(this.animateSunCheckBoxCheckBox_CheckedChanged);
            // 
            // SceneRaycasting3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 600);
            this.Controls.Add(this.modifyNormalCheckBox);
            this.Controls.Add(this.animateSunCheckBox);
            this.Controls.Add(this.colorPickButton);
            this.Controls.Add(this.lightPickButton);
            this.Controls.Add(this.normalPickButton);
            this.Controls.Add(this.texturePickButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.viewPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SceneRaycasting3D";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewPictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sunZTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ksTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ViewPictureBox viewPictureBox;
        private ToolStripMenuItem loadSceneToolStripMenuItem;
        private OpenFileDialog openFileDialog;
        private GroupBox groupBox1;
        private RadioButton normalInterpolationRB;
        private RadioButton colorInterpolationRB;
        private GroupBox groupBox2;
        private Label label2;
        private Label label1;
        private TrackBar mTrackBar;
        private TrackBar ksTrackBar;
        private TrackBar kdTrackBar;
        private Label label4;
        private TrackBar sunZTrackBar;
        private Label label3;
        private ColorDialog colorDialog1;
        private Button colorPickButton;
        private Button lightPickButton;
        private Button normalPickButton;
        private Button texturePickButton;
        private CheckBox modifyNormalCheckBox;
        private CheckBox animateSunCheckBox;
        private System.Windows.Forms.Timer timer;
    }
}