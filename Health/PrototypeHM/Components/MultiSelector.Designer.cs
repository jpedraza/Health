namespace PrototypeHM.Components
{
    partial class MultiSelector
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
            this.btnToRight = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.btnToLeft = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDisplayMode = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.ydgvLeft = new PrototypeHM.Components.YDataGridView();
            this.ydgvRight = new PrototypeHM.Components.YDataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvRight)).BeginInit();
            this.SuspendLayout();
            // 
            // btnToRight
            // 
            this.btnToRight.Enabled = false;
            this.btnToRight.Location = new System.Drawing.Point(24, 2);
            this.btnToRight.Name = "btnToRight";
            this.btnToRight.Size = new System.Drawing.Size(50, 25);
            this.btnToRight.TabIndex = 2;
            this.btnToRight.Text = ">";
            this.btnToRight.UseVisualStyleBackColor = true;
            this.btnToRight.Click += new System.EventHandler(this.BtnToRightClick);
            // 
            // btnSwap
            // 
            this.btnSwap.Enabled = false;
            this.btnSwap.Location = new System.Drawing.Point(80, 2);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(50, 25);
            this.btnSwap.TabIndex = 3;
            this.btnSwap.Text = "<>";
            this.btnSwap.UseVisualStyleBackColor = true;
            // 
            // btnToLeft
            // 
            this.btnToLeft.Enabled = false;
            this.btnToLeft.Location = new System.Drawing.Point(136, 2);
            this.btnToLeft.Name = "btnToLeft";
            this.btnToLeft.Size = new System.Drawing.Size(50, 25);
            this.btnToLeft.TabIndex = 4;
            this.btnToLeft.Text = "<";
            this.btnToLeft.UseVisualStyleBackColor = true;
            this.btnToLeft.Click += new System.EventHandler(this.BtnToLeftClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.controlPanel);
            this.panel1.Controls.Add(this.btnDisplayMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 36);
            this.panel1.TabIndex = 5;
            // 
            // btnDisplayMode
            // 
            this.btnDisplayMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisplayMode.Location = new System.Drawing.Point(3, 6);
            this.btnDisplayMode.Name = "btnDisplayMode";
            this.btnDisplayMode.Size = new System.Drawing.Size(80, 25);
            this.btnDisplayMode.TabIndex = 6;
            this.btnDisplayMode.Text = "Show/Hide";
            this.btnDisplayMode.UseVisualStyleBackColor = true;
            this.btnDisplayMode.Click += new System.EventHandler(this.BtnDisplayModeClick);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(2, 37);
            this.splitContainer.MinimumSize = new System.Drawing.Size(0, 350);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.ydgvLeft);
            this.splitContainer.Panel1MinSize = 0;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.ydgvRight);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(397, 360);
            this.splitContainer.SplitterDistance = 493;
            this.splitContainer.TabIndex = 6;
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.Controls.Add(this.btnSwap);
            this.controlPanel.Controls.Add(this.btnToRight);
            this.controlPanel.Controls.Add(this.btnToLeft);
            this.controlPanel.Location = new System.Drawing.Point(183, 4);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(214, 29);
            this.controlPanel.TabIndex = 7;
            this.controlPanel.Visible = false;
            // 
            // ydgvLeft
            // 
            this.ydgvLeft.AllowUserToAddRows = false;
            this.ydgvLeft.AllowUserToDeleteRows = false;
            this.ydgvLeft.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ydgvLeft.BackgroundColor = System.Drawing.Color.White;
            this.ydgvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ydgvLeft.Delete = null;
            this.ydgvLeft.Detail = null;
            this.ydgvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ydgvLeft.Location = new System.Drawing.Point(0, 0);
            this.ydgvLeft.MinimumSize = new System.Drawing.Size(200, 200);
            this.ydgvLeft.Name = "ydgvLeft";
            this.ydgvLeft.ReadOnly = true;
            this.ydgvLeft.RowHeadersVisible = false;
            this.ydgvLeft.Size = new System.Drawing.Size(397, 360);
            this.ydgvLeft.TabIndex = 0;
            // 
            // ydgvRight
            // 
            this.ydgvRight.AllowUserToAddRows = false;
            this.ydgvRight.AllowUserToDeleteRows = false;
            this.ydgvRight.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ydgvRight.BackgroundColor = System.Drawing.Color.White;
            this.ydgvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ydgvRight.Delete = null;
            this.ydgvRight.Detail = null;
            this.ydgvRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ydgvRight.Location = new System.Drawing.Point(0, 0);
            this.ydgvRight.MinimumSize = new System.Drawing.Size(200, 200);
            this.ydgvRight.Name = "ydgvRight";
            this.ydgvRight.ReadOnly = true;
            this.ydgvRight.RowHeadersVisible = false;
            this.ydgvRight.Size = new System.Drawing.Size(505, 546);
            this.ydgvRight.TabIndex = 1;
            // 
            // MultiSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "MultiSelector";
            this.Size = new System.Drawing.Size(400, 400);
            this.panel1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ydgvLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private YDataGridView ydgvLeft;
        private YDataGridView ydgvRight;
        private System.Windows.Forms.Button btnToRight;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Button btnToLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDisplayMode;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
