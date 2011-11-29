namespace Prototype.Parameter.UserControls
{
    partial class AnswerControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDisplayValue = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbAnswerValueType = new System.Windows.Forms.ToolStripComboBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pAnswerValue = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display value";
            // 
            // txtDisplayValue
            // 
            this.txtDisplayValue.Location = new System.Drawing.Point(103, 19);
            this.txtDisplayValue.Name = "txtDisplayValue";
            this.txtDisplayValue.Size = new System.Drawing.Size(100, 20);
            this.txtDisplayValue.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbAnswerValueType});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(478, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel1.Text = "Тип значения";
            // 
            // tscbAnswerValueType
            // 
            this.tscbAnswerValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbAnswerValueType.Items.AddRange(new object[] {
            "Число",
            "Строка"});
            this.tscbAnswerValueType.Name = "tscbAnswerValueType";
            this.tscbAnswerValueType.Size = new System.Drawing.Size(121, 25);
            this.tscbAnswerValueType.SelectedIndexChanged += new System.EventHandler(this.TscbAnswerValueTypeSelectedIndexChanged);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.pAnswerValue);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.txtDescription);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.txtDisplayValue);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 25);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(478, 251);
            this.mainPanel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Value";
            // 
            // pAnswerValue
            // 
            this.pAnswerValue.Location = new System.Drawing.Point(250, 19);
            this.pAnswerValue.Name = "pAnswerValue";
            this.pAnswerValue.Size = new System.Drawing.Size(212, 54);
            this.pAnswerValue.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(103, 79);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(359, 156);
            this.txtDescription.TabIndex = 2;
            // 
            // AnswerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AnswerControl";
            this.Size = new System.Drawing.Size(478, 276);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel pAnswerValue;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Panel mainPanel;
        protected System.Windows.Forms.TextBox txtDescription;
        protected System.Windows.Forms.TextBox txtDisplayValue;
        protected System.Windows.Forms.ToolStripComboBox tscbAnswerValueType;
    }
}
