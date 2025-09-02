namespace Puzzles
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGA = new System.Windows.Forms.Button();
            this.btnKR = new System.Windows.Forms.Button();
            this.btnS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MintCream;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(152, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(825, 82);
            this.label1.TabIndex = 4;
            this.label1.Text = "Розвиваючі ігри «Головоломки»";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(263, 546);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 35);
            this.label2.TabIndex = 5;
            this.label2.Text = "Судоку";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(570, 549);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Знайди пару";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(825, 549);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(335, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Головоломки з арифметики";
            // 
            // btnGA
            // 
            this.btnGA.BackgroundImage = global::Puzzles.Properties.Resources.image_2025_04_29_10_32_16;
            this.btnGA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGA.Location = new System.Drawing.Point(816, 273);
            this.btnGA.Name = "btnGA";
            this.btnGA.Size = new System.Drawing.Size(289, 267);
            this.btnGA.TabIndex = 3;
            this.btnGA.UseVisualStyleBackColor = true;
            this.btnGA.Click += new System.EventHandler(this.btnGA_Click);
            // 
            // btnKR
            // 
            this.btnKR.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnKR.BackgroundImage")));
            this.btnKR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKR.Location = new System.Drawing.Point(411, 273);
            this.btnKR.Name = "btnKR";
            this.btnKR.Size = new System.Drawing.Size(315, 267);
            this.btnKR.TabIndex = 2;
            this.btnKR.UseVisualStyleBackColor = true;
            this.btnKR.Click += new System.EventHandler(this.btnKR_Click);
            // 
            // btnS
            // 
            this.btnS.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnS.BackgroundImage")));
            this.btnS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnS.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnS.Location = new System.Drawing.Point(46, 273);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(303, 267);
            this.btnS.TabIndex = 1;
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.btnS_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1186, 609);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGA);
            this.Controls.Add(this.btnKR);
            this.Controls.Add(this.btnS);
            this.Name = "FormMain";
            this.Text = "Розвиваючі ігри «Головоломки»";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnKR;
        private System.Windows.Forms.Label label4;
    }
}

