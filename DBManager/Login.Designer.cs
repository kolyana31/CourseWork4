namespace CourseWork2
{
    partial class Login
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBaseList = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.isLocal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please choose your database";
            // 
            // ComboBaseList
            // 
            this.ComboBaseList.FormattingEnabled = true;
            this.ComboBaseList.Items.AddRange(new object[] {
            "MySQL",
            "MsSQL",
            "PostgresSQL",
            "Access"});
            this.ComboBaseList.Location = new System.Drawing.Point(15, 35);
            this.ComboBaseList.Name = "ComboBaseList";
            this.ComboBaseList.Size = new System.Drawing.Size(144, 21);
            this.ComboBaseList.TabIndex = 1;
            this.ComboBaseList.SelectedIndexChanged += new System.EventHandler(this.ComboBaseList_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // isLocal
            // 
            this.isLocal.AutoSize = true;
            this.isLocal.Location = new System.Drawing.Point(165, 37);
            this.isLocal.Name = "isLocal";
            this.isLocal.Size = new System.Drawing.Size(58, 17);
            this.isLocal.TabIndex = 3;
            this.isLocal.Text = "Local?";
            this.isLocal.UseVisualStyleBackColor = true;
            this.isLocal.Visible = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 93);
            this.Controls.Add(this.isLocal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ComboBaseList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboBaseList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox isLocal;
    }
}

