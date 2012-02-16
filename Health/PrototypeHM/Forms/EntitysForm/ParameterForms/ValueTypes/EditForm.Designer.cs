namespace PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes
{
    partial class EditForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveClickButton = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CloseButton);
            this.groupBox1.Controls.Add(this.SaveClickButton);
            this.groupBox1.Controls.Add(this.NameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Укажите название";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(18, 134);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Отмена";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // SaveClickButton
            // 
            this.SaveClickButton.Location = new System.Drawing.Point(233, 134);
            this.SaveClickButton.Name = "SaveClickButton";
            this.SaveClickButton.Size = new System.Drawing.Size(75, 23);
            this.SaveClickButton.TabIndex = 1;
            this.SaveClickButton.Text = "Сохранить";
            this.SaveClickButton.UseVisualStyleBackColor = true;
            this.SaveClickButton.Click += new System.EventHandler(this.SaveClickButtonClick);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(18, 28);
            this.NameTextBox.Multiline = true;
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.NameTextBox.Size = new System.Drawing.Size(290, 76);
            this.NameTextBox.TabIndex = 0;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 226);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditForm";
            this.Text = "Создание нового типа мета-данных";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button SaveClickButton;
        private System.Windows.Forms.TextBox NameTextBox;
    }
}