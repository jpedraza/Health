namespace PrototypeHM.Forms
{
    partial class DIMainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDoctors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiagnosis = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыЗдоровьяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметрыЗдоровьяToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.метаданныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.типыМетаданныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пациентуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вводПараметровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsMenu,
            this.списикиToolStripMenuItem,
            this.пациентуToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1072, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(47, 20);
            this.windowsMenu.Text = "&Окна";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.cascadeToolStripMenuItem.Text = "&Каскадом";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItemClick);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.tileVerticalToolStripMenuItem.Text = "С&лева направо";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItemClick);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.tileHorizontalToolStripMenuItem.Text = "С&верху вниз";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItemClick);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.closeAllToolStripMenuItem.Text = "&Закрыть все";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItemClick);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.arrangeIconsToolStripMenuItem.Text = "&Упорядочить значки";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItemClick);
            // 
            // списикиToolStripMenuItem
            // 
            this.списикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDoctors,
            this.tsmiUser,
            this.tsmiDiagnosis,
            this.параметрыЗдоровьяToolStripMenuItem});
            this.списикиToolStripMenuItem.Name = "списикиToolStripMenuItem";
            this.списикиToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.списикиToolStripMenuItem.Text = "Администратору";
            this.списикиToolStripMenuItem.Click += new System.EventHandler(this.списикиToolStripMenuItem_Click);
            // 
            // tsmiDoctors
            // 
            this.tsmiDoctors.Name = "tsmiDoctors";
            this.tsmiDoctors.Size = new System.Drawing.Size(194, 22);
            this.tsmiDoctors.Text = "Доктора";
            this.tsmiDoctors.Click += new System.EventHandler(this.TsmiDoctorsClick);
            // 
            // tsmiUser
            // 
            this.tsmiUser.Name = "tsmiUser";
            this.tsmiUser.Size = new System.Drawing.Size(194, 22);
            this.tsmiUser.Text = "Пользователи";
            this.tsmiUser.Click += new System.EventHandler(this.TsmiUserClick);
            // 
            // tsmiDiagnosis
            // 
            this.tsmiDiagnosis.Name = "tsmiDiagnosis";
            this.tsmiDiagnosis.Size = new System.Drawing.Size(194, 22);
            this.tsmiDiagnosis.Text = "Диагнозы";
            this.tsmiDiagnosis.Click += new System.EventHandler(this.TsmiDiagnosisClick);
            // 
            // параметрыЗдоровьяToolStripMenuItem
            // 
            this.параметрыЗдоровьяToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.параметрыЗдоровьяToolStripMenuItem1,
            this.метаданныеToolStripMenuItem,
            this.типыМетаданныхToolStripMenuItem});
            this.параметрыЗдоровьяToolStripMenuItem.Name = "параметрыЗдоровьяToolStripMenuItem";
            this.параметрыЗдоровьяToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.параметрыЗдоровьяToolStripMenuItem.Text = " Параметры здоровья";
            // 
            // параметрыЗдоровьяToolStripMenuItem1
            // 
            this.параметрыЗдоровьяToolStripMenuItem1.Name = "параметрыЗдоровьяToolStripMenuItem1";
            this.параметрыЗдоровьяToolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
            this.параметрыЗдоровьяToolStripMenuItem1.Text = "Параметры здоровья";
            this.параметрыЗдоровьяToolStripMenuItem1.Click += new System.EventHandler(this.параметрыЗдоровьяToolStripMenuItem1_Click);
            // 
            // метаданныеToolStripMenuItem
            // 
            this.метаданныеToolStripMenuItem.Name = "метаданныеToolStripMenuItem";
            this.метаданныеToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.метаданныеToolStripMenuItem.Text = "Метаданные";
            this.метаданныеToolStripMenuItem.Click += new System.EventHandler(this.метаданныеToolStripMenuItem_Click);
            // 
            // типыМетаданныхToolStripMenuItem
            // 
            this.типыМетаданныхToolStripMenuItem.Name = "типыМетаданныхToolStripMenuItem";
            this.типыМетаданныхToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.типыМетаданныхToolStripMenuItem.Text = "Типы метаданных";
            this.типыМетаданныхToolStripMenuItem.Click += new System.EventHandler(this.типыМетаданныхToolStripMenuItem_Click);
            // 
            // пациентуToolStripMenuItem
            // 
            this.пациентуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вводПараметровToolStripMenuItem});
            this.пациентуToolStripMenuItem.Name = "пациентуToolStripMenuItem";
            this.пациентуToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.пациентуToolStripMenuItem.Text = "Пациенту";
            // 
            // вводПараметровToolStripMenuItem
            // 
            this.вводПараметровToolStripMenuItem.Name = "вводПараметровToolStripMenuItem";
            this.вводПараметровToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.вводПараметровToolStripMenuItem.Text = "Ввод параметров";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 705);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1072, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel.Text = "Состояние";
            // 
            // DIMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 727);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "DIMainForm";
            this.Text = "MDIMainForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem списикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoctors;
        private System.Windows.Forms.ToolStripMenuItem tsmiUser;
        private System.Windows.Forms.ToolStripMenuItem tsmiDiagnosis;
        private System.Windows.Forms.ToolStripMenuItem пациентуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вводПараметровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыЗдоровьяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметрыЗдоровьяToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem метаданныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem типыМетаданныхToolStripMenuItem;
    }
}



