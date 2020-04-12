namespace XPerf
{
    partial class FormGraph
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStrip toolStrip;
            System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
            System.Windows.Forms.ToolStripDropDownButton bData;
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bAddSource = new System.Windows.Forms.ToolStripMenuItem();
            this._dataCollectTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer = new XPerf.Controls.MinimalSplitContainer();
            this.graphPreviewPanel = new XPerf.Controls.StackPanel();
            toolStrip = new System.Windows.Forms.ToolStrip();
            toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            bData = new System.Windows.Forms.ToolStripDropDownButton();
            toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.BackColor = System.Drawing.Color.White;
            toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripDropDownButton1,
            bData});
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(859, 25);
            toolStrip.TabIndex = 2;
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.ShowDropDownArrow = false;
            toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            toolStripDropDownButton1.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem1.Text = "&Exit";
            // 
            // bData
            // 
            bData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bAddSource});
            bData.Name = "bData";
            bData.ShowDropDownArrow = false;
            bData.Size = new System.Drawing.Size(35, 22);
            bData.Text = "&Data";
            // 
            // bAddSource
            // 
            this.bAddSource.Name = "bAddSource";
            this.bAddSource.Size = new System.Drawing.Size(180, 22);
            this.bAddSource.Text = "&Add Source";
            // 
            // _dataCollectTimer
            // 
            this._dataCollectTimer.Enabled = true;
            this._dataCollectTimer.Interval = 1000;
            this._dataCollectTimer.Tick += new System.EventHandler(this.CollectData);
            // 
            // splitContainer
            // 
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Name = "splitContainer";
            // 
            // 
            // 
            this.splitContainer.Panel1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Panel1.Name = "";
            this.splitContainer.Panel1.Size = new System.Drawing.Size(211, 736);
            this.splitContainer.Panel1.TabIndex = 1;
            // 
            // 
            // 
            this.splitContainer.Panel2.Location = new System.Drawing.Point(215, 0);
            this.splitContainer.Panel2.Name = "";
            this.splitContainer.Panel2.Size = new System.Drawing.Size(646, 736);
            this.splitContainer.Panel2.TabIndex = 0;
            this.splitContainer.Size = new System.Drawing.Size(859, 736);
            this.splitContainer.SplitterDistance = 212;
            this.splitContainer.TabIndex = 1;
            // 
            // graphPreviewPanel
            // 
            this.graphPreviewPanel.AutoScroll = true;
            this.graphPreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphPreviewPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.graphPreviewPanel.Location = new System.Drawing.Point(0, 0);
            this.graphPreviewPanel.Name = "graphPreviewPanel";
            this.graphPreviewPanel.Size = new System.Drawing.Size(209, 426);
            this.graphPreviewPanel.TabIndex = 0;
            this.graphPreviewPanel.WrapContents = false;
            // 
            // FormGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(859, 761);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(toolStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormGraph";
            this.Text = "FormGraph";
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer _dataCollectTimer;
        private Controls.StackPanel graphPreviewPanel;
        private XPerf.Controls.MinimalSplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem bAddSource;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}