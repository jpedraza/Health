namespace PrototypeHM.Components
{
    partial class YDataGridViewWithControl
    {
        /// <summary> 
        /// “ребуетс€ переменна€ конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// ќсвободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управл€емый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region  од, автоматически созданный конструктором компонентов

        /// <summary> 
        /// ќб€зательный метод дл€ поддержки конструктора - не измен€йте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsOperations = new System.Windows.Forms.ToolStrip();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.ydgvData = new YDataGridView();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // tsOperations
            // 
            this.tsOperations.Dock = System.Windows.Forms.DockStyle.None;
            this.tsOperations.Location = new System.Drawing.Point(3, 0);
            this.tsOperations.Name = "tsOperations";
            this.tsOperations.Size = new System.Drawing.Size(111, 25);
            this.tsOperations.TabIndex = 0;
            this.tsOperations.Text = "toolStrip1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.ydgvData);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(514, 389);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(514, 414);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsOperations);
            // 
            // ydgvData
            // 
            this.ydgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ydgvData.BackgroundColor = System.Drawing.Color.White;
            this.ydgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ydgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ydgvData.Location = new System.Drawing.Point(0, 0);
            this.ydgvData.Name = "ydgvData";
            this.ydgvData.Size = new System.Drawing.Size(514, 389);
            this.ydgvData.TabIndex = 0;
            // 
            // YDataGridViewWithControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "YDataGridViewWithControl";
            this.Size = new System.Drawing.Size(514, 414);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        internal YDataGridView ydgvData;
        internal System.Windows.Forms.ToolStrip tsOperations;
    }
}
