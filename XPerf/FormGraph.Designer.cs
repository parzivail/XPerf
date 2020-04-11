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
            System.Windows.Forms.ToolStripDropDownButton bFile;
            this.bExit = new System.Windows.Forms.ToolStripMenuItem();
            this._dataCollectTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer = new XPerf.Controls.MinimalSplitContainer();
            this.graphPreviewPanel = new XPerf.Controls.StackPanel();
            toolStrip = new System.Windows.Forms.ToolStrip();
            bFile = new System.Windows.Forms.ToolStripDropDownButton();
            toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.BackColor = System.Drawing.Color.White;
            toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            bFile});
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(859, 25);
            toolStrip.TabIndex = 2;
            // 
            // bFile
            // 
            bFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bExit});
            bFile.Name = "bFile";
            bFile.ShowDropDownArrow = false;
            bFile.Size = new System.Drawing.Size(29, 22);
            bFile.Text = "&File";
            // 
            // bExit
            // 
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(180, 22);
            this.bExit.Text = "&Exit";
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
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
        private System.Windows.Forms.ToolStripMenuItem bExit;
    }
}