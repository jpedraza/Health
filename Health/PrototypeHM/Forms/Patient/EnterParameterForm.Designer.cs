namespace PrototypeHM.Forms.Patient
{
    partial class EnterParameterForm
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
            this.statusPanel = new System.Windows.Forms.StatusStrip();
            this.toolPanel = new System.Windows.Forms.ToolStrip();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // statusPanel
            // 
            this.statusPanel.Location = new System.Drawing.Point(0, 510);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(696, 22);
            this.statusPanel.TabIndex = 0;
            this.statusPanel.Text = "statusStrip1";
            // 
            // toolPanel
            // 
            this.toolPanel.Location = new System.Drawing.Point(0, 0);
            this.toolPanel.Name = "toolPanel";
            this.toolPanel.Size = new System.Drawing.Size(696, 25);
            this.toolPanel.TabIndex = 1;
            this.toolPanel.Text = "toolStrip1";
            // 
            // layoutPanel
            // 
            this.layoutPanel.AutoScroll = true;
            this.layoutPanel.ColumnCount = 1;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.layoutPanel.Location = new System.Drawing.Point(0, 25);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 1;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutPanel.Size = new System.Drawing.Size(696, 485);
            this.layoutPanel.TabIndex = 2;
            // 
            // EnterParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 532);
            this.Controls.Add(this.layoutPanel);
            this.Controls.Add(this.toolPanel);
            this.Controls.Add(this.statusPanel);
            this.Name = "EnterParameterForm";
            this.Text = "Ввод параметров";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusPanel;
        private System.Windows.Forms.ToolStrip toolPanel;
        private System.Windows.Forms.TableLayoutPanel layoutPanel;

    }
}