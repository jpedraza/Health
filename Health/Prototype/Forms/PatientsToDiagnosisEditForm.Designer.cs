namespace Prototype.Forms
{
    partial class PatientsToDiagnosisEditForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientsToDiagnosisEditForm));
            this.healthDatabaseDataSet = new Prototype.HealthDatabaseDataSet();
            this.patientsToDiagnosisBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.patientsToDiagnosisTableAdapter = new Prototype.HealthDatabaseDataSetTableAdapters.PatientsToDiagnosisTableAdapter();
            this.tableAdapterManager = new Prototype.HealthDatabaseDataSetTableAdapters.TableAdapterManager();
            this.patientsToDiagnosisBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.patientsToDiagnosisBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.patientsToDiagnosisDataGridView = new System.Windows.Forms.DataGridView();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.patientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.patientsTableAdapter = new Prototype.HealthDatabaseDataSetTableAdapters.PatientsTableAdapter();
            this.diagnosisBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.diagnosisTableAdapter = new Prototype.HealthDatabaseDataSetTableAdapters.DiagnosisTableAdapter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.healthDatabaseDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientsToDiagnosisBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientsToDiagnosisBindingNavigator)).BeginInit();
            this.patientsToDiagnosisBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsToDiagnosisDataGridView)).BeginInit();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagnosisBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // healthDatabaseDataSet
            // 
            this.healthDatabaseDataSet.DataSetName = "HealthDatabaseDataSet";
            this.healthDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // patientsToDiagnosisBindingSource
            // 
            this.patientsToDiagnosisBindingSource.DataMember = "PatientsToDiagnosis";
            this.patientsToDiagnosisBindingSource.DataSource = this.healthDatabaseDataSet;
            // 
            // patientsToDiagnosisTableAdapter
            // 
            this.patientsToDiagnosisTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AppointmentsTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CandidatesTableAdapter = null;
            this.tableAdapterManager.DefaultScheduleTableAdapter = null;
            this.tableAdapterManager.DiagnosisClassTableAdapter = null;
            this.tableAdapterManager.DiagnosisTableAdapter = this.diagnosisTableAdapter;
            this.tableAdapterManager.DoctorsTableAdapter = null;
            this.tableAdapterManager.FunctionalClassesTableAdapter = null;
            this.tableAdapterManager.FunctionalDisordersTableAdapter = null;
            this.tableAdapterManager.FunctionalDisordersToPatientsTableAdapter = null;
            this.tableAdapterManager.ParametersTableAdapter = null;
            this.tableAdapterManager.PatientsTableAdapter = this.patientsTableAdapter;
            this.tableAdapterManager.PatientsToDiagnosisTableAdapter = this.patientsToDiagnosisTableAdapter;
            this.tableAdapterManager.PatientsToDoctorsTableAdapter = null;
            this.tableAdapterManager.PatientsToSurgerysTableAdapter = null;
            this.tableAdapterManager.PersonalScheduleTableAdapter = null;
            this.tableAdapterManager.RolesTableAdapter = null;
            this.tableAdapterManager.SpecialtiesTableAdapter = null;
            this.tableAdapterManager.SurgerysTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = Prototype.HealthDatabaseDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.WorkWeeksTableAdapter = null;
            // 
            // patientsToDiagnosisBindingNavigator
            // 
            this.patientsToDiagnosisBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.patientsToDiagnosisBindingNavigator.BindingSource = this.patientsToDiagnosisBindingSource;
            this.patientsToDiagnosisBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.patientsToDiagnosisBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.patientsToDiagnosisBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.patientsToDiagnosisBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.patientsToDiagnosisBindingNavigatorSaveItem});
            this.patientsToDiagnosisBindingNavigator.Location = new System.Drawing.Point(3, 0);
            this.patientsToDiagnosisBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.patientsToDiagnosisBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.patientsToDiagnosisBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.patientsToDiagnosisBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.patientsToDiagnosisBindingNavigator.Name = "patientsToDiagnosisBindingNavigator";
            this.patientsToDiagnosisBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.patientsToDiagnosisBindingNavigator.Size = new System.Drawing.Size(286, 25);
            this.patientsToDiagnosisBindingNavigator.TabIndex = 0;
            this.patientsToDiagnosisBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(43, 22);
            this.bindingNavigatorCountItem.Text = "для {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Добавить";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Удалить";
            // 
            // patientsToDiagnosisBindingNavigatorSaveItem
            // 
            this.patientsToDiagnosisBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.patientsToDiagnosisBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("patientsToDiagnosisBindingNavigatorSaveItem.Image")));
            this.patientsToDiagnosisBindingNavigatorSaveItem.Name = "patientsToDiagnosisBindingNavigatorSaveItem";
            this.patientsToDiagnosisBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.patientsToDiagnosisBindingNavigatorSaveItem.Text = "Сохранить данные";
            this.patientsToDiagnosisBindingNavigatorSaveItem.Click += new System.EventHandler(this.PatientsToDiagnosisBindingNavigatorSaveItemClick);
            // 
            // patientsToDiagnosisDataGridView
            // 
            this.patientsToDiagnosisDataGridView.AutoGenerateColumns = false;
            this.patientsToDiagnosisDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.patientsToDiagnosisDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientsToDiagnosisDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.patientsToDiagnosisDataGridView.DataSource = this.patientsToDiagnosisBindingSource;
            this.patientsToDiagnosisDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientsToDiagnosisDataGridView.Location = new System.Drawing.Point(0, 0);
            this.patientsToDiagnosisDataGridView.Name = "patientsToDiagnosisDataGridView";
            this.patientsToDiagnosisDataGridView.Size = new System.Drawing.Size(720, 433);
            this.patientsToDiagnosisDataGridView.TabIndex = 1;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.patientsToDiagnosisDataGridView);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(720, 433);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(720, 480);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.patientsToDiagnosisBindingNavigator);
            // 
            // patientsBindingSource
            // 
            this.patientsBindingSource.DataMember = "Patients";
            this.patientsBindingSource.DataSource = this.healthDatabaseDataSet;
            // 
            // patientsTableAdapter
            // 
            this.patientsTableAdapter.ClearBeforeFill = true;
            // 
            // diagnosisBindingSource
            // 
            this.diagnosisBindingSource.DataMember = "Diagnosis";
            this.diagnosisBindingSource.DataSource = this.healthDatabaseDataSet;
            // 
            // diagnosisTableAdapter
            // 
            this.diagnosisTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PatientId";
            this.dataGridViewTextBoxColumn1.DataSource = this.patientsBindingSource;
            this.dataGridViewTextBoxColumn1.DisplayMember = "Policy";
            this.dataGridViewTextBoxColumn1.HeaderText = "Patient";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn1.ValueMember = "PatientId";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DiagnosisId";
            this.dataGridViewTextBoxColumn2.DataSource = this.diagnosisBindingSource;
            this.dataGridViewTextBoxColumn2.DisplayMember = "Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Diagnosis";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn2.ValueMember = "DiagnosisId";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(720, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(42, 17);
            this.tsslStatus.Text = "Status:";
            // 
            // PatientsToDiagnosisEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 480);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "PatientsToDiagnosisEditForm";
            this.Text = "PatientsToDiagnosisEditForm";
            this.Load += new System.EventHandler(this.PatientsToDiagnosisEditFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.healthDatabaseDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientsToDiagnosisBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientsToDiagnosisBindingNavigator)).EndInit();
            this.patientsToDiagnosisBindingNavigator.ResumeLayout(false);
            this.patientsToDiagnosisBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsToDiagnosisDataGridView)).EndInit();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagnosisBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private HealthDatabaseDataSet healthDatabaseDataSet;
        private System.Windows.Forms.BindingSource patientsToDiagnosisBindingSource;
        private HealthDatabaseDataSetTableAdapters.PatientsToDiagnosisTableAdapter patientsToDiagnosisTableAdapter;
        private HealthDatabaseDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator patientsToDiagnosisBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton patientsToDiagnosisBindingNavigatorSaveItem;
        private HealthDatabaseDataSetTableAdapters.PatientsTableAdapter patientsTableAdapter;
        private System.Windows.Forms.DataGridView patientsToDiagnosisDataGridView;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.BindingSource patientsBindingSource;
        private HealthDatabaseDataSetTableAdapters.DiagnosisTableAdapter diagnosisTableAdapter;
        private System.Windows.Forms.BindingSource diagnosisBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
    }
}