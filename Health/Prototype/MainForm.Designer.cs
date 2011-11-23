namespace Prototype
{
    partial class MainForm
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
            this.администрированиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.управлениеПользователямиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCandidates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPatients = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDoctors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpecialities = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAppointments = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiagnosis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiagnosisClass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFunctionalClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFunctionalDisorders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSurgerys = new System.Windows.Forms.ToolStripMenuItem();
            this.пациентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFunctionalDisordersToPatients = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPatientsToDiagnosis = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPatientsToDoctors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPatientsToSurgerys = new System.Windows.Forms.ToolStripMenuItem();
            this.сериализацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSerializationMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeserializationMetadata = new System.Windows.Forms.ToolStripMenuItem();
            this.answerTypeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.администрированиеToolStripMenuItem,
            this.пациентыToolStripMenuItem,
            this.сериализацияToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(940, 24);
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
            // администрированиеToolStripMenuItem
            // 
            this.администрированиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеПользователямиToolStripMenuItem,
            this.tsmiAppointments,
            this.tsmiDiagnosis,
            this.tsmiDiagnosisClass,
            this.tsmiFunctionalClasses,
            this.tsmiFunctionalDisorders,
            this.tsmiSurgerys});
            this.администрированиеToolStripMenuItem.Name = "администрированиеToolStripMenuItem";
            this.администрированиеToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.администрированиеToolStripMenuItem.Text = "Администрирование";
            // 
            // управлениеПользователямиToolStripMenuItem
            // 
            this.управлениеПользователямиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRoles,
            this.tsmiUsers,
            this.tsmiCandidates,
            this.tsmiPatients,
            this.tsmiDoctors,
            this.tsmiSpecialities});
            this.управлениеПользователямиToolStripMenuItem.Name = "управлениеПользователямиToolStripMenuItem";
            this.управлениеПользователямиToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.управлениеПользователямиToolStripMenuItem.Text = "Управление пользователями";
            // 
            // tsmiRoles
            // 
            this.tsmiRoles.Name = "tsmiRoles";
            this.tsmiRoles.Size = new System.Drawing.Size(160, 22);
            this.tsmiRoles.Text = "Роли";
            this.tsmiRoles.Click += new System.EventHandler(this.TsmiRolesClick);
            // 
            // tsmiUsers
            // 
            this.tsmiUsers.Name = "tsmiUsers";
            this.tsmiUsers.Size = new System.Drawing.Size(160, 22);
            this.tsmiUsers.Text = "Пользователи";
            this.tsmiUsers.Click += new System.EventHandler(this.TsmiUsersClick);
            // 
            // tsmiCandidates
            // 
            this.tsmiCandidates.Name = "tsmiCandidates";
            this.tsmiCandidates.Size = new System.Drawing.Size(160, 22);
            this.tsmiCandidates.Text = "Кандидаты";
            this.tsmiCandidates.Click += new System.EventHandler(this.TsmiCandidatesClick);
            // 
            // tsmiPatients
            // 
            this.tsmiPatients.Name = "tsmiPatients";
            this.tsmiPatients.Size = new System.Drawing.Size(160, 22);
            this.tsmiPatients.Text = "Пациенты";
            this.tsmiPatients.Click += new System.EventHandler(this.TsmiPatientsClick);
            // 
            // tsmiDoctors
            // 
            this.tsmiDoctors.Name = "tsmiDoctors";
            this.tsmiDoctors.Size = new System.Drawing.Size(160, 22);
            this.tsmiDoctors.Text = "Доктора";
            this.tsmiDoctors.Click += new System.EventHandler(this.TsmiDoctorsClick);
            // 
            // tsmiSpecialities
            // 
            this.tsmiSpecialities.Name = "tsmiSpecialities";
            this.tsmiSpecialities.Size = new System.Drawing.Size(160, 22);
            this.tsmiSpecialities.Text = "Специальности";
            this.tsmiSpecialities.Click += new System.EventHandler(this.TsmiSpecialitiesClick);
            // 
            // tsmiAppointments
            // 
            this.tsmiAppointments.Name = "tsmiAppointments";
            this.tsmiAppointments.Size = new System.Drawing.Size(238, 22);
            this.tsmiAppointments.Text = "Записи на прием";
            this.tsmiAppointments.Click += new System.EventHandler(this.TsmiAppointmentsClick);
            // 
            // tsmiDiagnosis
            // 
            this.tsmiDiagnosis.Name = "tsmiDiagnosis";
            this.tsmiDiagnosis.Size = new System.Drawing.Size(238, 22);
            this.tsmiDiagnosis.Text = "Диагнозы";
            this.tsmiDiagnosis.Click += new System.EventHandler(this.TsmiDiagnosisClick);
            // 
            // tsmiDiagnosisClass
            // 
            this.tsmiDiagnosisClass.Name = "tsmiDiagnosisClass";
            this.tsmiDiagnosisClass.Size = new System.Drawing.Size(238, 22);
            this.tsmiDiagnosisClass.Text = "Классы диагнозов";
            this.tsmiDiagnosisClass.Click += new System.EventHandler(this.TsmiDiagnosisClassClick);
            // 
            // tsmiFunctionalClasses
            // 
            this.tsmiFunctionalClasses.Name = "tsmiFunctionalClasses";
            this.tsmiFunctionalClasses.Size = new System.Drawing.Size(238, 22);
            this.tsmiFunctionalClasses.Text = "Функциональные классы";
            this.tsmiFunctionalClasses.Click += new System.EventHandler(this.TsmiFunctionalClassesClick);
            // 
            // tsmiFunctionalDisorders
            // 
            this.tsmiFunctionalDisorders.Name = "tsmiFunctionalDisorders";
            this.tsmiFunctionalDisorders.Size = new System.Drawing.Size(238, 22);
            this.tsmiFunctionalDisorders.Text = "Фннкциональные нарушения";
            this.tsmiFunctionalDisorders.Click += new System.EventHandler(this.TsmiFunctionalDisordersClick);
            // 
            // tsmiSurgerys
            // 
            this.tsmiSurgerys.Name = "tsmiSurgerys";
            this.tsmiSurgerys.Size = new System.Drawing.Size(238, 22);
            this.tsmiSurgerys.Text = "Виды операций";
            this.tsmiSurgerys.Click += new System.EventHandler(this.TsmiSurgerysClick);
            // 
            // пациентыToolStripMenuItem
            // 
            this.пациентыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFunctionalDisordersToPatients,
            this.tsmiPatientsToDiagnosis,
            this.tsmiPatientsToDoctors,
            this.tsmiPatientsToSurgerys});
            this.пациентыToolStripMenuItem.Name = "пациентыToolStripMenuItem";
            this.пациентыToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.пациентыToolStripMenuItem.Text = "Пациенты";
            // 
            // tsmiFunctionalDisordersToPatients
            // 
            this.tsmiFunctionalDisordersToPatients.Name = "tsmiFunctionalDisordersToPatients";
            this.tsmiFunctionalDisordersToPatients.Size = new System.Drawing.Size(292, 22);
            this.tsmiFunctionalDisordersToPatients.Text = "Привязка функциональных нарушений";
            this.tsmiFunctionalDisordersToPatients.Click += new System.EventHandler(this.TsmiFunctionalDisordersToPatientsClick);
            // 
            // tsmiPatientsToDiagnosis
            // 
            this.tsmiPatientsToDiagnosis.Name = "tsmiPatientsToDiagnosis";
            this.tsmiPatientsToDiagnosis.Size = new System.Drawing.Size(292, 22);
            this.tsmiPatientsToDiagnosis.Text = "Привязка диагнозов";
            this.tsmiPatientsToDiagnosis.Click += new System.EventHandler(this.TsmiPatientsToDiagnosisClick);
            // 
            // tsmiPatientsToDoctors
            // 
            this.tsmiPatientsToDoctors.Name = "tsmiPatientsToDoctors";
            this.tsmiPatientsToDoctors.Size = new System.Drawing.Size(292, 22);
            this.tsmiPatientsToDoctors.Text = "Привязка докторов";
            this.tsmiPatientsToDoctors.Click += new System.EventHandler(this.TsmiPatientsToDoctorsClick);
            // 
            // tsmiPatientsToSurgerys
            // 
            this.tsmiPatientsToSurgerys.Name = "tsmiPatientsToSurgerys";
            this.tsmiPatientsToSurgerys.Size = new System.Drawing.Size(292, 22);
            this.tsmiPatientsToSurgerys.Text = "Привязка операций";
            this.tsmiPatientsToSurgerys.Click += new System.EventHandler(this.TsmiPatientsToSurgerysClick);
            // 
            // сериализацияToolStripMenuItem
            // 
            this.сериализацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSerializationMetadata,
            this.tsmiDeserializationMetadata,
            this.answerTypeControlToolStripMenuItem});
            this.сериализацияToolStripMenuItem.Name = "сериализацияToolStripMenuItem";
            this.сериализацияToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.сериализацияToolStripMenuItem.Text = "Сериализация";
            // 
            // tsmiSerializationMetadata
            // 
            this.tsmiSerializationMetadata.Name = "tsmiSerializationMetadata";
            this.tsmiSerializationMetadata.Size = new System.Drawing.Size(234, 22);
            this.tsmiSerializationMetadata.Text = "Сериализация метаданных";
            this.tsmiSerializationMetadata.Click += new System.EventHandler(this.TsmiSerializationMetadataClick);
            // 
            // tsmiDeserializationMetadata
            // 
            this.tsmiDeserializationMetadata.Name = "tsmiDeserializationMetadata";
            this.tsmiDeserializationMetadata.Size = new System.Drawing.Size(234, 22);
            this.tsmiDeserializationMetadata.Text = "Десериализация метаданных";
            this.tsmiDeserializationMetadata.Click += new System.EventHandler(this.TsmiDeserializationMetadataClick);
            // 
            // answerTypeControlToolStripMenuItem
            // 
            this.answerTypeControlToolStripMenuItem.Name = "answerTypeControlToolStripMenuItem";
            this.answerTypeControlToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.answerTypeControlToolStripMenuItem.Text = "AnswerTypeControl";
            this.answerTypeControlToolStripMenuItem.Click += new System.EventHandler(this.AnswerTypeControlToolStripMenuItemClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 545);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(940, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel.Text = "Состояние";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 567);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
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
        private System.Windows.Forms.ToolStripMenuItem администрированиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem управлениеПользователямиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiRoles;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiCandidates;
        private System.Windows.Forms.ToolStripMenuItem tsmiPatients;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoctors;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpecialities;
        private System.Windows.Forms.ToolStripMenuItem tsmiAppointments;
        private System.Windows.Forms.ToolStripMenuItem tsmiDiagnosis;
        private System.Windows.Forms.ToolStripMenuItem tsmiDiagnosisClass;
        private System.Windows.Forms.ToolStripMenuItem tsmiFunctionalClasses;
        private System.Windows.Forms.ToolStripMenuItem tsmiFunctionalDisorders;
        private System.Windows.Forms.ToolStripMenuItem пациентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiFunctionalDisordersToPatients;
        private System.Windows.Forms.ToolStripMenuItem tsmiPatientsToDiagnosis;
        private System.Windows.Forms.ToolStripMenuItem tsmiPatientsToDoctors;
        private System.Windows.Forms.ToolStripMenuItem tsmiPatientsToSurgerys;
        private System.Windows.Forms.ToolStripMenuItem tsmiSurgerys;
        private System.Windows.Forms.ToolStripMenuItem сериализацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSerializationMetadata;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeserializationMetadata;
        private System.Windows.Forms.ToolStripMenuItem answerTypeControlToolStripMenuItem;
    }
}



