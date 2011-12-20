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
            this.ydgvRight = new PrototypeHM.Components.YDataGridView();
            this.ydgvLeft = new PrototypeHM.Components.YDataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // btnToRight
            // 
            this.btnToRight.Location = new System.Drawing.Point(28, 113);
            this.btnToRight.Name = "btnToRight";
            this.btnToRight.Size = new System.Drawing.Size(75, 23);
            this.btnToRight.TabIndex = 2;
            this.btnToRight.Text = ">";
            this.btnToRight.UseVisualStyleBackColor = true;
            this.btnToRight.Click += new System.EventHandler(this.BtnToRightClick);
            // 
            // btnSwap
            // 
            this.btnSwap.Location = new System.Drawing.Point(28, 142);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(75, 23);
            this.btnSwap.TabIndex = 3;
            this.btnSwap.Text = "<>";
            this.btnSwap.UseVisualStyleBackColor = true;
            // 
            // btnToLeft
            // 
            this.btnToLeft.Location = new System.Drawing.Point(28, 171);
            this.btnToLeft.Name = "btnToLeft";
            this.btnToLeft.Size = new System.Drawing.Size(75, 23);
            this.btnToLeft.TabIndex = 4;
            this.btnToLeft.Text = "<";
            this.btnToLeft.UseVisualStyleBackColor = true;
            this.btnToLeft.Click += new System.EventHandler(this.BtnToLeftClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSwap);
            this.panel1.Controls.Add(this.btnToLeft);
            this.panel1.Controls.Add(this.btnToRight);
            this.panel1.Location = new System.Drawing.Point(300, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(100, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(125, 400);
            this.panel1.TabIndex = 5;
            // 
            // btnDisplayMode
            // 
            this.btnDisplayMode.Location = new System.Drawing.Point(2, 374);
            this.btnDisplayMode.Name = "btnDisplayMode";
            this.btnDisplayMode.Size = new System.Drawing.Size(298, 23);
            this.btnDisplayMode.TabIndex = 6;
            this.btnDisplayMode.Text = "Show/Hide";
            this.btnDisplayMode.UseVisualStyleBackColor = true;
            this.btnDisplayMode.Click += new System.EventHandler(this.BtnDisplayModeClick);
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
            this.ydgvRight.Location = new System.Drawing.Point(425, 0);
            this.ydgvRight.MinimumSize = new System.Drawing.Size(200, 200);
            this.ydgvRight.Name = "ydgvRight";
            this.ydgvRight.ReadOnly = true;
            this.ydgvRight.Size = new System.Drawing.Size(300, 400);
            this.ydgvRight.TabIndex = 1;
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
            this.ydgvLeft.Location = new System.Drawing.Point(0, 0);
            this.ydgvLeft.MinimumSize = new System.Drawing.Size(200, 200);
            this.ydgvLeft.Name = "ydgvLeft";
            this.ydgvLeft.ReadOnly = true;
            this.ydgvLeft.Size = new System.Drawing.Size(300, 374);
            this.ydgvLeft.TabIndex = 0;
            // 
            // MultiSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDisplayMode);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ydgvRight);
            this.Controls.Add(this.ydgvLeft);
            this.Name = "MultiSelector";
            this.Size = new System.Drawing.Size(300, 400);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ydgvRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ydgvLeft)).EndInit();
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
    }
}
