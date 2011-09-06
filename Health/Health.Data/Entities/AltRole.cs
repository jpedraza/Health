using Health.API.Entities;

namespace Health.Data.Entities
{
    public class AltRole : IRole
    {
        private string _name;

        #region IRole Members

        public string Name
        {
            get { return "Piter"; }
            set { _name = value; }
        }

        public int Code { get; set; }

        #endregion
    }
}