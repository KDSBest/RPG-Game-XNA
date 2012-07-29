namespace MapEditor
{
    partial class MapEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrawLayer1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DrawLayer = new System.Windows.Forms.ComboBox();
            this.DrawLayer3 = new System.Windows.Forms.CheckBox();
            this.DrawLayer2 = new System.Windows.Forms.CheckBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.NewMap = new System.Windows.Forms.Button();
            this.TileSet = new System.Windows.Forms.ComboBox();
            this.SizeX = new System.Windows.Forms.NumericUpDown();
            this.SizeY = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeY)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawLayer1
            // 
            this.DrawLayer1.AutoSize = true;
            this.DrawLayer1.Checked = true;
            this.DrawLayer1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawLayer1.Location = new System.Drawing.Point(6, 19);
            this.DrawLayer1.Name = "DrawLayer1";
            this.DrawLayer1.Size = new System.Drawing.Size(83, 17);
            this.DrawLayer1.TabIndex = 0;
            this.DrawLayer1.Text = "DrawLayer1";
            this.DrawLayer1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DrawLayer);
            this.groupBox1.Controls.Add(this.DrawLayer3);
            this.groupBox1.Controls.Add(this.DrawLayer2);
            this.groupBox1.Controls.Add(this.DrawLayer1);
            this.groupBox1.Location = new System.Drawing.Point(18, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(646, 57);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layer";
            // 
            // DrawLayer
            // 
            this.DrawLayer.FormattingEnabled = true;
            this.DrawLayer.Items.AddRange(new object[] {
            "Layer1",
            "Layer2",
            "Layer3"});
            this.DrawLayer.Location = new System.Drawing.Point(270, 17);
            this.DrawLayer.Name = "DrawLayer";
            this.DrawLayer.Size = new System.Drawing.Size(370, 21);
            this.DrawLayer.TabIndex = 3;
            this.DrawLayer.Text = "Layer1";
            // 
            // DrawLayer3
            // 
            this.DrawLayer3.AutoSize = true;
            this.DrawLayer3.Checked = true;
            this.DrawLayer3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawLayer3.Location = new System.Drawing.Point(181, 19);
            this.DrawLayer3.Name = "DrawLayer3";
            this.DrawLayer3.Size = new System.Drawing.Size(83, 17);
            this.DrawLayer3.TabIndex = 2;
            this.DrawLayer3.Text = "DrawLayer3";
            this.DrawLayer3.UseVisualStyleBackColor = true;
            // 
            // DrawLayer2
            // 
            this.DrawLayer2.AutoSize = true;
            this.DrawLayer2.Checked = true;
            this.DrawLayer2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawLayer2.Location = new System.Drawing.Point(95, 19);
            this.DrawLayer2.Name = "DrawLayer2";
            this.DrawLayer2.Size = new System.Drawing.Size(83, 17);
            this.DrawLayer2.TabIndex = 1;
            this.DrawLayer2.Text = "DrawLayer2";
            this.DrawLayer2.UseVisualStyleBackColor = true;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(667, 86);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(25, 320);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(18, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 320);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(646, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(18, 409);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(674, 24);
            this.hScrollBar1.TabIndex = 5;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(706, 462);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.hScrollBar1);
            this.tabPage1.Controls.Add(this.vScrollBar1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(698, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.SizeY);
            this.tabPage2.Controls.Add(this.SizeX);
            this.tabPage2.Controls.Add(this.TileSet);
            this.tabPage2.Controls.Add(this.NewMap);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(698, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // NewMap
            // 
            this.NewMap.Location = new System.Drawing.Point(3, 59);
            this.NewMap.Name = "NewMap";
            this.NewMap.Size = new System.Drawing.Size(686, 23);
            this.NewMap.TabIndex = 0;
            this.NewMap.Text = "NewMap";
            this.NewMap.UseVisualStyleBackColor = true;
            this.NewMap.Click += new System.EventHandler(this.NewMap_Click);
            // 
            // TileSet
            // 
            this.TileSet.FormattingEnabled = true;
            this.TileSet.Items.AddRange(new object[] {
            "ForestTiles",
            "DungeonTiles"});
            this.TileSet.Location = new System.Drawing.Point(6, 6);
            this.TileSet.Name = "TileSet";
            this.TileSet.Size = new System.Drawing.Size(686, 21);
            this.TileSet.TabIndex = 1;
            this.TileSet.Text = "ForestTiles";
            // 
            // SizeX
            // 
            this.SizeX.Location = new System.Drawing.Point(6, 33);
            this.SizeX.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.SizeX.Name = "SizeX";
            this.SizeX.Size = new System.Drawing.Size(120, 20);
            this.SizeX.TabIndex = 2;
            this.SizeX.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // SizeY
            // 
            this.SizeY.Location = new System.Drawing.Point(132, 33);
            this.SizeY.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.SizeY.Name = "SizeY";
            this.SizeY.Size = new System.Drawing.Size(120, 20);
            this.SizeY.TabIndex = 3;
            this.SizeY.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 479);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            this.Load += new System.EventHandler(this.MapEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckBox DrawLayer1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox DrawLayer3;
        public System.Windows.Forms.CheckBox DrawLayer2;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        public System.Windows.Forms.ComboBox DrawLayer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button NewMap;
        private System.Windows.Forms.ComboBox TileSet;
        private System.Windows.Forms.NumericUpDown SizeY;
        private System.Windows.Forms.NumericUpDown SizeX;
    }
}