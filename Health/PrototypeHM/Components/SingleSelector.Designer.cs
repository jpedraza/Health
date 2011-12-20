namespace PrototypeHM.Components
{
    partial class SingleSelector<T>
    {
        /// <summary> 
        /// “ребуетс€ переменна€ конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// ќсвободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управл€емый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region  од, автоматически созданный конструктором компонентов

        /// <summary> 
        /// ќб€зательный метод дл€ поддержки конструктора - не измен€йте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.txbSelectedValue = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.ydgvCollection = new PrototypeHM.Components.YDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // txbSelectedValue
            // 
            this.txbSelectedValue.Enabled = false;
            this.txbSelectedValue.Location = new System.Drawing.Point(0, 0);
            this.txbSelectedValue.Name = "txbSelectedValue";
            this.txbSelectedValue.Size = new System.Drawing.Size(200, 20);
            this.txbSelectedValue.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(200, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 21);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "¬ыбрать";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.BtnSelectClick);
            // 
            // ydgvCollection
            // 
            this.ydgvCollection.AllowUserToAddRows = false;
            this.ydgvCollection.AllowUserToDeleteRows = false;
            this.ydgvCollection.AllowUserToResizeColumns = false;
            this.ydgvCollection.AllowUserToResizeRows = false;
            this.ydgvCollection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ydgvCollection.BackgroundColor = System.Drawing.Color.White;
            this.ydgvCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ydgvCollection.Delete = null;
            this.ydgvCollection.Detail = null;
            this.ydgvCollection.Location = new System.Drawing.Point(0, 21);
            this.ydgvCollection.Name = "ydgvCollection";
            this.ydgvCollection.ReadOnly = true;
            this.ydgvCollection.Size = new System.Drawing.Size(398, 378);
            this.ydgvCollection.TabIndex = 2;
            this.ydgvCollection.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.YdgvCollectionCellDoubleClick);
            // 
            // SingleSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ydgvCollection);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txbSelectedValue);
            this.Name = "SingleSelector";
            this.Size = new System.Drawing.Size(275, 20);
            ((System.ComponentModel.ISupportInitialize)(this.ydgvCollection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private YDataGridView ydgvCollection;
        public System.Windows.Forms.TextBox txbSelectedValue;
    }
}
