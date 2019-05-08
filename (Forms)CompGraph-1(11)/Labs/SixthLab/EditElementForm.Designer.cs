namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    partial class EditElementForm
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
            this.SaveElementBtn = new System.Windows.Forms.Button();
            this.ElementTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveElementBtn
            // 
            this.SaveElementBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SaveElementBtn.Location = new System.Drawing.Point(0, 385);
            this.SaveElementBtn.Name = "SaveElementBtn";
            this.SaveElementBtn.Size = new System.Drawing.Size(739, 23);
            this.SaveElementBtn.TabIndex = 0;
            this.SaveElementBtn.Text = "Save";
            this.SaveElementBtn.UseVisualStyleBackColor = true;
            this.SaveElementBtn.Click += new System.EventHandler(this.SaveElementBtn_Click);
            // 
            // ElementTB
            // 
            this.ElementTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.ElementTB.Location = new System.Drawing.Point(0, 0);
            this.ElementTB.Multiline = true;
            this.ElementTB.Name = "ElementTB";
            this.ElementTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ElementTB.Size = new System.Drawing.Size(739, 379);
            this.ElementTB.TabIndex = 1;
            // 
            // EditElementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 408);
            this.Controls.Add(this.ElementTB);
            this.Controls.Add(this.SaveElementBtn);
            this.Name = "EditElementForm";
            this.Text = "EditElementForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveElementBtn;
        private System.Windows.Forms.TextBox ElementTB;
    }
}