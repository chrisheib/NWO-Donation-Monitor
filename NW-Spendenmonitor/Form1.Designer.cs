namespace NW_Spendenmonitor
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_export = new System.Windows.Forms.Button();
            this.cb_uilanguage = new System.Windows.Forms.ComboBox();
            this.lbl_uilanguage = new System.Windows.Forms.Label();
            this.lbl_importlanguage = new System.Windows.Forms.Label();
            this.cb_importlanguage = new System.Windows.Forms.ComboBox();
            this.btn_sqlhistory = new System.Windows.Forms.CheckBox();
            this.cb_statistic = new System.Windows.Forms.ComboBox();
            this.chk_rename = new System.Windows.Forms.CheckBox();
            this.lbl_status = new System.Windows.Forms.Label();
            this.dTPTo = new System.Windows.Forms.DateTimePicker();
            this.dTPFrom = new System.Windows.Forms.DateTimePicker();
            this.btn_import = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_export2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_export2);
            this.splitContainer1.Panel1.Controls.Add(this.btn_export);
            this.splitContainer1.Panel1.Controls.Add(this.cb_uilanguage);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_uilanguage);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_importlanguage);
            this.splitContainer1.Panel1.Controls.Add(this.cb_importlanguage);
            this.splitContainer1.Panel1.Controls.Add(this.btn_sqlhistory);
            this.splitContainer1.Panel1.Controls.Add(this.cb_statistic);
            this.splitContainer1.Panel1.Controls.Add(this.chk_rename);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_status);
            this.splitContainer1.Panel1.Controls.Add(this.dTPTo);
            this.splitContainer1.Panel1.Controls.Add(this.dTPFrom);
            this.splitContainer1.Panel1.Controls.Add(this.btn_import);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(723, 761);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 8;
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(618, 62);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(45, 23);
            this.btn_export.TabIndex = 26;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.Btn_export_Click);
            // 
            // cb_uilanguage
            // 
            this.cb_uilanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_uilanguage.FormattingEnabled = true;
            this.cb_uilanguage.Items.AddRange(new object[] {
            "Englisch",
            "Deutsch"});
            this.cb_uilanguage.Location = new System.Drawing.Point(94, 36);
            this.cb_uilanguage.Name = "cb_uilanguage";
            this.cb_uilanguage.Size = new System.Drawing.Size(121, 21);
            this.cb_uilanguage.TabIndex = 25;
            // 
            // lbl_uilanguage
            // 
            this.lbl_uilanguage.AutoSize = true;
            this.lbl_uilanguage.Location = new System.Drawing.Point(9, 41);
            this.lbl_uilanguage.Name = "lbl_uilanguage";
            this.lbl_uilanguage.Size = new System.Drawing.Size(72, 13);
            this.lbl_uilanguage.TabIndex = 24;
            this.lbl_uilanguage.Text = "UI Language:";
            // 
            // lbl_importlanguage
            // 
            this.lbl_importlanguage.AutoSize = true;
            this.lbl_importlanguage.Location = new System.Drawing.Point(255, 40);
            this.lbl_importlanguage.Name = "lbl_importlanguage";
            this.lbl_importlanguage.Size = new System.Drawing.Size(71, 13);
            this.lbl_importlanguage.TabIndex = 23;
            this.lbl_importlanguage.Text = "Spielsprache:";
            // 
            // cb_importlanguage
            // 
            this.cb_importlanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_importlanguage.FormattingEnabled = true;
            this.cb_importlanguage.Items.AddRange(new object[] {
            "Englisch",
            "Deutsch"});
            this.cb_importlanguage.Location = new System.Drawing.Point(345, 36);
            this.cb_importlanguage.Name = "cb_importlanguage";
            this.cb_importlanguage.Size = new System.Drawing.Size(121, 21);
            this.cb_importlanguage.TabIndex = 22;
            // 
            // btn_sqlhistory
            // 
            this.btn_sqlhistory.Appearance = System.Windows.Forms.Appearance.Button;
            this.btn_sqlhistory.AutoSize = true;
            this.btn_sqlhistory.Location = new System.Drawing.Point(12, 62);
            this.btn_sqlhistory.Name = "btn_sqlhistory";
            this.btn_sqlhistory.Size = new System.Drawing.Size(76, 23);
            this.btn_sqlhistory.TabIndex = 21;
            this.btn_sqlhistory.Text = "SQL-Historie";
            this.btn_sqlhistory.UseVisualStyleBackColor = true;
            this.btn_sqlhistory.CheckedChanged += new System.EventHandler(this.CheckBox2_CheckedChanged);
            // 
            // cb_statistic
            // 
            this.cb_statistic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_statistic.FormattingEnabled = true;
            this.cb_statistic.Location = new System.Drawing.Point(378, 63);
            this.cb_statistic.Name = "cb_statistic";
            this.cb_statistic.Size = new System.Drawing.Size(232, 21);
            this.cb_statistic.TabIndex = 20;
            // 
            // chk_rename
            // 
            this.chk_rename.AutoSize = true;
            this.chk_rename.Checked = true;
            this.chk_rename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_rename.Location = new System.Drawing.Point(485, 39);
            this.chk_rename.Name = "chk_rename";
            this.chk_rename.Size = new System.Drawing.Size(116, 17);
            this.chk_rename.TabIndex = 19;
            this.chk_rename.Text = "Datei umbenennen";
            this.chk_rename.UseVisualStyleBackColor = true;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(9, 16);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(40, 13);
            this.lbl_status.TabIndex = 18;
            this.lbl_status.Text = "Status:";
            // 
            // dTPTo
            // 
            this.dTPTo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPTo.Location = new System.Drawing.Point(236, 63);
            this.dTPTo.Name = "dTPTo";
            this.dTPTo.Size = new System.Drawing.Size(136, 20);
            this.dTPTo.TabIndex = 16;
            this.dTPTo.Value = new System.DateTime(2018, 1, 6, 23, 59, 59, 0);
            // 
            // dTPFrom
            // 
            this.dTPFrom.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPFrom.Location = new System.Drawing.Point(94, 63);
            this.dTPFrom.Name = "dTPFrom";
            this.dTPFrom.Size = new System.Drawing.Size(136, 20);
            this.dTPFrom.TabIndex = 15;
            this.dTPFrom.Value = new System.DateTime(2018, 1, 6, 0, 0, 0, 0);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(618, 35);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(92, 23);
            this.btn_import.TabIndex = 14;
            this.btn_import.Text = "Import";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.Button3_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(55, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(655, 20);
            this.textBox2.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(91, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Abfrage";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Befehl";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 145);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(698, 95);
            this.listBox1.TabIndex = 10;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBox1_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "SQL:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(56, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(655, 20);
            this.textBox1.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(723, 507);
            this.dataGridView1.TabIndex = 6;
            // 
            // btn_export2
            // 
            this.btn_export2.Location = new System.Drawing.Point(665, 62);
            this.btn_export2.Name = "btn_export2";
            this.btn_export2.Size = new System.Drawing.Size(45, 23);
            this.btn_export2.TabIndex = 27;
            this.btn_export2.Text = "Ex. 2";
            this.btn_export2.UseVisualStyleBackColor = true;
            this.btn_export2.Click += new System.EventHandler(this.Btn_export2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 761);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Neverwinter Spendenmonitor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dTPTo;
        private System.Windows.Forms.DateTimePicker dTPFrom;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.CheckBox chk_rename;
        private System.Windows.Forms.ComboBox cb_statistic;
        private System.Windows.Forms.CheckBox btn_sqlhistory;
        private System.Windows.Forms.Label lbl_importlanguage;
        private System.Windows.Forms.ComboBox cb_importlanguage;
        private System.Windows.Forms.ComboBox cb_uilanguage;
        private System.Windows.Forms.Label lbl_uilanguage;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_export2;
    }
}

