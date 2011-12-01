using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Support
{
    internal interface ITask
    {
        void Process(SqlConnection connection);
    }
}
