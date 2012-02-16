namespace PrototypeHM.Forms.EntitysForm.ParameterForms.ValueTypes
{
    partial class ListForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ValueTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameValueType2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddValueTypes = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 208);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Все типы метаданных";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValueTypeId,
            this.NameValueType2});
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(513, 168);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // ValueTypeId
            // 
            this.ValueTypeId.HeaderText = "Id";
            this.ValueTypeId.Name = "ValueTypeId";
            this.ValueTypeId.ReadOnly = true;
            // 
            // NameValueType2
            // 
            this.NameValueType2.HeaderText = "Название типа";
            this.NameValueType2.Name = "NameValueType2";
            this.NameValueType2.ReadOnly = true;
            this.NameValueType2.Width = 400;
            // 
            // AddValueTypes
            // 
            this.AddValueTypes.Location = new System.Drawing.Point(17, 56);
            this.AddValueTypes.Name = "AddValueTypes";
            this.AddValueTypes.Size = new System.Drawing.Size(130, 23);
            this.AddValueTypes.TabIndex = 0;
            this.AddValueTypes.Text = "Добавить новый тип";
            this.AddValueTypes.UseVisualStyleBackColor = true;
            this.AddValueTypes.Click += new System.EventHandler(this.AddValueTypes_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(402, 56);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Закрыть";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(156, 56);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 2;
            this.UpdateButton.Text = "Обновить";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.UpdateButton);
            this.groupBox2.Controls.Add(this.CloseButton);
            this.groupBox2.Controls.Add(this.AddValueTypes);
            this.groupBox2.Location = new System.Drawing.Point(13, 227);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 102);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Действия";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.OliveDrab;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Для редактирования, либо удаления типа,\r\n нажмите правой кнопкой мыши по соответс" +
                "твующей строке.";
            // 
            // ListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 341);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListForm";
            this.Text = "Работа с типами метаданных параметра";
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameValueType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameValueType2;
        private System.Windows.Forms.Button AddValueTypes;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
    }
}