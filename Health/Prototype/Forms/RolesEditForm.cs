using EFDAL;

namespace Prototype.Forms
{
    public partial class RolesEditForm : YForm
    {
        public RolesEditForm()
        {
            
        }

        public RolesEditForm(Entities entities) : base(entities)
        {
            
        }

        private void RolesEditFormLoad(object sender, System.EventArgs e)
        {
            rolesDataGridView.DataSource = _entities.Roles;
        }

        private void RolesBindingNavigatorSaveItemClick(object sender, System.EventArgs e)
        {
            _entities.SaveChanges();
        }
    }
}
