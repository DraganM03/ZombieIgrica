namespace Igrica
{
    partial class gameForm
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
            this.txtStats = new System.Windows.Forms.Label();
            this.txtAmmo = new System.Windows.Forms.Label();
            this.txtKills = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.txtHealth = new System.Windows.Forms.Label();
            this.pbHealth = new System.Windows.Forms.ProgressBar();
            this.txtTest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtStats
            // 
            this.txtStats.AutoSize = true;
            this.txtStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtStats.ForeColor = System.Drawing.Color.White;
            this.txtStats.Location = new System.Drawing.Point(37, 22);
            this.txtStats.Name = "txtStats";
            this.txtStats.Size = new System.Drawing.Size(40, 16);
            this.txtStats.TabIndex = 0;
            this.txtStats.Text = "Stats:";
            // 
            // txtAmmo
            // 
            this.txtAmmo.AutoSize = true;
            this.txtAmmo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtAmmo.ForeColor = System.Drawing.Color.White;
            this.txtAmmo.Location = new System.Drawing.Point(254, 22);
            this.txtAmmo.Name = "txtAmmo";
            this.txtAmmo.Size = new System.Drawing.Size(49, 16);
            this.txtAmmo.TabIndex = 1;
            this.txtAmmo.Text = "Ammo:";
            // 
            // txtKills
            // 
            this.txtKills.AutoSize = true;
            this.txtKills.ForeColor = System.Drawing.Color.White;
            this.txtKills.Location = new System.Drawing.Point(478, 22);
            this.txtKills.Name = "txtKills";
            this.txtKills.Size = new System.Drawing.Size(31, 16);
            this.txtKills.TabIndex = 2;
            this.txtKills.Text = "Kills";
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.MainTimerEvent);
            // 
            // txtHealth
            // 
            this.txtHealth.AutoSize = true;
            this.txtHealth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtHealth.ForeColor = System.Drawing.Color.White;
            this.txtHealth.Location = new System.Drawing.Point(630, 22);
            this.txtHealth.Name = "txtHealth";
            this.txtHealth.Size = new System.Drawing.Size(49, 16);
            this.txtHealth.TabIndex = 4;
            this.txtHealth.Text = "Health:";
            // 
            // pbHealth
            // 
            this.pbHealth.Location = new System.Drawing.Point(685, 22);
            this.pbHealth.Name = "pbHealth";
            this.pbHealth.Size = new System.Drawing.Size(226, 16);
            this.pbHealth.TabIndex = 5;
            this.pbHealth.Value = 100;
            // 
            // txtTest
            // 
            this.txtTest.AutoSize = true;
            this.txtTest.ForeColor = System.Drawing.Color.White;
            this.txtTest.Location = new System.Drawing.Point(40, 71);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(28, 16);
            this.txtTest.TabIndex = 6;
            this.txtTest.Text = "test";
            // 
            // gameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
            this.ClientSize = new System.Drawing.Size(942, 553);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.pbHealth);
            this.Controls.Add(this.txtHealth);
            this.Controls.Add(this.txtKills);
            this.Controls.Add(this.txtAmmo);
            this.Controls.Add(this.txtStats);
            this.DoubleBuffered = true;
            this.Name = "gameForm";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawForm);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayerShoot);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlayerAim);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtStats;
        private System.Windows.Forms.Label txtAmmo;
        private System.Windows.Forms.Label txtKills;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label txtHealth;
        private System.Windows.Forms.ProgressBar pbHealth;
        private System.Windows.Forms.Label txtTest;
    }
}

