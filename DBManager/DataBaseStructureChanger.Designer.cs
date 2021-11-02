namespace CourseWork2
{
    partial class DataBaseStructureChanger
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
            this.TableList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Confirm = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ForeignKeyMode = new System.Windows.Forms.CheckBox();
            this.AddFK = new System.Windows.Forms.Button();
            this.DeleteFK = new System.Windows.Forms.Button();
            this.EditType = new System.Windows.Forms.Button();
            this.EditName = new System.Windows.Forms.Button();
            this.AddIndex = new System.Windows.Forms.Button();
            this.AddRow = new System.Windows.Forms.CheckBox();
            this.DropIndex = new System.Windows.Forms.Button();
            this.DeleteColumn = new System.Windows.Forms.Button();
            this.DeleteTable = new System.Windows.Forms.Button();
            this.AddTable = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableList
            // 
            this.TableList.Dock = System.Windows.Forms.DockStyle.Left;
            this.TableList.FormattingEnabled = true;
            this.TableList.Location = new System.Drawing.Point(0, 24);
            this.TableList.Name = "TableList";
            this.TableList.Size = new System.Drawing.Size(128, 375);
            this.TableList.TabIndex = 0;
            this.TableList.Click += new System.EventHandler(this.TableList_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Confirm);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(128, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 375);
            this.panel1.TabIndex = 2;
            // 
            // Confirm
            // 
            this.Confirm.Dock = System.Windows.Forms.DockStyle.Top;
            this.Confirm.Location = new System.Drawing.Point(0, 354);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(636, 23);
            this.Confirm.TabIndex = 2;
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(636, 294);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ForeignKeyMode);
            this.panel2.Controls.Add(this.AddFK);
            this.panel2.Controls.Add(this.DeleteFK);
            this.panel2.Controls.Add(this.EditType);
            this.panel2.Controls.Add(this.EditName);
            this.panel2.Controls.Add(this.AddIndex);
            this.panel2.Controls.Add(this.AddRow);
            this.panel2.Controls.Add(this.DropIndex);
            this.panel2.Controls.Add(this.DeleteColumn);
            this.panel2.Controls.Add(this.DeleteTable);
            this.panel2.Controls.Add(this.AddTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(636, 60);
            this.panel2.TabIndex = 0;
            // 
            // ForeignKeyMode
            // 
            this.ForeignKeyMode.AutoSize = true;
            this.ForeignKeyMode.Location = new System.Drawing.Point(428, 8);
            this.ForeignKeyMode.Name = "ForeignKeyMode";
            this.ForeignKeyMode.Size = new System.Drawing.Size(106, 17);
            this.ForeignKeyMode.TabIndex = 14;
            this.ForeignKeyMode.Text = "ForeignKeyMode";
            this.ForeignKeyMode.UseVisualStyleBackColor = true;
            this.ForeignKeyMode.CheckedChanged += new System.EventHandler(this.ForeignKeyMode_CheckedChanged);
            // 
            // AddFK
            // 
            this.AddFK.Location = new System.Drawing.Point(540, 5);
            this.AddFK.Name = "AddFK";
            this.AddFK.Size = new System.Drawing.Size(84, 23);
            this.AddFK.TabIndex = 13;
            this.AddFK.Text = "AddFK";
            this.AddFK.UseVisualStyleBackColor = true;
            this.AddFK.Click += new System.EventHandler(this.AddFK_Click);
            // 
            // DeleteFK
            // 
            this.DeleteFK.Location = new System.Drawing.Point(540, 30);
            this.DeleteFK.Name = "DeleteFK";
            this.DeleteFK.Size = new System.Drawing.Size(84, 23);
            this.DeleteFK.TabIndex = 12;
            this.DeleteFK.Text = "DeleteFK";
            this.DeleteFK.UseVisualStyleBackColor = true;
            this.DeleteFK.Click += new System.EventHandler(this.DeleteFK_Click);
            // 
            // EditType
            // 
            this.EditType.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditType.Location = new System.Drawing.Point(283, 31);
            this.EditType.Name = "EditType";
            this.EditType.Size = new System.Drawing.Size(84, 23);
            this.EditType.TabIndex = 11;
            this.EditType.Text = "Edit Type";
            this.EditType.UseVisualStyleBackColor = true;
            this.EditType.Click += new System.EventHandler(this.EditType_Click);
            // 
            // EditName
            // 
            this.EditName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EditName.Location = new System.Drawing.Point(283, 5);
            this.EditName.Name = "EditName";
            this.EditName.Size = new System.Drawing.Size(84, 23);
            this.EditName.TabIndex = 10;
            this.EditName.Text = "Edit Name";
            this.EditName.UseVisualStyleBackColor = true;
            this.EditName.Click += new System.EventHandler(this.EditName_Click);
            // 
            // AddIndex
            // 
            this.AddIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddIndex.Location = new System.Drawing.Point(193, 5);
            this.AddIndex.Name = "AddIndex";
            this.AddIndex.Size = new System.Drawing.Size(84, 23);
            this.AddIndex.TabIndex = 9;
            this.AddIndex.Text = "Add Index";
            this.AddIndex.UseVisualStyleBackColor = true;
            this.AddIndex.Click += new System.EventHandler(this.AddIndex_Click);
            // 
            // AddRow
            // 
            this.AddRow.AutoSize = true;
            this.AddRow.Location = new System.Drawing.Point(103, 11);
            this.AddRow.Name = "AddRow";
            this.AddRow.Size = new System.Drawing.Size(67, 17);
            this.AddRow.TabIndex = 8;
            this.AddRow.Text = "AddRow";
            this.AddRow.UseVisualStyleBackColor = true;
            this.AddRow.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DropIndex
            // 
            this.DropIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DropIndex.Location = new System.Drawing.Point(193, 31);
            this.DropIndex.Name = "DropIndex";
            this.DropIndex.Size = new System.Drawing.Size(84, 23);
            this.DropIndex.TabIndex = 7;
            this.DropIndex.Text = "Drop Index";
            this.DropIndex.UseVisualStyleBackColor = true;
            this.DropIndex.Click += new System.EventHandler(this.DropIndex_Click);
            // 
            // DeleteColumn
            // 
            this.DeleteColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteColumn.Location = new System.Drawing.Point(103, 31);
            this.DeleteColumn.Name = "DeleteColumn";
            this.DeleteColumn.Size = new System.Drawing.Size(84, 23);
            this.DeleteColumn.TabIndex = 5;
            this.DeleteColumn.Text = "DeleteColumn";
            this.DeleteColumn.UseVisualStyleBackColor = true;
            this.DeleteColumn.Click += new System.EventHandler(this.DeleteColumn_Click);
            // 
            // DeleteTable
            // 
            this.DeleteTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteTable.Location = new System.Drawing.Point(6, 31);
            this.DeleteTable.Name = "DeleteTable";
            this.DeleteTable.Size = new System.Drawing.Size(84, 23);
            this.DeleteTable.TabIndex = 4;
            this.DeleteTable.Text = "DeleteTable";
            this.DeleteTable.UseVisualStyleBackColor = true;
            this.DeleteTable.Click += new System.EventHandler(this.DeleteTable_Click);
            // 
            // AddTable
            // 
            this.AddTable.AutoSize = true;
            this.AddTable.Location = new System.Drawing.Point(6, 11);
            this.AddTable.Name = "AddTable";
            this.AddTable.Size = new System.Drawing.Size(72, 17);
            this.AddTable.TabIndex = 0;
            this.AddTable.Text = "AddTable";
            this.AddTable.UseVisualStyleBackColor = true;
            this.AddTable.CheckedChanged += new System.EventHandler(this.AddTable_CheckedChanged);
            // 
            // DataBaseStructureChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 399);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TableList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataBaseStructureChanger";
            this.Text = "DataBaseStructureChanger";
            this.Shown += new System.EventHandler(this.DataBaseStructureChanger_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox TableList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox AddTable;
        private System.Windows.Forms.Button DeleteTable;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button DeleteColumn;
        private System.Windows.Forms.Button DropIndex;
        private System.Windows.Forms.CheckBox AddRow;
        private System.Windows.Forms.Button AddIndex;
        private System.Windows.Forms.Button EditName;
        private System.Windows.Forms.Button EditType;
        private System.Windows.Forms.CheckBox ForeignKeyMode;
        private System.Windows.Forms.Button AddFK;
        private System.Windows.Forms.Button DeleteFK;
    }
}