namespace Tankeschön
{
    partial class Tankeschön
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tankeschön));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TxtOrt1 = new System.Windows.Forms.TextBox();
            this.TxtOrt2 = new System.Windows.Forms.TextBox();
            this.CbSpritsorte = new System.Windows.Forms.ComboBox();
            this.NUDRadius = new System.Windows.Forms.NumericUpDown();
            this.BtnSuche = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DGVTankstellen = new System.Windows.Forms.DataGridView();
            this.MapControl = new GMap.NET.WindowsForms.GMapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.FlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTankstellen)).BeginInit();
            this.FlowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Coral;
            this.flowLayoutPanel1.Controls.Add(this.TxtOrt1);
            this.flowLayoutPanel1.Controls.Add(this.TxtOrt2);
            this.flowLayoutPanel1.Controls.Add(this.CbSpritsorte);
            this.flowLayoutPanel1.Controls.Add(this.NUDRadius);
            this.flowLayoutPanel1.Controls.Add(this.BtnSuche);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(144, 552);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // TxtOrt1
            // 
            this.TxtOrt1.Location = new System.Drawing.Point(3, 25);
            this.TxtOrt1.Margin = new System.Windows.Forms.Padding(3, 25, 3, 3);
            this.TxtOrt1.Name = "TxtOrt1";
            this.TxtOrt1.Size = new System.Drawing.Size(133, 22);

            this.TxtOrt1.TabIndex = 2;
            this.TxtOrt1.TextChanged += new System.EventHandler(this.TxtOrt1_TextChanged);
            // 
            // TxtOrt2
            // 
            this.TxtOrt2.Location = new System.Drawing.Point(3, 53);
            this.TxtOrt2.Name = "TxtOrt2";
            this.TxtOrt2.Size = new System.Drawing.Size(133, 22);
            this.TxtOrt2.TabIndex = 1;
            // 
            // CbSpritsorte
            // 
            this.CbSpritsorte.FormattingEnabled = true;
            this.CbSpritsorte.Location = new System.Drawing.Point(2, 93);
            this.CbSpritsorte.Margin = new System.Windows.Forms.Padding(2);
            this.CbSpritsorte.Name = "CbSpritsorte";
            this.CbSpritsorte.Size = new System.Drawing.Size(133, 24);
            this.CbSpritsorte.TabIndex = 4;
            this.CbSpritsorte.SelectedIndexChanged += new System.EventHandler(this.CbSpritsorte_SelectedIndexChanged);
            // 
            // NUDRadius
            // 
            this.NUDRadius.Location = new System.Drawing.Point(3, 111);
            this.NUDRadius.Margin = new System.Windows.Forms.Padding(2);

            this.NUDRadius.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NUDRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUDRadius.Name = "NUDRadius";
            this.NUDRadius.Size = new System.Drawing.Size(133, 22);
            this.NUDRadius.TabIndex = 3;
            this.NUDRadius.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUDRadius.ValueChanged += new System.EventHandler(this.NUDRadius_ValueChanged);
            // 
            // BtnSuche
            // 
            this.BtnSuche.BackColor = System.Drawing.Color.LightSeaGreen;
            this.BtnSuche.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnSuche.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnSuche.Font = new System.Drawing.Font("Ink Free", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSuche.ForeColor = System.Drawing.Color.Black;
            this.BtnSuche.Location = new System.Drawing.Point(3, 139);
            this.BtnSuche.Margin = new System.Windows.Forms.Padding(2);
            this.BtnSuche.Name = "BtnSuche";
            this.BtnSuche.Size = new System.Drawing.Size(133, 51);
            this.BtnSuche.TabIndex = 0;
            this.BtnSuche.Text = "Sparen";
            this.BtnSuche.UseVisualStyleBackColor = false;
            this.BtnSuche.Click += new System.EventHandler(this.BtnSuche_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 383);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 190, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // DGVTankstellen
            // 
            this.DGVTankstellen.AllowUserToAddRows = false;
            this.DGVTankstellen.AllowUserToDeleteRows = false;
            this.DGVTankstellen.AllowUserToResizeColumns = false;
            this.DGVTankstellen.AllowUserToResizeRows = false;
            this.DGVTankstellen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVTankstellen.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGVTankstellen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTankstellen.Location = new System.Drawing.Point(152, 376);
            this.DGVTankstellen.Name = "DGVTankstellen";
            this.DGVTankstellen.RowHeadersVisible = false;
            this.DGVTankstellen.RowHeadersWidth = 51;
            this.DGVTankstellen.RowTemplate.Height = 24;
            this.DGVTankstellen.ShowEditingIcon = false;
            this.DGVTankstellen.Size = new System.Drawing.Size(760, 192);
            this.DGVTankstellen.TabIndex = 6;
            // 
            // MapControl
            // 
            this.MapControl.Bearing = 0F;
            this.MapControl.CanDragMap = true;
            this.MapControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.MapControl.GrayScaleMode = false;
            this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MapControl.LevelsKeepInMemmory = 5;
            this.MapControl.Location = new System.Drawing.Point(152, 56);
            this.MapControl.Margin = new System.Windows.Forms.Padding(2);
            this.MapControl.MarkersEnabled = true;
            this.MapControl.MaxZoom = 2;
            this.MapControl.MinZoom = 2;
            this.MapControl.MouseWheelZoomEnabled = true;
            this.MapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MapControl.Name = "MapControl";
            this.MapControl.NegativeMode = false;
            this.MapControl.PolygonsEnabled = true;
            this.MapControl.RetryLoadTile = 0;
            this.MapControl.RoutesEnabled = true;
            this.MapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.MapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MapControl.ShowTileGridLines = false;

            this.MapControl.Size = new System.Drawing.Size(760, 312);

            this.MapControl.TabIndex = 7;
            this.MapControl.Zoom = 0D;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(885, 3);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.BtnSchließen_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(842, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 26);
            this.button2.TabIndex = 1;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.BtnMin_Click);
            // 
            // FlowPanel
            // 
            this.FlowPanel.BackColor = System.Drawing.Color.Black;
            this.FlowPanel.Controls.Add(this.button1);
            this.FlowPanel.Controls.Add(this.button2);
            this.FlowPanel.Controls.Add(this.textBox1);
            this.FlowPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.FlowPanel.Location = new System.Drawing.Point(0, 0);
            this.FlowPanel.Name = "FlowPanel";
            this.FlowPanel.Margin = new System.Windows.Forms.Padding(2);
            this.FlowPanel.Size = new System.Drawing.Size(925, 40);
            this.FlowPanel.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Javanese Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(15, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 0, 700, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(124, 46);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Tankeschön";
            // 
            // Tankeschön
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(925, 581);
            this.Controls.Add(this.FlowPanel);
            this.Controls.Add(this.MapControl);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.DGVTankstellen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Tankeschön";
            this.Text = "Tankeschön";
            this.Load += new System.EventHandler(this.Tankeschön_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUDRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTankstellen)).EndInit();
            this.FlowPanel.ResumeLayout(false);
            this.FlowPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridView DGVTankstellen;
        private System.Windows.Forms.TextBox TxtOrt1;
        private System.Windows.Forms.TextBox TxtOrt2;
        private System.Windows.Forms.ComboBox CbSpritsorte;
        private System.Windows.Forms.Button BtnSuche;
        private System.Windows.Forms.NumericUpDown NUDRadius;
        private GMap.NET.WindowsForms.GMapControl MapControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel FlowPanel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

