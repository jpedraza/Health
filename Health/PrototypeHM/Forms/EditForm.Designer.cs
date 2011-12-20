namespace PrototypeHM.Forms
{
    partial class AddForm<TData>
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
            this.tsOperations = new System.Windows.Forms.ToolStrip();
            this.tscContent = new System.Windows.Forms.ToolStripContainer();
            this.tscContent.TopToolStripPanel.SuspendLayout();
            this.tscContent.SuspendLayout();
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
            // tscContent
            // 
            // 
            // tscContent.ContentPanel
            // 
            this.tscContent.ContentPanel.Size = new System.Drawing.Size(622, 505);
            this.tscContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscContent.Location = new System.Drawing.Point(0, 0);
            this.tscContent.Name = "tscContent";
            this.tscContent.Size = new System.Drawing.Size(622, 530);
            this.tscContent.TabIndex = 1;
            this.tscContent.Text = "toolStripContainer1";
            // 
            // tscContent.TopToolStripPanel
            // 
            this.tscContent.TopToolStripPanel.Controls.Add(this.tsOperations);
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 530);
            this.Controls.Add(this.tscContent);
            this.Name = "AddForm";
            this.Text = "EditForm";
            this.tscContent.TopToolStripPanel.ResumeLayout(false);
            this.tscContent.TopToolStripPanel.PerformLayout();
            this.tscContent.ResumeLayout(false);
            this.tscContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsOperations;
        private System.Windows.Forms.ToolStripContainer tscContent;
    }
}