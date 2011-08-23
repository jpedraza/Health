using System;
using Health.API.Extensions;

namespace Health.API.Entities.Virtual
{
    public class Week : IWeek
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _alias = value.TranslitRuToEn();
            }
        }

        protected string _alias;
        public string Alias
        {
            get
            {
                if (String.IsNullOrEmpty(_alias))
                {
                    _alias = Name.TranslitRuToEn();
                }
                return _alias;
            }
            set { _alias = value; }
        }
    }
}
