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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDatabasrListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLCreaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TableList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.HelperGrid = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.EditMode = new System.Windows.Forms.CheckBox();
            this.DELETEALL = new System.Windows.Forms.Button();
            this.UpdateInfo = new System.Windows.Forms.Button();
            this.DeleteInfo = new System.Windows.Forms.Button();
            this.DataViewer = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HelperGrid)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.utillsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(939, 24);
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
            this.utillsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLCreaterToolStripMenuItem});
            this.utillsToolStripMenuItem.Name = "utillsToolStripMenuItem";
            this.utillsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.utillsToolStripMenuItem.Text = "Utills";
            // 
            // sQLCreaterToolStripMenuItem
            // 
            this.sQLCreaterToolStripMenuItem.Name = "sQLCreaterToolStripMenuItem";
            this.sQLCreaterToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.sQLCreaterToolStripMenuItem.Text = "SQL Creater";
            this.sQLCreaterToolStripMenuItem.Click += new System.EventHandler(this.sQLCreaterToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TableList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 428);
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
            this.TableList.Size = new System.Drawing.Size(167, 409);
            this.TableList.TabIndex = 0;
            this.TableList.Click += new System.EventHandler(this.TableList_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.HelperGrid);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.DataViewer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(173, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 428);
            this.panel1.TabIndex = 4;
            // 
            // HelperGrid
            // 
            this.HelperGrid.AllowUserToAddRows = false;
            this.HelperGrid.AllowUserToDeleteRows = false;
            this.HelperGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HelperGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HelperGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HelperGrid.Location = new System.Drawing.Point(0, 285);
            this.HelperGrid.MaximumSize = new System.Drawing.Size(0, 250);
            this.HelperGrid.MultiSelect = false;
            this.HelperGrid.Name = "HelperGrid";
            this.HelperGrid.ReadOnly = true;
            this.HelperGrid.Size = new System.Drawing.Size(766, 143);
            this.HelperGrid.TabIndex = 2;
            this.HelperGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HelperGrid_CellClick);
            this.HelperGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HelperGrid_CellDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.EditMode);
            this.panel2.Controls.Add(this.DELETEALL);
            this.panel2.Controls.Add(this.UpdateInfo);
            this.panel2.Controls.Add(this.DeleteInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 250);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 35);
            this.panel2.TabIndex = 1;
            // 
            // EditMode
            // 
            this.EditMode.AutoSize = true;
            this.EditMode.Location = new System.Drawing.Point(191, 10);
            this.EditMode.Name = "EditMode";
            this.EditMode.Size = new System.Drawing.Size(83, 17);
            this.EditMode.TabIndex = 4;
            this.EditMode.Text = "EDITMODE";
            this.EditMode.UseVisualStyleBackColor = true;
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
            // UpdateInfo
            // 
            this.UpdateInfo.Location = new System.Drawing.Point(280, 6);
            this.UpdateInfo.Name = "UpdateInfo";
            this.UpdateInfo.Size = new System.Drawing.Size(96, 23);
            this.UpdateInfo.TabIndex = 1;
            this.UpdateInfo.Text = "UPDATE";
            this.UpdateInfo.UseVisualStyleBackColor = true;
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
            this.DataViewer.Size = new System.Drawing.Size(766, 250);
            this.DataViewer.TabIndex = 0;
            this.DataViewer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataViewer_CellClick);
            this.DataViewer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataViewer_CellDoubleClick);
            // 
            // DataBaseShower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 452);
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
            ((System.ComponentModel.ISupportInitialize)(this.HelperGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDatabasrListToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox TableList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DataViewer;
        private System.Windows.Forms.ToolStripMenuItem utillsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLCreaterToolStripMenuItem;
        private System.Windows.Forms.DataGridView HelperGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button DeleteInfo;
        private System.Windows.Forms.Button UpdateInfo;
        private System.Windows.Forms.Button DELETEALL;
        private System.Windows.Forms.CheckBox EditMode;
    }
}