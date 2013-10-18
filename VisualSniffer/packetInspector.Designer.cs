namespace VisualSniffer
{
    partial class packetInspector
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
            this.packViewer = new System.Windows.Forms.TreeView();
            this.HexView = new HexViewer.HexBox();
            this.SuspendLayout();
            // 
            // packViewer
            // 
            this.packViewer.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packViewer.Location = new System.Drawing.Point(12, 12);
            this.packViewer.Name = "packViewer";
            this.packViewer.Size = new System.Drawing.Size(640, 385);
            this.packViewer.TabIndex = 0;
            this.packViewer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.packViewer_AfterSelect);
            // 
            // HexView
            // 
            this.HexView.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HexView.GroupSeparatorVisible = true;
            this.HexView.InfoForeColor = System.Drawing.Color.Empty;
            this.HexView.LineInfoVisible = true;
            this.HexView.Location = new System.Drawing.Point(658, 12);
            this.HexView.Name = "HexView";
            this.HexView.ReadOnly = true;
            this.HexView.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.HexView.Size = new System.Drawing.Size(662, 386);
            this.HexView.StringViewVisible = true;
            this.HexView.TabIndex = 1;
            this.HexView.UseFixedBytesPerLine = true;
            this.HexView.VScrollBarVisible = true;
            // 
            // packetInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 410);
            this.Controls.Add(this.HexView);
            this.Controls.Add(this.packViewer);
            this.Name = "packetInspector";
            this.Text = "packetInspector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView packViewer;
        private HexViewer.HexBox HexView;
    }
}