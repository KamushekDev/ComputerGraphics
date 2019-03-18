namespace _Forms_CompGraph_1_11_ {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pbScene = new System.Windows.Forms.PictureBox();
            this.lblFirstCircle = new System.Windows.Forms.Label();
            this.tbFirstX = new System.Windows.Forms.TextBox();
            this.tbFirstY = new System.Windows.Forms.TextBox();
            this.tbFirstRadius = new System.Windows.Forms.TextBox();
            this.tbSecondRadius = new System.Windows.Forms.TextBox();
            this.tbSecondY = new System.Windows.Forms.TextBox();
            this.tbSecondX = new System.Windows.Forms.TextBox();
            this.lblSecondCircle = new System.Windows.Forms.Label();
            this.tbAdditional = new System.Windows.Forms.TextBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbScene)).BeginInit();
            this.SuspendLayout();
            // 
            // pbScene
            // 
            this.pbScene.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbScene.Location = new System.Drawing.Point(9, 10);
            this.pbScene.Margin = new System.Windows.Forms.Padding(2);
            this.pbScene.Name = "pbScene";
            this.pbScene.Size = new System.Drawing.Size(501, 501);
            this.pbScene.TabIndex = 0;
            this.pbScene.TabStop = false;
            this.toolTip.SetToolTip(this.pbScene, "sd");
            this.pbScene.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbScene_MouseClick);
            this.pbScene.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbScene_MouseMove);
            // 
            // lblFirstCircle
            // 
            this.lblFirstCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFirstCircle.AutoSize = true;
            this.lblFirstCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFirstCircle.Location = new System.Drawing.Point(569, 10);
            this.lblFirstCircle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFirstCircle.Name = "lblFirstCircle";
            this.lblFirstCircle.Size = new System.Drawing.Size(55, 13);
            this.lblFirstCircle.TabIndex = 1;
            this.lblFirstCircle.Text = "First Circle";
            // 
            // tbFirstX
            // 
            this.tbFirstX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFirstX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFirstX.Location = new System.Drawing.Point(519, 28);
            this.tbFirstX.Margin = new System.Windows.Forms.Padding(2);
            this.tbFirstX.Name = "tbFirstX";
            this.tbFirstX.Size = new System.Drawing.Size(76, 20);
            this.tbFirstX.TabIndex = 2;
            // 
            // tbFirstY
            // 
            this.tbFirstY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFirstY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFirstY.Location = new System.Drawing.Point(598, 28);
            this.tbFirstY.Margin = new System.Windows.Forms.Padding(2);
            this.tbFirstY.Name = "tbFirstY";
            this.tbFirstY.Size = new System.Drawing.Size(76, 20);
            this.tbFirstY.TabIndex = 3;
            // 
            // tbFirstRadius
            // 
            this.tbFirstRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFirstRadius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFirstRadius.Location = new System.Drawing.Point(519, 52);
            this.tbFirstRadius.Margin = new System.Windows.Forms.Padding(2);
            this.tbFirstRadius.Name = "tbFirstRadius";
            this.tbFirstRadius.Size = new System.Drawing.Size(155, 20);
            this.tbFirstRadius.TabIndex = 4;
            // 
            // tbSecondRadius
            // 
            this.tbSecondRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecondRadius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSecondRadius.Location = new System.Drawing.Point(519, 136);
            this.tbSecondRadius.Margin = new System.Windows.Forms.Padding(2);
            this.tbSecondRadius.Name = "tbSecondRadius";
            this.tbSecondRadius.Size = new System.Drawing.Size(155, 20);
            this.tbSecondRadius.TabIndex = 8;
            // 
            // tbSecondY
            // 
            this.tbSecondY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecondY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSecondY.Location = new System.Drawing.Point(598, 114);
            this.tbSecondY.Margin = new System.Windows.Forms.Padding(2);
            this.tbSecondY.Name = "tbSecondY";
            this.tbSecondY.Size = new System.Drawing.Size(76, 20);
            this.tbSecondY.TabIndex = 7;
            // 
            // tbSecondX
            // 
            this.tbSecondX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSecondX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSecondX.Location = new System.Drawing.Point(519, 114);
            this.tbSecondX.Margin = new System.Windows.Forms.Padding(2);
            this.tbSecondX.Name = "tbSecondX";
            this.tbSecondX.Size = new System.Drawing.Size(76, 20);
            this.tbSecondX.TabIndex = 6;
            // 
            // lblSecondCircle
            // 
            this.lblSecondCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSecondCircle.AutoSize = true;
            this.lblSecondCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSecondCircle.Location = new System.Drawing.Point(560, 98);
            this.lblSecondCircle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSecondCircle.Name = "lblSecondCircle";
            this.lblSecondCircle.Size = new System.Drawing.Size(73, 13);
            this.lblSecondCircle.TabIndex = 5;
            this.lblSecondCircle.Text = "Second Circle";
            // 
            // tbAdditional
            // 
            this.tbAdditional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAdditional.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAdditional.Location = new System.Drawing.Point(519, 487);
            this.tbAdditional.Margin = new System.Windows.Forms.Padding(2);
            this.tbAdditional.Name = "tbAdditional";
            this.tbAdditional.Size = new System.Drawing.Size(155, 20);
            this.tbAdditional.TabIndex = 9;
            // 
            // btnDraw
            // 
            this.btnDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDraw.Location = new System.Drawing.Point(519, 175);
            this.btnDraw.Margin = new System.Windows.Forms.Padding(2);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(155, 24);
            this.btnDraw.TabIndex = 10;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(579, 405);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(35, 13);
            this.lblWidth.TabIndex = 11;
            this.lblWidth.Text = "Width";
            // 
            // lblHeight
            // 
            this.lblHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(577, 425);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(38, 13);
            this.lblHeight.TabIndex = 12;
            this.lblHeight.Text = "Height";
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 5000;
            this.toolTip.AutoPopDelay = 50000;
            this.toolTip.InitialDelay = 500000;
            this.toolTip.ReshowDelay = 1000;
            this.toolTip.Tag = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 521);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.tbAdditional);
            this.Controls.Add(this.tbSecondRadius);
            this.Controls.Add(this.tbSecondY);
            this.Controls.Add(this.tbSecondX);
            this.Controls.Add(this.lblSecondCircle);
            this.Controls.Add(this.tbFirstRadius);
            this.Controls.Add(this.tbFirstY);
            this.Controls.Add(this.tbFirstX);
            this.Controls.Add(this.lblFirstCircle);
            this.Controls.Add(this.pbScene);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(16, 518);
            this.Name = "Form1";
            this.Text = "Tuturu";
            ((System.ComponentModel.ISupportInitialize)(this.pbScene)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbScene;
        private System.Windows.Forms.Label lblFirstCircle;
        private System.Windows.Forms.TextBox tbFirstX;
        private System.Windows.Forms.TextBox tbFirstY;
        private System.Windows.Forms.TextBox tbFirstRadius;
        private System.Windows.Forms.TextBox tbSecondRadius;
        private System.Windows.Forms.TextBox tbSecondY;
        private System.Windows.Forms.TextBox tbSecondX;
        private System.Windows.Forms.Label lblSecondCircle;
        private System.Windows.Forms.TextBox tbAdditional;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

