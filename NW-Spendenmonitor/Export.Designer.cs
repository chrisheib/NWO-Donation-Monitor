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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_exportclipboard = new System.Windows.Forms.RadioButton();
            this.rb_exportfile = new System.Windows.Forms.RadioButton();
            this.rb_export_vouchers = new System.Windows.Forms.RadioButton();
            this.rb_export_curSel = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // export1
            // 
            this.export1.AutoSize = true;
            this.export1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.export1.Location = new System.Drawing.Point(0, 128);
            this.export1.Name = "export1";
            this.export1.Size = new System.Drawing.Size(173, 50);
            this.export1.TabIndex = 0;
            this.export1.Text = "Export!";
            this.export1.UseVisualStyleBackColor = true;
            this.export1.Click += new System.EventHandler(this.Btn_Export_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 0);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(173, 0);
            this.panel2.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_export_vouchers);
            this.groupBox2.Controls.Add(this.rb_export_curSel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 65);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_exportclipboard);
            this.groupBox1.Controls.Add(this.rb_exportfile);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 64);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target";
            // 
            // rb_exportclipboard
            // 
            this.rb_exportclipboard.AutoSize = true;
            this.rb_exportclipboard.Location = new System.Drawing.Point(12, 42);
            this.rb_exportclipboard.Name = "rb_exportclipboard";
            this.rb_exportclipboard.Size = new System.Drawing.Size(113, 17);
            this.rb_exportclipboard.TabIndex = 7;
            this.rb_exportclipboard.Text = "Export to clipboard";
            this.rb_exportclipboard.UseVisualStyleBackColor = true;
            // 
            // rb_exportfile
            // 
            this.rb_exportfile.AutoSize = true;
            this.rb_exportfile.Checked = true;
            this.rb_exportfile.Location = new System.Drawing.Point(12, 19);
            this.rb_exportfile.Name = "rb_exportfile";
            this.rb_exportfile.Size = new System.Drawing.Size(83, 17);
            this.rb_exportfile.TabIndex = 6;
            this.rb_exportfile.TabStop = true;
            this.rb_exportfile.Text = "Export to file";
            this.rb_exportfile.UseVisualStyleBackColor = true;
            // 
            // rb_export_vouchers
            // 
            this.rb_export_vouchers.AutoSize = true;
            this.rb_export_vouchers.Location = new System.Drawing.Point(12, 42);
            this.rb_export_vouchers.Name = "rb_export_vouchers";
            this.rb_export_vouchers.Size = new System.Drawing.Size(100, 17);
            this.rb_export_vouchers.TabIndex = 3;
            this.rb_export_vouchers.Text = "Siege Vouchers";
            this.rb_export_vouchers.UseVisualStyleBackColor = true;
            // 
            // rb_export_curSel
            // 
            this.rb_export_curSel.AutoSize = true;
            this.rb_export_curSel.Checked = true;
            this.rb_export_curSel.Location = new System.Drawing.Point(12, 19);
            this.rb_export_curSel.Name = "rb_export_curSel";
            this.rb_export_curSel.Size = new System.Drawing.Size(104, 17);
            this.rb_export_curSel.TabIndex = 2;
            this.rb_export_curSel.TabStop = true;
            this.rb_export_curSel.Text = "Current selection";
            this.rb_export_curSel.UseVisualStyleBackColor = true;
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 178);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.export1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Export";
            this.Text = "Exportieren";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button export1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_export_vouchers;
        private System.Windows.Forms.RadioButton rb_export_curSel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_exportclipboard;
        private System.Windows.Forms.RadioButton rb_exportfile;
    }
}