namespace Prototype.Forms
{
    partial class ParametersEditForm
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
            this.cbParameterType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbParameterType
            // 
            this.cbParameterType.FormattingEnabled = true;
            this.cbParameterType.Items.AddRange(new object[] {
            "Текстовый",
            "Числовой"});
            this.cbParameterType.Location = new System.Drawing.Point(149, 23);
            this.cbParameterType.Name = "cbParameterType";
            this.cbParameterType.Size = new System.Drawing.Size(121, 21);
            this.cbParameterType.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип данных параметра\r\n";
            // 
            // ParametersEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 652);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbParameterType);
            this.Name = "ParametersEditForm";
            this.Text = "ParametersEditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbParameterType;
        private System.Windows.Forms.Label label1;
    }
}