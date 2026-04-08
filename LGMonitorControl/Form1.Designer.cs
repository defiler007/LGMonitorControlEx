namespace LGMonitorControl
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.WindowName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.checkBox_Autostart = new System.Windows.Forms.CheckBox();
            this.checkBox_Minimized = new System.Windows.Forms.CheckBox();
            this.comboBox_Defaultmode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "LGMonitorControl";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WindowName,
            this.Mode});
            this.dataGridView1.Location = new System.Drawing.Point(18, 60);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(610, 424);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            // 
            // WindowName
            // 
            this.WindowName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WindowName.HeaderText = "Window Name";
            this.WindowName.MinimumWidth = 8;
            this.WindowName.Name = "WindowName";
            // 
            // Mode
            // 
            this.Mode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Mode.HeaderText = "Mode";
            this.Mode.MinimumWidth = 100;
            this.Mode.Name = "Mode";
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.Location = new System.Drawing.Point(129, 494);
            this.addButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(150, 38);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add Application";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Location = new System.Drawing.Point(332, 494);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(150, 38);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // checkBox_Autostart
            // 
            this.checkBox_Autostart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Autostart.AutoSize = true;
            this.checkBox_Autostart.Location = new System.Drawing.Point(376, 604);
            this.checkBox_Autostart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Autostart.Name = "checkBox_Autostart";
            this.checkBox_Autostart.Size = new System.Drawing.Size(101, 24);
            this.checkBox_Autostart.TabIndex = 4;
            this.checkBox_Autostart.Text = "Autostart";
            this.checkBox_Autostart.UseVisualStyleBackColor = true;
            this.checkBox_Autostart.CheckedChanged += new System.EventHandler(this.checkBox_Autostart_CheckedChanged);
            // 
            // checkBox_Minimized
            // 
            this.checkBox_Minimized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_Minimized.AutoSize = true;
            this.checkBox_Minimized.Location = new System.Drawing.Point(486, 604);
            this.checkBox_Minimized.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox_Minimized.Name = "checkBox_Minimized";
            this.checkBox_Minimized.Size = new System.Drawing.Size(144, 24);
            this.checkBox_Minimized.TabIndex = 5;
            this.checkBox_Minimized.Text = "Start Minimized";
            this.checkBox_Minimized.UseVisualStyleBackColor = true;
            this.checkBox_Minimized.CheckedChanged += new System.EventHandler(this.checkBox_Minimized_CheckedChanged);
            // 
            // comboBox_Defaultmode
            // 
            this.comboBox_Defaultmode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_Defaultmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Defaultmode.FormattingEnabled = true;
            this.comboBox_Defaultmode.Location = new System.Drawing.Point(129, 601);
            this.comboBox_Defaultmode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox_Defaultmode.Name = "comboBox_Defaultmode";
            this.comboBox_Defaultmode.Size = new System.Drawing.Size(180, 28);
            this.comboBox_Defaultmode.TabIndex = 6;
            this.comboBox_Defaultmode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_Defaultmode_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 606);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Default Mode";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 644);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Defaultmode);
            this.Controls.Add(this.checkBox_Minimized);
            this.Controls.Add(this.checkBox_Autostart);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 500);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LGMonitorControl Extended";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.CheckBox checkBox_Autostart;
        private System.Windows.Forms.CheckBox checkBox_Minimized;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowName;
        private System.Windows.Forms.DataGridViewComboBoxColumn Mode;
        private System.Windows.Forms.ComboBox comboBox_Defaultmode;
        private System.Windows.Forms.Label label1;
    }
}

