namespace Tetris
{
    partial class GameOver
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
            this.lbGameOver = new System.Windows.Forms.Label();
            this.lbQuestion2 = new System.Windows.Forms.Label();
            this.tbAnswer2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbGameOver
            // 
            this.lbGameOver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGameOver.AutoSize = true;
            this.lbGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGameOver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbGameOver.Location = new System.Drawing.Point(115, 6);
            this.lbGameOver.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbGameOver.Name = "lbGameOver";
            this.lbGameOver.Size = new System.Drawing.Size(177, 31);
            this.lbGameOver.TabIndex = 0;
            this.lbGameOver.Text = "GAME OVER";
            // 
            // lbQuestion2
            // 
            this.lbQuestion2.AutoSize = true;
            this.lbQuestion2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuestion2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbQuestion2.Location = new System.Drawing.Point(11, 65);
            this.lbQuestion2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbQuestion2.Name = "lbQuestion2";
            this.lbQuestion2.Size = new System.Drawing.Size(358, 17);
            this.lbQuestion2.TabIndex = 1;
            this.lbQuestion2.Text = "Do you want try to make a new record ones more? (y/n)";
            // 
            // tbAnswer2
            // 
            this.tbAnswer2.BackColor = System.Drawing.Color.Black;
            this.tbAnswer2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAnswer2.ForeColor = System.Drawing.Color.White;
            this.tbAnswer2.Location = new System.Drawing.Point(373, 69);
            this.tbAnswer2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbAnswer2.Name = "tbAnswer2";
            this.tbAnswer2.Size = new System.Drawing.Size(25, 13);
            this.tbAnswer2.TabIndex = 2;
            this.tbAnswer2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbAnswer2_KeyPress);
            // 
            // GameOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(407, 118);
            this.ControlBox = false;
            this.Controls.Add(this.tbAnswer2);
            this.Controls.Add(this.lbQuestion2);
            this.Controls.Add(this.lbGameOver);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(423, 157);
            this.MinimumSize = new System.Drawing.Size(423, 157);
            this.Name = "GameOver";
            this.Text = ":(";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbGameOver;
        private System.Windows.Forms.Label lbQuestion2;
        private System.Windows.Forms.TextBox tbAnswer2;
    }
}