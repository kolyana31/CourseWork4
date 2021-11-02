namespace CourseWork2
{
    partial class DataBaseShower
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDatabasrListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TableList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ClearEditValue = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.HelperGrid = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.InsertMode = new System.Windows.Forms.CheckBox();
            this.EditMode = new System.Windows.Forms.CheckBox();
            this.DELETEALL = new System.Windows.Forms.Button();
            this.DeleteInfo = new System.Windows.Forms.Button();
            this.DataViewer = new System.Windows.Forms.DataGridView();
            this.openPictureDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HelperGrid)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.utillsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(931, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDatabasrListToolStripMenuItem});
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // refreshDatabasrListToolStripMenuItem
            // 
            this.refreshDatabasrListToolStripMenuItem.Name = "refreshDatabasrListToolStripMenuItem";
            this.refreshDatabasrListToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.refreshDatabasrListToolStripMenuItem.Text = "Refresh database list";
            this.refreshDatabasrListToolStripMenuItem.Click += new System.EventHandler(this.refreshDatabasrListToolStripMenuItem_Click);
            // 
            // utillsToolStripMenuItem
            // 
            this.utillsToolStripMenuItem.Name = "utillsToolStripMenuItem";
            this.utillsToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.utillsToolStripMenuItem.Text = "Structure Changer";
            this.utillsToolStripMenuItem.Click += new System.EventHandler(this.utillsToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TableList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 397);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tables list";
            // 
            // TableList
            // 
            this.TableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableList.FormattingEnabled = true;
            this.TableList.Location = new System.Drawing.Point(3, 16);
            this.TableList.Name = "TableList";
            this.TableList.Size = new System.Drawing.Size(167, 378);
            this.TableList.TabIndex = 0;
            this.TableList.Click += new System.EventHandler(this.TableList_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.HelperGrid);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.DataViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(173, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 397);
            this.panel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ClearEditValue);
            this.panel3.Controls.Add(this.EditButton);
            this.panel3.Controls.Add(this.UpdateButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 367);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(758, 30);
            this.panel3.TabIndex = 3;
            // 
            // ClearEditValue
            // 
            this.ClearEditValue.Dock = System.Windows.Forms.DockStyle.Left;
            this.ClearEditValue.Location = new System.Drawing.Point(550, 0);
            this.ClearEditValue.Name = "ClearEditValue";
            this.ClearEditValue.Size = new System.Drawing.Size(208, 30);
            this.ClearEditValue.TabIndex = 2;
            this.ClearEditValue.Text = "Clear";
            this.ClearEditValue.UseVisualStyleBackColor = true;
            this.ClearEditValue.Click += new System.EventHandler(this.ClearEditValue_Click);
            // 
            // EditButton
            // 
            this.EditButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.EditButton.Location = new System.Drawing.Point(291, 0);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(259, 30);
            this.EditButton.TabIndex = 1;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.UpdateButton.Location = new System.Drawing.Point(0, 0);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(291, 30);
            this.UpdateButton.TabIndex = 0;
            this.UpdateButton.Text = "UPDATE/INSERT";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // HelperGrid
            // 
            this.HelperGrid.AllowUserToAddRows = false;
            this.HelperGrid.AllowUserToDeleteRows = false;
            this.HelperGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HelperGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HelperGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.HelperGrid.Location = new System.Drawing.Point(0, 285);
            this.HelperGrid.MaximumSize = new System.Drawing.Size(0, 250);
            this.HelperGrid.MultiSelect = false;
            this.HelperGrid.Name = "HelperGrid";
            this.HelperGrid.Size = new System.Drawing.Size(758, 82);
            this.HelperGrid.TabIndex = 2;
            this.HelperGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HelperGrid_CellDoubleClick);
            this.HelperGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.HelperGrid_DataError);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.InsertMode);
            this.panel2.Controls.Add(this.EditMode);
            this.panel2.Controls.Add(this.DELETEALL);
            this.panel2.Controls.Add(this.DeleteInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 250);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(758, 35);
            this.panel2.TabIndex = 1;
            // 
            // InsertMode
            // 
            this.InsertMode.AutoSize = true;
            this.InsertMode.Location = new System.Drawing.Point(180, 10);
            this.InsertMode.Name = "InsertMode";
            this.InsertMode.Size = new System.Drawing.Size(98, 17);
            this.InsertMode.TabIndex = 5;
            this.InsertMode.Text = "INSERTMODE";
            this.InsertMode.UseVisualStyleBackColor = true;
            this.InsertMode.CheckedChanged += new System.EventHandler(this.InsertMode_CheckedChanged);
            // 
            // EditMode
            // 
            this.EditMode.AutoSize = true;
            this.EditMode.Location = new System.Drawing.Point(284, 10);
            this.EditMode.Name = "EditMode";
            this.EditMode.Size = new System.Drawing.Size(83, 17);
            this.EditMode.TabIndex = 4;
            this.EditMode.Text = "EDITMODE";
            this.EditMode.UseVisualStyleBackColor = true;
            this.EditMode.CheckedChanged += new System.EventHandler(this.EditMode_CheckedChanged);
            // 
            // DELETEALL
            // 
            this.DELETEALL.Location = new System.Drawing.Point(463, 6);
            this.DELETEALL.Name = "DELETEALL";
            this.DELETEALL.Size = new System.Drawing.Size(96, 23);
            this.DELETEALL.TabIndex = 3;
            this.DELETEALL.Text = "DELETE ALL";
            this.DELETEALL.UseVisualStyleBackColor = true;
            this.DELETEALL.Click += new System.EventHandler(this.DELETEALL_Click);
            // 
            // DeleteInfo
            // 
            this.DeleteInfo.Location = new System.Drawing.Point(382, 6);
            this.DeleteInfo.Name = "DeleteInfo";
            this.DeleteInfo.Size = new System.Drawing.Size(75, 23);
            this.DeleteInfo.TabIndex = 2;
            this.DeleteInfo.Text = "DELETE";
            this.DeleteInfo.UseVisualStyleBackColor = true;
            this.DeleteInfo.Click += new System.EventHandler(this.DeleteInfo_Click);
            // 
            // DataViewer
            // 
            this.DataViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataViewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataViewer.Location = new System.Drawing.Point(0, 0);
            this.DataViewer.MaximumSize = new System.Drawing.Size(0, 250);
            this.DataViewer.MultiSelect = false;
            this.DataViewer.Name = "DataViewer";
            this.DataViewer.ReadOnly = true;
            this.DataViewer.Size = new System.Drawing.Size(758, 250);
            this.DataViewer.TabIndex = 0;
            this.DataViewer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataViewer_CellClick);
            this.DataViewer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataViewer_CellDoubleClick);
            // 
            // openPictureDialog
            // 
            this.openPictureDialog.FileName = "openFileDialog1";
            this.openPictureDialog.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            // 
            // DataBaseShower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 421);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataBaseShower";
            this.Text = "DataBaseShower";
            this.Shown += new System.EventHandler(this.refreshDatabasrListToolStripMenuItem_Click);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HelperGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDatabasrListToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox TableList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DataViewer;
        private System.Windows.Forms.ToolStripMenuItem utillsToolStripMenuItem;
        private System.Windows.Forms.DataGridView HelperGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button DeleteInfo;
        private System.Windows.Forms.Button DELETEALL;
        private System.Windows.Forms.CheckBox EditMode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.OpenFileDialog openPictureDialog;
        private System.Windows.Forms.Button ClearEditValue;
        private System.Windows.Forms.CheckBox InsertMode;
    }
}