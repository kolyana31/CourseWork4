namespace CourseWork2
{
    partial class ForeignKeyAddeer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cancel = new System.Windows.Forms.Button();
            this.FKName = new System.Windows.Forms.Label();
            this.RefColumn = new System.Windows.Forms.Label();
            this.RefTable = new System.Windows.Forms.Label();
            this.TableColumn = new System.Windows.Forms.Label();
            this.OwTable = new System.Windows.Forms.Label();
            this.Confirm = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ReferColumnCombo = new System.Windows.Forms.ComboBox();
            this.ReferCombo = new System.Windows.Forms.ComboBox();
            this.OwningColumnCombo = new System.Windows.Forms.ComboBox();
            this.OwningCombo = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Cancel);
            this.panel1.Controls.Add(this.FKName);
            this.panel1.Controls.Add(this.RefColumn);
            this.panel1.Controls.Add(this.RefTable);
            this.panel1.Controls.Add(this.TableColumn);
            this.panel1.Controls.Add(this.OwTable);
            this.panel1.Controls.Add(this.Confirm);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.ReferColumnCombo);
            this.panel1.Controls.Add(this.ReferCombo);
            this.panel1.Controls.Add(this.OwningColumnCombo);
            this.panel1.Controls.Add(this.OwningCombo);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 75);
            this.panel1.TabIndex = 0;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(266, 49);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(253, 23);
            this.Cancel.TabIndex = 11;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // FKName
            // 
            this.FKName.AutoSize = true;
            this.FKName.Location = new System.Drawing.Point(419, 9);
            this.FKName.Name = "FKName";
            this.FKName.Size = new System.Drawing.Size(85, 13);
            this.FKName.TabIndex = 10;
            this.FKName.Text = "Constraint Name";
            // 
            // RefColumn
            // 
            this.RefColumn.AutoSize = true;
            this.RefColumn.Location = new System.Drawing.Point(316, 9);
            this.RefColumn.Name = "RefColumn";
            this.RefColumn.Size = new System.Drawing.Size(71, 13);
            this.RefColumn.TabIndex = 9;
            this.RefColumn.Text = "Refer Column";
            // 
            // RefTable
            // 
            this.RefTable.AutoSize = true;
            this.RefTable.Location = new System.Drawing.Point(213, 9);
            this.RefTable.Name = "RefTable";
            this.RefTable.Size = new System.Drawing.Size(63, 13);
            this.RefTable.TabIndex = 8;
            this.RefTable.Text = "Refer Table";
            // 
            // TableColumn
            // 
            this.TableColumn.AutoSize = true;
            this.TableColumn.Location = new System.Drawing.Point(110, 9);
            this.TableColumn.Name = "TableColumn";
            this.TableColumn.Size = new System.Drawing.Size(72, 13);
            this.TableColumn.TabIndex = 7;
            this.TableColumn.Text = "Table Column";
            // 
            // OwTable
            // 
            this.OwTable.AutoSize = true;
            this.OwTable.Location = new System.Drawing.Point(10, 9);
            this.OwTable.Name = "OwTable";
            this.OwTable.Size = new System.Drawing.Size(73, 13);
            this.OwTable.TabIndex = 6;
            this.OwTable.Text = "Owning Table";
            // 
            // Confirm
            // 
            this.Confirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Confirm.Location = new System.Drawing.Point(10, 49);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(253, 23);
            this.Confirm.TabIndex = 5;
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(422, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(97, 20);
            this.textBox1.TabIndex = 4;
            // 
            // ReferColumnCombo
            // 
            this.ReferColumnCombo.FormattingEnabled = true;
            this.ReferColumnCombo.Location = new System.Drawing.Point(319, 25);
            this.ReferColumnCombo.Name = "ReferColumnCombo";
            this.ReferColumnCombo.Size = new System.Drawing.Size(97, 21);
            this.ReferColumnCombo.TabIndex = 3;
            // 
            // ReferCombo
            // 
            this.ReferCombo.FormattingEnabled = true;
            this.ReferCombo.Location = new System.Drawing.Point(216, 25);
            this.ReferCombo.Name = "ReferCombo";
            this.ReferCombo.Size = new System.Drawing.Size(97, 21);
            this.ReferCombo.TabIndex = 2;
            this.ReferCombo.SelectedIndexChanged += new System.EventHandler(this.ReferCombo_SelectedIndexChanged);
            // 
            // OwningColumnCombo
            // 
            this.OwningColumnCombo.FormattingEnabled = true;
            this.OwningColumnCombo.Location = new System.Drawing.Point(113, 25);
            this.OwningColumnCombo.Name = "OwningColumnCombo";
            this.OwningColumnCombo.Size = new System.Drawing.Size(97, 21);
            this.OwningColumnCombo.TabIndex = 1;
            // 
            // OwningCombo
            // 
            this.OwningCombo.FormattingEnabled = true;
            this.OwningCombo.Location = new System.Drawing.Point(10, 25);
            this.OwningCombo.Name = "OwningCombo";
            this.OwningCombo.Size = new System.Drawing.Size(97, 21);
            this.OwningCombo.TabIndex = 0;
            this.OwningCombo.SelectedIndexChanged += new System.EventHandler(this.OwningCombo_SelectedIndexChanged);
            // 
            // ForeignKeyAddeer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(533, 85);
            this.Controls.Add(this.panel1);
            this.Name = "ForeignKeyAddeer";
            this.Text = "ForeignKeyAddeer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FKName;
        private System.Windows.Forms.Label RefColumn;
        private System.Windows.Forms.Label RefTable;
        private System.Windows.Forms.Label TableColumn;
        private System.Windows.Forms.Label OwTable;
        private System.Windows.Forms.Button Confirm;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ComboBox ReferColumnCombo;
        public System.Windows.Forms.ComboBox ReferCombo;
        public System.Windows.Forms.ComboBox OwningColumnCombo;
        public System.Windows.Forms.ComboBox OwningCombo;
        private System.Windows.Forms.Button Cancel;
    }
}