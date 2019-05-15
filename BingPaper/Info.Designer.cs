namespace BingPaper
{
    partial class Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
            this.btnClose = new System.Windows.Forms.Button();
            this.pbBing = new System.Windows.Forms.PictureBox();
            this.lblBing = new System.Windows.Forms.Label();
            this.pbGit = new System.Windows.Forms.PictureBox();
            this.pbTwitter = new System.Windows.Forms.PictureBox();
            this.pbInsta = new System.Windows.Forms.PictureBox();
            this.pbCoffee = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTwitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoffee)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Firebrick;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(458, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(35, 15);
            this.btnClose.TabIndex = 15;
            this.btnClose.TabStop = false;
            this.btnClose.Tag = "close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbBing
            // 
            this.pbBing.BackColor = System.Drawing.Color.Transparent;
            this.pbBing.Image = global::BingPaper.Properties.Resources.bingpaper;
            this.pbBing.Location = new System.Drawing.Point(12, 7);
            this.pbBing.Name = "pbBing";
            this.pbBing.Size = new System.Drawing.Size(64, 64);
            this.pbBing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBing.TabIndex = 22;
            this.pbBing.TabStop = false;
            // 
            // lblBing
            // 
            this.lblBing.AutoSize = true;
            this.lblBing.BackColor = System.Drawing.Color.Transparent;
            this.lblBing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBing.ForeColor = System.Drawing.SystemColors.Control;
            this.lblBing.Location = new System.Drawing.Point(91, 33);
            this.lblBing.MaximumSize = new System.Drawing.Size(360, 80);
            this.lblBing.MinimumSize = new System.Drawing.Size(35, 13);
            this.lblBing.Name = "lblBing";
            this.lblBing.Size = new System.Drawing.Size(347, 80);
            this.lblBing.TabIndex = 23;
            this.lblBing.Text = resources.GetString("lblBing.Text");
            this.lblBing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbGit
            // 
            this.pbGit.BackColor = System.Drawing.Color.Transparent;
            this.pbGit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbGit.Image = global::BingPaper.Properties.Resources.github1;
            this.pbGit.Location = new System.Drawing.Point(228, 164);
            this.pbGit.Name = "pbGit";
            this.pbGit.Size = new System.Drawing.Size(141, 40);
            this.pbGit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGit.TabIndex = 19;
            this.pbGit.TabStop = false;
            this.pbGit.Click += new System.EventHandler(this.Images_Click);
            // 
            // pbTwitter
            // 
            this.pbTwitter.BackColor = System.Drawing.Color.Transparent;
            this.pbTwitter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbTwitter.Image = global::BingPaper.Properties.Resources.twitter1;
            this.pbTwitter.Location = new System.Drawing.Point(40, 199);
            this.pbTwitter.Name = "pbTwitter";
            this.pbTwitter.Size = new System.Drawing.Size(180, 25);
            this.pbTwitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTwitter.TabIndex = 18;
            this.pbTwitter.TabStop = false;
            this.pbTwitter.Click += new System.EventHandler(this.Images_Click);
            // 
            // pbInsta
            // 
            this.pbInsta.BackColor = System.Drawing.Color.Transparent;
            this.pbInsta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbInsta.Image = global::BingPaper.Properties.Resources.instagram1;
            this.pbInsta.Location = new System.Drawing.Point(40, 132);
            this.pbInsta.Name = "pbInsta";
            this.pbInsta.Size = new System.Drawing.Size(180, 40);
            this.pbInsta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInsta.TabIndex = 17;
            this.pbInsta.TabStop = false;
            this.pbInsta.Click += new System.EventHandler(this.Images_Click);
            // 
            // pbCoffee
            // 
            this.pbCoffee.BackColor = System.Drawing.Color.Transparent;
            this.pbCoffee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCoffee.Image = global::BingPaper.Properties.Resources.coffee;
            this.pbCoffee.Location = new System.Drawing.Point(380, 132);
            this.pbCoffee.Name = "pbCoffee";
            this.pbCoffee.Size = new System.Drawing.Size(80, 95);
            this.pbCoffee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCoffee.TabIndex = 24;
            this.pbCoffee.TabStop = false;
            this.pbCoffee.Click += new System.EventHandler(this.Images_Click);
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.pbCoffee);
            this.Controls.Add(this.lblBing);
            this.Controls.Add(this.pbBing);
            this.Controls.Add(this.pbGit);
            this.Controls.Add(this.pbTwitter);
            this.Controls.Add(this.pbInsta);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "Info";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Info";
            this.Load += new System.EventHandler(this.Info_OnLoad);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbBing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTwitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInsta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoffee)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbBing;
        private System.Windows.Forms.Label lblBing;
        private System.Windows.Forms.PictureBox pbGit;
        private System.Windows.Forms.PictureBox pbTwitter;
        private System.Windows.Forms.PictureBox pbInsta;
        private System.Windows.Forms.PictureBox pbCoffee;
    }
}