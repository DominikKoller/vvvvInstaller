namespace VVVV
{
    partial class InstallerForm
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
            this.CloseButton = new System.Windows.Forms.Label();
            this.dragPanel = new System.Windows.Forms.Panel();
            this.DepsIndicator = new System.Windows.Forms.Label();
            this.force32CheckBox = new System.Windows.Forms.CheckBox();
            this.addAddonsCheck = new System.Windows.Forms.CheckBox();
            this.choosePathButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StartButton = new System.Windows.Forms.Button();
            this.InstallPathBox = new System.Windows.Forms.TextBox();
            this.dragPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Location = new System.Drawing.Point(457, 9);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 13);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "close";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CloseButton_Click);
            // 
            // dragPanel
            // 
            this.dragPanel.Controls.Add(this.DepsIndicator);
            this.dragPanel.Controls.Add(this.force32CheckBox);
            this.dragPanel.Controls.Add(this.addAddonsCheck);
            this.dragPanel.Controls.Add(this.choosePathButton);
            this.dragPanel.Controls.Add(this.progressBar1);
            this.dragPanel.Controls.Add(this.StartButton);
            this.dragPanel.Controls.Add(this.InstallPathBox);
            this.dragPanel.Controls.Add(this.CloseButton);
            this.dragPanel.Location = new System.Drawing.Point(-1, 0);
            this.dragPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dragPanel.Name = "dragPanel";
            this.dragPanel.Size = new System.Drawing.Size(500, 501);
            this.dragPanel.TabIndex = 1;
            this.dragPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.dragPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.dragPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // DepsIndicator
            // 
            this.DepsIndicator.AutoSize = true;
            this.DepsIndicator.BackColor = System.Drawing.Color.Red;
            this.DepsIndicator.Location = new System.Drawing.Point(247, 274);
            this.DepsIndicator.Name = "DepsIndicator";
            this.DepsIndicator.Size = new System.Drawing.Size(19, 13);
            this.DepsIndicator.TabIndex = 28;
            this.DepsIndicator.Text = "    ";
            // 
            // force32CheckBox
            // 
            this.force32CheckBox.AutoSize = true;
            this.force32CheckBox.Location = new System.Drawing.Point(129, 232);
            this.force32CheckBox.Name = "force32CheckBox";
            this.force32CheckBox.Size = new System.Drawing.Size(79, 17);
            this.force32CheckBox.TabIndex = 18;
            this.force32CheckBox.Text = "force 32 bit";
            this.force32CheckBox.UseVisualStyleBackColor = true;
            // 
            // addAddonsCheck
            // 
            this.addAddonsCheck.AutoSize = true;
            this.addAddonsCheck.Checked = true;
            this.addAddonsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.addAddonsCheck.Location = new System.Drawing.Point(214, 232);
            this.addAddonsCheck.Name = "addAddonsCheck";
            this.addAddonsCheck.Size = new System.Drawing.Size(99, 17);
            this.addAddonsCheck.TabIndex = 8;
            this.addAddonsCheck.Text = "include Addons";
            this.addAddonsCheck.UseVisualStyleBackColor = true;
            // 
            // choosePathButton
            // 
            this.choosePathButton.Location = new System.Drawing.Point(102, 186);
            this.choosePathButton.Name = "choosePathButton";
            this.choosePathButton.Size = new System.Drawing.Size(32, 23);
            this.choosePathButton.TabIndex = 6;
            this.choosePathButton.Text = "O";
            this.choosePathButton.UseVisualStyleBackColor = true;
            this.choosePathButton.Click += new System.EventHandler(this.choosePathButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(102, 307);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(232, 10);
            this.progressBar1.TabIndex = 4;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(166, 269);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start Setup";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // InstallPathBox
            // 
            this.InstallPathBox.Location = new System.Drawing.Point(140, 189);
            this.InstallPathBox.Name = "InstallPathBox";
            this.InstallPathBox.Size = new System.Drawing.Size(232, 20);
            this.InstallPathBox.TabIndex = 2;
            this.InstallPathBox.Text = "C:\\vvvv";
            // 
            // InstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(500, 500);
            this.Controls.Add(this.dragPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InstallerForm";
            this.Text = "B";
            this.dragPanel.ResumeLayout(false);
            this.dragPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label CloseButton;
        private System.Windows.Forms.Panel dragPanel;
        private System.Windows.Forms.TextBox InstallPathBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button choosePathButton;
        private System.Windows.Forms.CheckBox addAddonsCheck;
        private System.Windows.Forms.CheckBox force32CheckBox;
        private System.Windows.Forms.Label DepsIndicator;
    }
}

