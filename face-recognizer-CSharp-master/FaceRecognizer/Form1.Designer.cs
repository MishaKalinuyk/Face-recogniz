namespace FaceRecognizer
{
    partial class Form1
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
            this.Analyze = new System.Windows.Forms.Button();
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.LoadPhoto = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.isAsync = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.InfoLable = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Analyze
            // 
            this.Analyze.Location = new System.Drawing.Point(129, 3);
            this.Analyze.Name = "Analyze";
            this.Analyze.Size = new System.Drawing.Size(108, 43);
            this.Analyze.TabIndex = 0;
            this.Analyze.Text = "Аналізувати";
            this.Analyze.UseVisualStyleBackColor = true;
            this.Analyze.Visible = false;
            this.Analyze.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImageBox
            // 
            this.ImageBox.Location = new System.Drawing.Point(3, 3);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(90, 91);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImageBox.TabIndex = 1;
            this.ImageBox.TabStop = false;
            this.ImageBox.Click += new System.EventHandler(this.Image_Click);
            // 
            // LoadPhoto
            // 
            this.LoadPhoto.Location = new System.Drawing.Point(0, 3);
            this.LoadPhoto.Name = "LoadPhoto";
            this.LoadPhoto.Size = new System.Drawing.Size(108, 43);
            this.LoadPhoto.TabIndex = 2;
            this.LoadPhoto.Text = "Завантажити фото";
            this.LoadPhoto.UseVisualStyleBackColor = true;
            this.LoadPhoto.Click += new System.EventHandler(this.LoadPhoto_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.isAsync);
            this.panel1.Controls.Add(this.Analyze);
            this.panel1.Controls.Add(this.LoadPhoto);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 52);
            this.panel1.TabIndex = 3;
            // 
            // isAsync
            // 
            this.isAsync.AutoSize = true;
            this.isAsync.Checked = true;
            this.isAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isAsync.Location = new System.Drawing.Point(267, 12);
            this.isAsync.Name = "isAsync";
            this.isAsync.Size = new System.Drawing.Size(108, 21);
            this.isAsync.TabIndex = 3;
            this.isAsync.Text = "Асинхронно";
            this.isAsync.UseVisualStyleBackColor = true;
            this.isAsync.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.InfoLable);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 160);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(480, 37);
            this.panel3.TabIndex = 5;
            // 
            // InfoLable
            // 
            this.InfoLable.AutoSize = true;
            this.InfoLable.Location = new System.Drawing.Point(12, 11);
            this.InfoLable.Name = "InfoLable";
            this.InfoLable.Size = new System.Drawing.Size(0, 17);
            this.InfoLable.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.ImageBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 108);
            this.panel2.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(480, 197);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Face Recognizer";
            this.Text = "Face Recognizer";
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Analyze;
        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Button LoadPhoto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label InfoLable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox isAsync;
    }
}

