namespace CourseWork2
{
    partial class ConnectionForm
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
            this.PanelFileFormat = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.FilePathString = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.PanelLocalFormat = new System.Windows.Forms.Panel();
            this.LocalBD = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LocalTestDB = new System.Windows.Forms.Button();
            this.LocalDB = new System.Windows.Forms.Button();
            this.ServerAdress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Database = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Submit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Test = new System.Windows.Forms.Button();
            this.ServerPort = new System.Windows.Forms.TextBox();
            this.PanelPFormat = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.PanelFileFormat.SuspendLayout();
            this.PanelLocalFormat.SuspendLayout();
            this.PanelPFormat.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelFileFormat
            // 
            this.PanelFileFormat.Controls.Add(this.button3);
            this.PanelFileFormat.Controls.Add(this.FilePathString);
            this.PanelFileFormat.Controls.Add(this.button1);
            this.PanelFileFormat.Controls.Add(this.button2);
            this.PanelFileFormat.Location = new System.Drawing.Point(257, 12);
            this.PanelFileFormat.Name = "PanelFileFormat";
            this.PanelFileFormat.Size = new System.Drawing.Size(196, 108);
            this.PanelFileFormat.TabIndex = 13;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 36);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(178, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "ChooseFile";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FilePathString
            // 
            this.FilePathString.Location = new System.Drawing.Point(9, 11);
            this.FilePathString.Name = "FilePathString";
            this.FilePathString.Size = new System.Drawing.Size(178, 20);
            this.FilePathString.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 11;
            this.button1.Tag = "30";
            this.button1.Text = "Test Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 10;
            this.button2.Tag = "20";
            this.button2.Text = "Submit File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // PanelLocalFormat
            // 
            this.PanelLocalFormat.Controls.Add(this.LocalBD);
            this.PanelLocalFormat.Controls.Add(this.label6);
            this.PanelLocalFormat.Controls.Add(this.LocalTestDB);
            this.PanelLocalFormat.Controls.Add(this.LocalDB);
            this.PanelLocalFormat.Location = new System.Drawing.Point(12, 268);
            this.PanelLocalFormat.Name = "PanelLocalFormat";
            this.PanelLocalFormat.Size = new System.Drawing.Size(227, 103);
            this.PanelLocalFormat.TabIndex = 14;
            // 
            // LocalBD
            // 
            this.LocalBD.Location = new System.Drawing.Point(28, 33);
            this.LocalBD.Name = "LocalBD";
            this.LocalBD.Size = new System.Drawing.Size(178, 20);
            this.LocalBD.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Database";
            // 
            // LocalTestDB
            // 
            this.LocalTestDB.Location = new System.Drawing.Point(120, 59);
            this.LocalTestDB.Name = "LocalTestDB";
            this.LocalTestDB.Size = new System.Drawing.Size(86, 23);
            this.LocalTestDB.TabIndex = 11;
            this.LocalTestDB.Tag = "30";
            this.LocalTestDB.Text = "Test Connect";
            this.LocalTestDB.UseVisualStyleBackColor = true;
            this.LocalTestDB.Click += new System.EventHandler(this.button1_Click);
            // 
            // LocalDB
            // 
            this.LocalDB.Location = new System.Drawing.Point(28, 59);
            this.LocalDB.Name = "LocalDB";
            this.LocalDB.Size = new System.Drawing.Size(86, 23);
            this.LocalDB.TabIndex = 10;
            this.LocalDB.Tag = "20";
            this.LocalDB.Text = "Submit";
            this.LocalDB.UseVisualStyleBackColor = true;
            this.LocalDB.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServerAdress
            // 
            this.ServerAdress.Location = new System.Drawing.Point(28, 25);
            this.ServerAdress.Name = "ServerAdress";
            this.ServerAdress.Size = new System.Drawing.Size(117, 20);
            this.ServerAdress.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Database";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(28, 76);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(178, 20);
            this.UserName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(28, 126);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(178, 20);
            this.Password.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User";
            // 
            // Database
            // 
            this.Database.Location = new System.Drawing.Point(28, 170);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(178, 20);
            this.Database.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(28, 196);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(86, 23);
            this.Submit.TabIndex = 10;
            this.Submit.Tag = "20";
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(120, 196);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(86, 23);
            this.Test.TabIndex = 11;
            this.Test.Tag = "30";
            this.Test.Text = "Test Connect";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(151, 25);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(55, 20);
            this.ServerPort.TabIndex = 6;
            // 
            // PanelPFormat
            // 
            this.PanelPFormat.Controls.Add(this.ServerPort);
            this.PanelPFormat.Controls.Add(this.Test);
            this.PanelPFormat.Controls.Add(this.label1);
            this.PanelPFormat.Controls.Add(this.Submit);
            this.PanelPFormat.Controls.Add(this.label2);
            this.PanelPFormat.Controls.Add(this.Database);
            this.PanelPFormat.Controls.Add(this.label3);
            this.PanelPFormat.Controls.Add(this.Password);
            this.PanelPFormat.Controls.Add(this.label4);
            this.PanelPFormat.Controls.Add(this.UserName);
            this.PanelPFormat.Controls.Add(this.label5);
            this.PanelPFormat.Controls.Add(this.ServerAdress);
            this.PanelPFormat.Location = new System.Drawing.Point(12, 12);
            this.PanelPFormat.Name = "PanelPFormat";
            this.PanelPFormat.Size = new System.Drawing.Size(227, 233);
            this.PanelPFormat.TabIndex = 12;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(462, 383);
            this.Controls.Add(this.PanelLocalFormat);
            this.Controls.Add(this.PanelFileFormat);
            this.Controls.Add(this.PanelPFormat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConnectionSettings";
            this.PanelFileFormat.ResumeLayout(false);
            this.PanelFileFormat.PerformLayout();
            this.PanelLocalFormat.ResumeLayout(false);
            this.PanelLocalFormat.PerformLayout();
            this.PanelPFormat.ResumeLayout(false);
            this.PanelPFormat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PanelFileFormat;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel PanelLocalFormat;
        private System.Windows.Forms.TextBox LocalBD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button LocalTestDB;
        private System.Windows.Forms.Button LocalDB;
        private System.Windows.Forms.TextBox ServerAdress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Database;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.TextBox ServerPort;
        private System.Windows.Forms.Panel PanelPFormat;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox FilePathString;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}