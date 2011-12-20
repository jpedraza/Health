using System.Collections.Generic;
using System.Data.SqlClient;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;

namespace PrototypeHM.User
{
    internal class UserRepository : Repository
    {
        public UserRepository(IDIKernel diKernel) : base(diKernel)
        {
        }

        public IList<UserFullData> GetAll()
        {
            var db = DIKernel.Get<DB.DB>();
            SqlCommand command = CreateQuery(GetQueryText("GetAllUserShowData"));
            SqlDataReader reader = command.ExecuteReader();
            IList<UserFullData> doctors = Get<PropertyToColumnMapper<UserFullData>>().Map(reader);
            reader.Close();
            return doctors;
        }
    }
}
