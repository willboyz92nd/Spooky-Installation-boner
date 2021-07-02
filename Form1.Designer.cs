namespace LizzyInstaller
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
            this.PathBox = new System.Windows.Forms.MaskedTextBox();
            this.InstallButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PathSetButton = new System.Windows.Forms.Button();
            this.ExtractingLabel = new System.Windows.Forms.Label();
            this.DownloadingLabel = new System.Windows.Forms.Label();
            this.BetaBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(12, 12);
            this.PathBox.Name = "PathBox";
            this.PathBox.ReadOnly = true;
            this.PathBox.Size = new System.Drawing.Size(553, 20);
            this.PathBox.TabIndex = 0;
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(577, 11);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 20);
            this.InstallButton.TabIndex = 1;
            this.InstallButton.Text = "Install";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 38);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(553, 20);
            this.progressBar1.TabIndex = 2;
            // 
            // PathSetButton
            // 
            this.PathSetButton.Location = new System.Drawing.Point(577, 37);
            this.PathSetButton.Name = "PathSetButton";
            this.PathSetButton.Size = new System.Drawing.Size(75, 20);
            this.PathSetButton.TabIndex = 3;
            this.PathSetButton.Text = "Set Path";
            this.PathSetButton.UseVisualStyleBackColor = true;
            this.PathSetButton.Click += new System.EventHandler(this.PathSetButton_Click);
            // 
            // ExtractingLabel
            // 
            this.ExtractingLabel.AutoSize = true;
            this.ExtractingLabel.BackColor = System.Drawing.Color.Transparent;
            this.ExtractingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ExtractingLabel.Location = new System.Drawing.Point(264, 60);
            this.ExtractingLabel.Name = "ExtractingLabel";
            this.ExtractingLabel.Size = new System.Drawing.Size(61, 15);
            this.ExtractingLabel.TabIndex = 4;
            this.ExtractingLabel.Text = "Extracting";
            this.ExtractingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ExtractingLabel.Visible = false;
            // 
            // DownloadingLabel
            // 
            this.DownloadingLabel.AutoSize = true;
            this.DownloadingLabel.BackColor = System.Drawing.Color.Transparent;
            this.DownloadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.DownloadingLabel.Location = new System.Drawing.Point(255, 63);
            this.DownloadingLabel.Name = "DownloadingLabel";
            this.DownloadingLabel.Size = new System.Drawing.Size(80, 15);
            this.DownloadingLabel.TabIndex = 5;
            this.DownloadingLabel.Text = "Downloading";
            this.DownloadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DownloadingLabel.Visible = false;
            // 
            // BetaBox
            // 
            this.BetaBox.AutoSize = true;
            this.BetaBox.Location = new System.Drawing.Point(549, 61);
            this.BetaBox.Name = "BetaBox";
            this.BetaBox.Size = new System.Drawing.Size(104, 17);
            this.BetaBox.TabIndex = 6;
            this.BetaBox.Text = "Download Betas";
            this.BetaBox.UseVisualStyleBackColor = true;
            this.BetaBox.CheckedChanged += new System.EventHandler(this.BetaBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 81);
            this.Controls.Add(this.BetaBox);
            this.Controls.Add(this.DownloadingLabel);
            this.Controls.Add(this.ExtractingLabel);
            this.Controls.Add(this.PathSetButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.PathBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Lizzy\'s Launcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox PathBox;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button PathSetButton;
        private System.Windows.Forms.Label ExtractingLabel;
        private System.Windows.Forms.Label DownloadingLabel;
        private System.Windows.Forms.CheckBox BetaBox;
    }
}

