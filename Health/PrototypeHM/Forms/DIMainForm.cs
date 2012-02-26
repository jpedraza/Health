using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EFCFModel;
using PrototypeHM.DI;

namespace PrototypeHM.Forms
{
    public partial class DIMainForm : DIForm
    {
        private readonly SchemaManager _schemaManager;

        public DIMainForm(IDIKernel diKernel) : base(diKernel)
        {
            InitializeComponent();
            _schemaManager = Get<SchemaManager>();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            IList<Type> allScaffoldEntities = _schemaManager.GetAllScaffoldEntities();
            var menuItem = new ToolStripMenuItem("Администрирование");
            var dropDownMenu = new ToolStripDropDownMenu {Text = @"Администрирование"};
            foreach (Type scaffoldEntity in allScaffoldEntities)
            {
                Type entity = scaffoldEntity;
                var but = new ToolStripButton(entity.GetDisplayName());
                but.Click += (sender, e) =>
                                 {
                                     var form = new ListForm(DIKernel, entity) {MdiParent = this};
                                     form.Show();
                                 };
                dropDownMenu.Items.Add(but);
            }
            menuItem.DropDown = dropDownMenu;
            menuStrip.Items.Add(menuItem);
        }

        private void CascadeToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
                childForm.Close();
        }

        private void EnterParametersToolStripMenuItemClick(object sender, EventArgs e)
        {
            var form = new Patient.EnterParameterForm(DIKernel, 4);
            form.Show();
        }
    }
}