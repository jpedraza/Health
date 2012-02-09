namespace PrototypeHM.Components
{
    partial class DinamicCollection
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgvMetaData = new YDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetaData)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Добавить новый";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(185, 201);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Удалить выдел.";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dgvMetaData
            // 
            this.dgvMetaData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMetaData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMetaData.Location = new System.Drawing.Point(12, 3);
            this.dgvMetaData.Name = "dgvMetaData";
            this.dgvMetaData.RowHeadersVisible = false;
            this.dgvMetaData.Size = new System.Drawing.Size(295, 169);
            this.dgvMetaData.TabIndex = 2;
            // 
            // DinamicCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.dgvMetaData);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "DinamicCollection";
            this.Size = new System.Drawing.Size(322, 242);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetaData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public YDataGridView dgvMetaData;


    }
}
