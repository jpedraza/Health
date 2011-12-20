using PrototypeHM.Components;

namespace PrototypeHM.Forms
{
    partial class ListForm<TData>
        where TData : class
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
            this.ydgvwc = new YDataGridViewWithControl();
            this.SuspendLayout();
            // 
            // ydgvwc
            // 
            this.ydgvwc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ydgvwc.Location = new System.Drawing.Point(0, 0);
            this.ydgvwc.Name = "ydgvwc";
            this.ydgvwc.Size = new System.Drawing.Size(778, 536);
            this.ydgvwc.TabIndex = 0;
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 536);
            this.Controls.Add(this.ydgvwc);
            this.Name = "ListForm";
            this.Text = "ListForm";
            this.Load += new System.EventHandler(this.ListFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private YDataGridViewWithControl ydgvwc;


    }
}