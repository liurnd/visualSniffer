namespace VisualSniffer
{
    partial class snifferOptions
    {
        public static snifferOptions Instance = new snifferOptions();
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
            this.button1 = new System.Windows.Forms.Button();
            this.devList = new System.Windows.Forms.CheckedListBox();
            this.filterString = new System.Windows.Forms.ComboBox();
            this.devTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(318, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // devList
            // 
            this.devList.FormattingEnabled = true;
            this.devList.Location = new System.Drawing.Point(12, 12);
            this.devList.Name = "devList";
            this.devList.Size = new System.Drawing.Size(381, 154);
            this.devList.TabIndex = 2;
            this.devList.SelectedIndexChanged += new System.EventHandler(this.devList_SelectedIndexChanged);
            // 
            // filterString
            // 
            this.filterString.AutoCompleteCustomSource.AddRange(new string[] {
            "tcp",
            "port ",
            "icmp",
            "arp",
            "ip6"});
            this.filterString.FormattingEnabled = true;
            this.filterString.Items.AddRange(new object[] {
            "arp",
            "tcp",
            "tcp port 80"});
            this.filterString.Location = new System.Drawing.Point(13, 173);
            this.filterString.Name = "filterString";
            this.filterString.Size = new System.Drawing.Size(299, 21);
            this.filterString.TabIndex = 3;
            // 
            // snifferOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 205);
            this.Controls.Add(this.filterString);
            this.Controls.Add(this.devList);
            this.Controls.Add(this.button1);
            this.Name = "snifferOptions";
            this.Text = "snifferOptions";
            this.Load += new System.EventHandler(this.snifferOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox devList;
        private System.Windows.Forms.ComboBox filterString;
        private System.Windows.Forms.ToolTip devTip;
    }
}