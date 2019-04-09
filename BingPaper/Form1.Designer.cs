namespace BingPaper
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pctBoxWall = new System.Windows.Forms.PictureBox();
            this.btnSetWall = new System.Windows.Forms.Button();
            this.rightArrow1 = new BingPaper.ArrowRight();
            this.leftArrow1 = new BingPaper.ArrowLeft();
            this.lblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxWall)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBoxWall
            // 
            this.pctBoxWall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pctBoxWall.Location = new System.Drawing.Point(5, 33);
            this.pctBoxWall.Name = "pctBoxWall";
            this.pctBoxWall.Size = new System.Drawing.Size(960, 540);
            this.pctBoxWall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctBoxWall.TabIndex = 0;
            this.pctBoxWall.TabStop = false;
            // 
            // btnSetWall
            // 
            this.btnSetWall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSetWall.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.btnSetWall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetWall.ForeColor = System.Drawing.Color.Navy;
            this.btnSetWall.Location = new System.Drawing.Point(851, 5);
            this.btnSetWall.Name = "btnSetWall";
            this.btnSetWall.Size = new System.Drawing.Size(113, 23);
            this.btnSetWall.TabIndex = 1;
            this.btnSetWall.Text = "Set Wallpaper";
            this.btnSetWall.UseVisualStyleBackColor = false;
            this.btnSetWall.Click += new System.EventHandler(this.btnSetWall_Click);
            // 
            // rightArrow1
            // 
            this.rightArrow1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rightArrow1.FlatAppearance.BorderSize = 0;
            this.rightArrow1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightArrow1.Location = new System.Drawing.Point(925, 283);
            this.rightArrow1.Name = "rightArrow1";
            this.rightArrow1.Size = new System.Drawing.Size(30, 40);
            this.rightArrow1.TabIndex = 3;
            this.rightArrow1.UseVisualStyleBackColor = false;
            this.rightArrow1.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // leftArrow1
            // 
            this.leftArrow1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.leftArrow1.FlatAppearance.BorderSize = 0;
            this.leftArrow1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftArrow1.Location = new System.Drawing.Point(15, 283);
            this.leftArrow1.Name = "leftArrow1";
            this.leftArrow1.Size = new System.Drawing.Size(30, 40);
            this.leftArrow1.TabIndex = 2;
            this.leftArrow1.UseVisualStyleBackColor = false;
            this.leftArrow1.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(12, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 580);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.rightArrow1);
            this.Controls.Add(this.leftArrow1);
            this.Controls.Add(this.btnSetWall);
            this.Controls.Add(this.pctBoxWall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BingPaper";
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxWall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBoxWall;
        private System.Windows.Forms.Button btnSetWall;
        private ArrowLeft leftArrow1;
        private ArrowRight rightArrow1;
        private System.Windows.Forms.Label lblName;
    }
}

