using System;
using System.Windows.Forms;
using EFDAL;
using Prototype.Forms;

namespace Prototype
{
    public partial class MainForm : YForm
    {
        public MainForm()
        {
            
        }

        public MainForm(Entities entities) :  base(entities)
        {
            
        }

        private void ToolBarToolStripMenuItemClick(object sender, EventArgs e)
        {
        }

        private void StatusBarToolStripMenuItemClick(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
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

        private void CloseAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            
        }

        private void RolesToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowFormByType<RolesEditForm>();
        }

        private void ShowFormByType<T>()
        {
            Form[] childForms = MdiChildren;
            bool isset = false;
            foreach (var childForm in childForms)
            {
                if (childForm.GetType() == typeof(T))
                {
                    isset = true;
                }
            }
            if (!isset)
            {
                Form cform;
                if (typeof(T).BaseType == typeof(YForm))
                {
                    cform = Activator.CreateInstance(typeof(T), _entities) as Form;
                }
                else
                {
                    cform = Activator.CreateInstance(typeof (T)) as Form;
                }
                if (cform == null) throw new Exception(String.Format("Невозможно создать окно {0}", typeof(T).Name));
                cform.MdiParent = this;
                cform.Show();
            }
        }
    }
}
