namespace UmbraInjector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Title = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.UpdateCheck = new System.Windows.Forms.Timer(this.components);
            this.InjectButton = new System.Windows.Forms.Button();
            this.autoUpdateCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Title.ForeColor = System.Drawing.Color.Aqua;
            this.Title.Location = new System.Drawing.Point(67, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(177, 57);
            this.Title.TabIndex = 0;
            this.Title.Text = "Umbra Injector";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::UmbraInjector.Properties.Resources.UmbraInjecter_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(-4, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // UpdateCheck
            // 
            this.UpdateCheck.Enabled = true;
            this.UpdateCheck.Tick += new System.EventHandler(this.UpdateCheck_Tick);
            // 
            // InjectButton
            // 
            this.InjectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.InjectButton.ForeColor = System.Drawing.Color.Aqua;
            this.InjectButton.Location = new System.Drawing.Point(12, 76);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(220, 64);
            this.InjectButton.TabIndex = 4;
            this.InjectButton.Text = "Inject";
            this.InjectButton.UseVisualStyleBackColor = false;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // autoUpdateCheck
            // 
            this.autoUpdateCheck.AutoSize = true;
            this.autoUpdateCheck.Checked = true;
            this.autoUpdateCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoUpdateCheck.ForeColor = System.Drawing.Color.Aqua;
            this.autoUpdateCheck.Location = new System.Drawing.Point(158, 146);
            this.autoUpdateCheck.Name = "autoUpdateCheck";
            this.autoUpdateCheck.Size = new System.Drawing.Size(86, 17);
            this.autoUpdateCheck.TabIndex = 5;
            this.autoUpdateCheck.Text = "Auto Update";
            this.autoUpdateCheck.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(244, 164);
            this.Controls.Add(this.autoUpdateCheck);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Umbra Injector";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer UpdateCheck;
        private System.Windows.Forms.Button InjectButton;
        private System.Windows.Forms.CheckBox autoUpdateCheck;
    }
}