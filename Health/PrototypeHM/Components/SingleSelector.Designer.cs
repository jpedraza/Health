namespace PrototypeHM.Components
{
    partial class SingleSelector
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
            this.loadControl = new PrototypeHM.Components.LoadControl();
            this.ydgvCollection = new PrototypeHM.Components.YDataGridView();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvCollection)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbSelectedValue
            // 
            this.txbSelectedValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbSelectedValue.Enabled = false;
            this.txbSelectedValue.Location = new System.Drawing.Point(3, 3);
            this.txbSelectedValue.Name = "txbSelectedValue";
            this.txbSelectedValue.Size = new System.Drawing.Size(253, 20);
            this.txbSelectedValue.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelect.Location = new System.Drawing.Point(262, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(94, 19);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "¬ыбрать";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.ExpandClick);
            // 
            // loadControl
            // 
            this.loadControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadControl.Location = new System.Drawing.Point(0, 0);
            this.loadControl.Margin = new System.Windows.Forms.Padding(7);
            this.loadControl.Name = "loadControl";
            this.loadControl.Size = new System.Drawing.Size(353, 355);
            this.loadControl.TabIndex = 3;
            this.loadControl.Visible = false;
            // 
            // ydgvCollection
            // 
            this.ydgvCollection.AllowUserToAddRows = false;
            this.ydgvCollection.AllowUserToDeleteRows = false;
            this.ydgvCollection.AllowUserToResizeColumns = false;
            this.ydgvCollection.AllowUserToResizeRows = false;
            this.ydgvCollection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ydgvCollection.BackgroundColor = System.Drawing.Color.White;
            this.ydgvCollection.BindingSource = null;
            this.ydgvCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ydgvCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ydgvCollection.Location = new System.Drawing.Point(0, 0);
            this.ydgvCollection.Margin = new System.Windows.Forms.Padding(4);
            this.ydgvCollection.Name = "ydgvCollection";
            this.ydgvCollection.ReadOnly = true;
            this.ydgvCollection.Size = new System.Drawing.Size(353, 355);
            this.ydgvCollection.TabIndex = 2;
            this.ydgvCollection.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.YdgvCollectionCellDoubleClick);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel.Controls.Add(this.txbSelectedValue, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnSelect, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.panel, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(359, 386);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // panel
            // 
            this.tableLayoutPanel.SetColumnSpan(this.panel, 2);
            this.panel.Controls.Add(this.loadControl);
            this.panel.Controls.Add(this.ydgvCollection);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 28);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(353, 355);
            this.panel.TabIndex = 2;
            // 
            // SingleSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "SingleSelector";
            this.Size = new System.Drawing.Size(359, 386);
            ((System.ComponentModel.ISupportInitialize)(this.ydgvCollection)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private YDataGridView ydgvCollection;
        public System.Windows.Forms.TextBox txbSelectedValue;
        private LoadControl loadControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel;
    }
}
