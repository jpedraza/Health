namespace PrototypeHM.Forms
{
    partial class ListForm
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
            this.ydgvList = new PrototypeHM.Components.YDataGridView();
            this.toolPanel = new System.Windows.Forms.ToolStrip();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.loadControl = new PrototypeHM.Components.LoadControl();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvList)).BeginInit();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ydgvList
            // 
            this.ydgvList.AllowUserToAddRows = false;
            this.ydgvList.AllowUserToDeleteRows = false;
            this.ydgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ydgvList.BackgroundColor = System.Drawing.Color.White;
            this.ydgvList.BindingSource = null;
            this.ydgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ydgvList.Delete = null;
            this.ydgvList.Detail = null;
            this.ydgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ydgvList.Location = new System.Drawing.Point(0, 0);
            this.ydgvList.Name = "ydgvList";
            this.ydgvList.ReadOnly = true;
            this.ydgvList.Size = new System.Drawing.Size(942, 575);
            this.ydgvList.TabIndex = 0;
            // 
            // toolPanel
            // 
            this.toolPanel.Dock = System.Windows.Forms.DockStyle.None;
            this.toolPanel.Location = new System.Drawing.Point(5, 0);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(111, 25);
            this.toolPanel.TabIndex = 1;
            this.toolPanel.Text = "toolStrip1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.loadControl);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ydgvList);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(942, 575);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(942, 600);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolPanel);
            // 
            // loadControl
            // 
            this.loadControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadControl.Location = new System.Drawing.Point(0, 0);
            this.loadControl.Name = "loadControl";
            this.loadControl.Size = new System.Drawing.Size(942, 575);
            this.loadControl.TabIndex = 1;
            this.loadControl.Visible = false;
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 600);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ListForm";
            this.ShowIcon = false;
            this.Text = "ListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ListFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ydgvList)).EndInit();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Components.YDataGridView ydgvList;
        private System.Windows.Forms.ToolStrip toolPanel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private Components.LoadControl loadControl;
    }
}