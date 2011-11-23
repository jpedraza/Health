namespace Prototype.Parameter.UserControls
{
    partial class AgeDependsAnswerControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.nupdMinimalAge = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nupdMaximalAge = new System.Windows.Forms.NumericUpDown();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdMinimalAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdMaximalAge)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 119);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.nupdMaximalAge);
            this.mainPanel.Controls.Add(this.label5);
            this.mainPanel.Controls.Add(this.label4);
            this.mainPanel.Controls.Add(this.nupdMinimalAge);
            this.mainPanel.Size = new System.Drawing.Size(482, 291);
            this.mainPanel.Controls.SetChildIndex(this.txtDisplayValue, 0);
            this.mainPanel.Controls.SetChildIndex(this.nupdMinimalAge, 0);
            this.mainPanel.Controls.SetChildIndex(this.label4, 0);
            this.mainPanel.Controls.SetChildIndex(this.label5, 0);
            this.mainPanel.Controls.SetChildIndex(this.label2, 0);
            this.mainPanel.Controls.SetChildIndex(this.txtDescription, 0);
            this.mainPanel.Controls.SetChildIndex(this.nupdMaximalAge, 0);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(103, 119);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Minimal age";
            // 
            // nupdMinimalAge
            // 
            this.nupdMinimalAge.Location = new System.Drawing.Point(103, 81);
            this.nupdMinimalAge.Name = "nupdMinimalAge";
            this.nupdMinimalAge.Size = new System.Drawing.Size(120, 20);
            this.nupdMinimalAge.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Maximal age";
            // 
            // nupdMaximalAge
            // 
            this.nupdMaximalAge.Location = new System.Drawing.Point(323, 81);
            this.nupdMaximalAge.Name = "nupdMaximalAge";
            this.nupdMaximalAge.Size = new System.Drawing.Size(120, 20);
            this.nupdMaximalAge.TabIndex = 9;
            // 
            // AgeDependsAnswerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AgeDependsAnswerControl";
            this.Size = new System.Drawing.Size(482, 316);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupdMinimalAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupdMaximalAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nupdMinimalAge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nupdMaximalAge;
    }
}
