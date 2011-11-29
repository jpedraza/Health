namespace Prototype.Parameter.UserControls
{
    partial class EnumMetadataControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddAnswer = new System.Windows.Forms.ToolStripButton();
            this.tscbAnswerType = new System.Windows.Forms.ToolStripComboBox();
            this.pAnswerControls = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddAnswer,
            this.tscbAnswerType});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(606, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddAnswer
            // 
            this.tsbAddAnswer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddAnswer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddAnswer.Name = "tsbAddAnswer";
            this.tsbAddAnswer.Size = new System.Drawing.Size(95, 22);
            this.tsbAddAnswer.Text = "Добавить ответ";
            this.tsbAddAnswer.Click += new System.EventHandler(this.TsbAddAnswerClick);
            // 
            // tscbAnswerType
            // 
            this.tscbAnswerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbAnswerType.Items.AddRange(new object[] {
            "зависящий от возраста",
            "не зависящий от возраста"});
            this.tscbAnswerType.Name = "tscbAnswerType";
            this.tscbAnswerType.Size = new System.Drawing.Size(121, 25);
            this.tscbAnswerType.Text = "зависящий от возраста";
            // 
            // pAnswerControls
            // 
            this.pAnswerControls.AutoScroll = true;
            this.pAnswerControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pAnswerControls.Location = new System.Drawing.Point(0, 25);
            this.pAnswerControls.Name = "pAnswerControls";
            this.pAnswerControls.Size = new System.Drawing.Size(606, 619);
            this.pAnswerControls.TabIndex = 2;
            // 
            // EnumMetadataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pAnswerControls);
            this.Controls.Add(this.toolStrip1);
            this.Name = "EnumMetadataControl";
            this.Size = new System.Drawing.Size(606, 644);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddAnswer;
        private System.Windows.Forms.ToolStripComboBox tscbAnswerType;
        private System.Windows.Forms.Panel pAnswerControls;
    }
}
