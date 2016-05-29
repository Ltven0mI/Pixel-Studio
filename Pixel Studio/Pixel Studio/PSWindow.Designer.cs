namespace Pixel_Studio
{
    partial class PSWindow
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openThemeDialog = new System.Windows.Forms.OpenFileDialog();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerView = new Pixel_Studio.Controls.LayerView();
            this.projectView = new Pixel_Studio.Controls.ProjectView();
            this.canvas = new Pixel_Studio.Controls.Canvas();
            this.projectHandler = new Pixel_Studio.Components.ProjectHandler();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.themeToolStripMenuItem,
            this.layerToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // openThemeDialog
            // 
            this.openThemeDialog.DefaultExt = "theme";
            this.openThemeDialog.Title = "Load Theme";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newLayerToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // newLayerToolStripMenuItem
            // 
            this.newLayerToolStripMenuItem.Name = "newLayerToolStripMenuItem";
            this.newLayerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newLayerToolStripMenuItem.Text = "NewLayer";
            this.newLayerToolStripMenuItem.Click += new System.EventHandler(this.newLayerToolStripMenuItem_Click);
            // 
            // layerView
            // 
            this.layerView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layerView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.layerView.Location = new System.Drawing.Point(0, 597);
            this.layerView.Margin = new System.Windows.Forms.Padding(0);
            this.layerView.Name = "layerView";
            this.layerView.Size = new System.Drawing.Size(1264, 75);
            this.layerView.TabHeight = 25;
            this.layerView.TabIndex = 2;
            this.layerView.Visible = false;
            // 
            // projectView
            // 
            this.projectView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectView.BottomLine = 3;
            this.projectView.Location = new System.Drawing.Point(0, 24);
            this.projectView.Margin = new System.Windows.Forms.Padding(0);
            this.projectView.Name = "projectView";
            this.projectView.Size = new System.Drawing.Size(1264, 24);
            this.projectView.TabIndex = 0;
            this.projectView.TabStop = false;
            this.projectView.TabWidth = 110;
            this.projectView.Visible = false;
            // 
            // canvas
            // 
            this.canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
            this.canvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.canvas.Location = new System.Drawing.Point(0, 48);
            this.canvas.Margin = new System.Windows.Forms.Padding(0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1264, 549);
            this.canvas.TabIndex = 0;
            this.canvas.Visible = false;
            // 
            // projectHandler
            // 
            this.projectHandler.Canvas = this.canvas;
            this.projectHandler.LayerView = this.layerView;
            this.projectHandler.ProjectView = this.projectView;
            // 
            // PSWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 673);
            this.Controls.Add(this.layerView);
            this.Controls.Add(this.projectView);
            this.Controls.Add(this.canvas);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "PSWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pixel Studio";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Canvas canvas;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openThemeDialog;
        public Components.ProjectHandler projectHandler;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private Controls.ProjectView projectView;
        private Controls.LayerView layerView;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newLayerToolStripMenuItem;
    }
}

