namespace CENEX.MODEL.PAINT
{

    partial class PaintForm
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
            this.LineDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.RectDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.EllipseDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.FilledRectangle = new System.Windows.Forms.ToolStripMenuItem();
            this.FilledEllipse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LineDraw,
            this.RectDraw,
            this.EllipseDraw,
            this.FilledRectangle,
            this.FilledEllipse});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(580, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // LineDraw
            // 
            this.LineDraw.Name = "LineDraw";
            this.LineDraw.Size = new System.Drawing.Size(64, 20);
            this.LineDraw.Text = "LineDraw";
            this.LineDraw.Click += new System.EventHandler(this.LineDraw_Click);
            // 
            // RectDraw
            // 
            this.RectDraw.Name = "RectDraw";
            this.RectDraw.Size = new System.Drawing.Size(67, 20);
            this.RectDraw.Text = "RectDraw";
            this.RectDraw.Click += new System.EventHandler(this.RectDraw_Click);
            // 
            // EllipseDraw
            // 
            this.EllipseDraw.Name = "EllipseDraw";
            this.EllipseDraw.Size = new System.Drawing.Size(74, 20);
            this.EllipseDraw.Text = "EllipseDraw";
            this.EllipseDraw.Click += new System.EventHandler(this.EllipseDraw_Click);
            // 
            // FilledRectangle
            // 
            this.FilledRectangle.Name = "FilledRectangle";
            this.FilledRectangle.Size = new System.Drawing.Size(92, 20);
            this.FilledRectangle.Text = "FilledRectangle";
            this.FilledRectangle.Click += new System.EventHandler(this.FilledRectangle_Click);
            // 
            // FilledEllipse
            // 
            this.FilledEllipse.Name = "FilledEllipse";
            this.FilledEllipse.Size = new System.Drawing.Size(73, 20);
            this.FilledEllipse.Text = "FilledEllipse";
            this.FilledEllipse.Click += new System.EventHandler(this.FilledEllipse_Click);
            // 
            // PaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 264);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PaintForm";
            this.Text = "Paint";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem LineDraw;
        private System.Windows.Forms.ToolStripMenuItem RectDraw;
        private System.Windows.Forms.ToolStripMenuItem EllipseDraw;
        private System.Windows.Forms.ToolStripMenuItem FilledRectangle;
        private System.Windows.Forms.ToolStripMenuItem FilledEllipse;
        

    }
}

