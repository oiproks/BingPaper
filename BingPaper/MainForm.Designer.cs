﻿namespace BingPaper
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pctBoxWall = new System.Windows.Forms.PictureBox();
            this.btnSetWall = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.bingPaper2 = new System.Windows.Forms.PictureBox();
            this.btnSetMulti = new System.Windows.Forms.Button();
            this.btnOption = new System.Windows.Forms.Button();
            this.rightArrow1 = new BingPaper.ArrowRight();
            this.leftArrow1 = new BingPaper.ArrowLeft();
            this.btnPast = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxWall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bingPaper2)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBoxWall
            // 
            this.pctBoxWall.BackColor = System.Drawing.Color.Transparent;
            this.pctBoxWall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pctBoxWall.Location = new System.Drawing.Point(0, 20);
            this.pctBoxWall.Name = "pctBoxWall";
            this.pctBoxWall.Size = new System.Drawing.Size(960, 540);
            this.pctBoxWall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctBoxWall.TabIndex = 0;
            this.pctBoxWall.TabStop = false;
            // 
            // btnSetWall
            // 
            this.btnSetWall.BackColor = System.Drawing.Color.Transparent;
            this.btnSetWall.BackgroundImage = global::BingPaper.Properties.Resources.set;
            this.btnSetWall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetWall.FlatAppearance.BorderSize = 0;
            this.btnSetWall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSetWall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSetWall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetWall.ForeColor = System.Drawing.Color.Navy;
            this.btnSetWall.Location = new System.Drawing.Point(769, 24);
            this.btnSetWall.Name = "btnSetWall";
            this.btnSetWall.Size = new System.Drawing.Size(187, 41);
            this.btnSetWall.TabIndex = 1;
            this.btnSetWall.UseVisualStyleBackColor = false;
            this.btnSetWall.Click += new System.EventHandler(this.btnSetWall_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(7, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Firebrick;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(932, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 10);
            this.btnClose.TabIndex = 13;
            this.btnClose.TabStop = false;
            this.btnClose.Tag = "close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.White;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Location = new System.Drawing.Point(898, 4);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(25, 10);
            this.btnMin.TabIndex = 12;
            this.btnMin.TabStop = false;
            this.btnMin.Tag = "minimize";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // bingPaper2
            // 
            this.bingPaper2.BackColor = System.Drawing.Color.Transparent;
            this.bingPaper2.BackgroundImage = global::BingPaper.Properties.Resources.me;
            this.bingPaper2.Cursor = System.Windows.Forms.Cursors.Help;
            this.bingPaper2.Location = new System.Drawing.Point(820, 544);
            this.bingPaper2.Name = "bingPaper2";
            this.bingPaper2.Size = new System.Drawing.Size(139, 15);
            this.bingPaper2.TabIndex = 17;
            this.bingPaper2.TabStop = false;
            this.bingPaper2.Click += new System.EventHandler(this.ShowInfo);
            // 
            // btnSetMulti
            // 
            this.btnSetMulti.BackColor = System.Drawing.Color.Transparent;
            this.btnSetMulti.BackgroundImage = global::BingPaper.Properties.Resources.set_multi;
            this.btnSetMulti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetMulti.FlatAppearance.BorderSize = 0;
            this.btnSetMulti.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSetMulti.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSetMulti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetMulti.ForeColor = System.Drawing.Color.Navy;
            this.btnSetMulti.Location = new System.Drawing.Point(769, 71);
            this.btnSetMulti.Name = "btnSetMulti";
            this.btnSetMulti.Size = new System.Drawing.Size(187, 41);
            this.btnSetMulti.TabIndex = 18;
            this.btnSetMulti.UseVisualStyleBackColor = false;
            this.btnSetMulti.Visible = false;
            this.btnSetMulti.Click += new System.EventHandler(this.btnSetMultitWall_Click);
            // 
            // btnOption
            // 
            this.btnOption.BackColor = System.Drawing.Color.Transparent;
            this.btnOption.BackgroundImage = global::BingPaper.Properties.Resources.option;
            this.btnOption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOption.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnOption.FlatAppearance.BorderSize = 0;
            this.btnOption.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnOption.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOption.Location = new System.Drawing.Point(866, 0);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(20, 20);
            this.btnOption.TabIndex = 19;
            this.btnOption.UseVisualStyleBackColor = false;
            this.btnOption.Click += new System.EventHandler(this.btnOption_Click);
            // 
            // rightArrow1
            // 
            this.rightArrow1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rightArrow1.FlatAppearance.BorderSize = 0;
            this.rightArrow1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rightArrow1.Location = new System.Drawing.Point(920, 270);
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
            this.leftArrow1.Location = new System.Drawing.Point(10, 270);
            this.leftArrow1.Name = "leftArrow1";
            this.leftArrow1.Size = new System.Drawing.Size(30, 40);
            this.leftArrow1.TabIndex = 2;
            this.leftArrow1.UseVisualStyleBackColor = false;
            this.leftArrow1.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnPast
            // 
            this.btnPast.BackColor = System.Drawing.Color.Transparent;
            this.btnPast.BackgroundImage = global::BingPaper.Properties.Resources.past;
            this.btnPast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPast.FlatAppearance.BorderSize = 0;
            this.btnPast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPast.Location = new System.Drawing.Point(0, 26);
            this.btnPast.Name = "btnPast";
            this.btnPast.Size = new System.Drawing.Size(72, 39);
            this.btnPast.TabIndex = 20;
            this.btnPast.UseVisualStyleBackColor = false;
            this.btnPast.Visible = false;
            this.btnPast.Click += new System.EventHandler(this.btnLoadPast);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.Controls.Add(this.btnPast);
            this.Controls.Add(this.btnOption);
            this.Controls.Add(this.btnSetMulti);
            this.Controls.Add(this.bingPaper2);
            this.Controls.Add(this.btnSetWall);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.rightArrow1);
            this.Controls.Add(this.leftArrow1);
            this.Controls.Add(this.pctBoxWall);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BingPaper";
            this.Activated += new System.EventHandler(this.MainForm_Activate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pctBoxWall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bingPaper2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBoxWall;
        private System.Windows.Forms.Button btnSetWall;
        private ArrowLeft leftArrow1;
        private ArrowRight rightArrow1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.PictureBox bingPaper2;
        private System.Windows.Forms.Button btnSetMulti;
        private System.Windows.Forms.Button btnOption;
        private System.Windows.Forms.Button btnPast;
    }
}

