namespace YImageForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.图片剪切ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CropToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图片剪切ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 图片剪切ToolStripMenuItem
            // 
            this.图片剪切ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CropToolStripMenuItem1,
            this.CutToolStripMenuItem1});
            this.图片剪切ToolStripMenuItem.Name = "图片剪切ToolStripMenuItem";
            this.图片剪切ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图片剪切ToolStripMenuItem.Text = "图片工具";
            // 
            // CutToolStripMenuItem1
            // 
            this.CutToolStripMenuItem1.Name = "CutToolStripMenuItem1";
            this.CutToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.CutToolStripMenuItem1.Text = "图片分割";
            this.CutToolStripMenuItem1.Click += new System.EventHandler(this.CutToolStripMenuItem1_Click);
            // 
            // CropToolStripMenuItem1
            // 
            this.CropToolStripMenuItem1.Name = "CropToolStripMenuItem1";
            this.CropToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.CropToolStripMenuItem1.Text = "图片剪切";
            this.CropToolStripMenuItem1.Click += new System.EventHandler(this.CropToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ImageTool";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 图片剪切ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CropToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem1;
    }
}