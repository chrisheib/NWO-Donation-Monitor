namespace NW_Spendenmonitor
{
    partial class Export
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
            this.export1 = new System.Windows.Forms.Button();
            this.export2 = new System.Windows.Forms.Button();
            this.rb_exportfile = new System.Windows.Forms.RadioButton();
            this.rb_exportclipboard = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // export1
            // 
            this.export1.Location = new System.Drawing.Point(12, 163);
            this.export1.Name = "export1";
            this.export1.Size = new System.Drawing.Size(75, 23);
            this.export1.TabIndex = 0;
            this.export1.Text = "Export 1";
            this.export1.UseVisualStyleBackColor = true;
            this.export1.Click += new System.EventHandler(this.export1_Click);
            // 
            // export2
            // 
            this.export2.Location = new System.Drawing.Point(93, 163);
            this.export2.Name = "export2";
            this.export2.Size = new System.Drawing.Size(75, 23);
            this.export2.TabIndex = 1;
            this.export2.Text = "Export 2";
            this.export2.UseVisualStyleBackColor = true;
            this.export2.Click += new System.EventHandler(this.Btn_export2_Click);
            // 
            // rb_exportfile
            // 
            this.rb_exportfile.AutoSize = true;
            this.rb_exportfile.Checked = true;
            this.rb_exportfile.Location = new System.Drawing.Point(12, 12);
            this.rb_exportfile.Name = "rb_exportfile";
            this.rb_exportfile.Size = new System.Drawing.Size(83, 17);
            this.rb_exportfile.TabIndex = 2;
            this.rb_exportfile.TabStop = true;
            this.rb_exportfile.Text = "Export to file";
            this.rb_exportfile.UseVisualStyleBackColor = true;
            // 
            // rb_exportclipboard
            // 
            this.rb_exportclipboard.AutoSize = true;
            this.rb_exportclipboard.Location = new System.Drawing.Point(12, 35);
            this.rb_exportclipboard.Name = "rb_exportclipboard";
            this.rb_exportclipboard.Size = new System.Drawing.Size(113, 17);
            this.rb_exportclipboard.TabIndex = 3;
            this.rb_exportclipboard.Text = "Export to clipboard";
            this.rb_exportclipboard.UseVisualStyleBackColor = true;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 198);
            this.Controls.Add(this.rb_exportclipboard);
            this.Controls.Add(this.rb_exportfile);
            this.Controls.Add(this.export2);
            this.Controls.Add(this.export1);
            this.Name = "Export";
            this.Text = "Exportieren";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button export1;
        private System.Windows.Forms.Button export2;
        private System.Windows.Forms.RadioButton rb_exportfile;
        private System.Windows.Forms.RadioButton rb_exportclipboard;
    }
}