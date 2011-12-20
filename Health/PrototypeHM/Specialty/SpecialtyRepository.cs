using System.Collections.Generic;
using System.Data.SqlClient;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;

namespace PrototypeHM.Specialty
{
    internal class SpecialtyRepository : Repository
    {
        public SpecialtyRepository(IDIKernel diKernel) : base(diKernel)
        {
        }

        public IList<Specialty> GetAll()
        {
            SqlCommand command = CreateQuery("select * from Specialties");
            SqlDataReader reader = command.ExecuteReader();
            IList<Specialty> results = Get<PropertyToColumnMapper<Specialty>>().Map(reader);
            reader.Close();
            return results;
        }
    }
}
