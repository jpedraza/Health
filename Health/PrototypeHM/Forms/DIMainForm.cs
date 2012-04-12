using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Model;
using Prototype.DI;
using Prototype.Forms.Patient;

namespace Prototype.Forms
{
    public partial class DIMainForm : DIForm
    {
        private readonly ISchemaManager _schemaManager;

        public DIMainForm(IDIKernel diKernel) : base(diKernel)
        {
            InitializeComponent();
            _schemaManager = Get<ISchemaManager>();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            IEnumerable<Type> allScaffoldEntities = _schemaManager.GetAllScaffoldEntities();
            var menuItem = new ToolStripMenuItem("Администрирование") {Name = "MainMenuItem"};
            var dropDownMenu = new ToolStripDropDownMenu {Text = @"Администрирование", Name = "MainMenuDropDownMenu"};
            foreach (Type scaffoldEntity in allScaffoldEntities)
            {
                Type entity = scaffoldEntity;
                var but = new ToolStripButton(entity.GetDisplayName());
                but.Click += (sender, e) =>
                                    {
                                        var form =
                                            Get<ListForm>(new ConstructorArgument {Name = "etype", Value = entity});
                                        form.MdiParent = this;
                                        form.Show();
                                    };
                if (_schemaManager.HasBaseType(scaffoldEntity))
                {
                    Type baseType = _schemaManager.GetBaseType(scaffoldEntity);
                    var subMenuItem = dropDownMenu.Items[string.Format("SubMenuItem{0}", baseType.Name)] as ToolStripMenuItem;
                    if (subMenuItem == null)
                    {
                        subMenuItem = new ToolStripMenuItem
                                          {
                                              Text = baseType.GetDisplayName(),
                                              Name = string.Format("SubMenuItem{0}", baseType.Name),
                                              DropDown = new ToolStripDropDownMenu
                                                             {
                                                                 Text = baseType.GetDisplayName(),
                                                                 Name =
                                                                     string.Format("SubMenuDropDown{0}", baseType.Name)
                                                             }
                                          };
                        dropDownMenu.Items.Add(subMenuItem);
                    }
                    subMenuItem.DropDown.Items.Add(but);
                }
                else
                {
                    dropDownMenu.Items.Add(but);   
                }
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
            var form = Get<EnterParameterForm>(new ConstructorArgument {Name = "id", Value = 4});
            form.MdiParent = this;
            form.Show();
        }
    }
}