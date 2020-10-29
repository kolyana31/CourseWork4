namespace CourseWork2
{
    partial class SQLCreator
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
            this.button1 = new System.Windows.Forms.Button();
            this.CommandField = new System.Windows.Forms.RichTextBox();
            this.CustomViewGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.CustomViewGrid)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1108, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "ExecuteCommand";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CommandField
            // 
            this.CommandField.Dock = System.Windows.Forms.DockStyle.Left;
            this.CommandField.Location = new System.Drawing.Point(0, 24);
            this.CommandField.Name = "CommandField";
            this.CommandField.Size = new System.Drawing.Size(511, 268);
            this.CommandField.TabIndex = 1;
            this.CommandField.Text = "";
            // 
            // CustomViewGrid
            // 
            this.CustomViewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomViewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomViewGrid.Location = new System.Drawing.Point(511, 24);
            this.CustomViewGrid.Name = "CustomViewGrid";
            this.CustomViewGrid.Size = new System.Drawing.Size(597, 268);
            this.CustomViewGrid.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1108, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // SQLCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 328);
            this.Controls.Add(this.CustomViewGrid);
            this.Controls.Add(this.CommandField);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SQLCreator";
            this.Text = "SQLCreator";
            ((System.ComponentModel.ISupportInitialize)(this.CustomViewGrid)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox CommandField;
        private System.Windows.Forms.DataGridView CustomViewGrid;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}