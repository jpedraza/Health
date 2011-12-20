using System.Collections.Generic;
using System.Data.SqlClient;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;

namespace PrototypeHM.Diagnosis
{
    internal class DiagnosisRepository : Repository
    {
        public DiagnosisRepository(IDIKernel diKernel) : base(diKernel)
        {
        }

        public IList<DiagnosisFullData> GetAll()
        {
            SqlCommand command = CreateQuery(GetQueryText("GetAllDiagnosis"));
            SqlDataReader reader = command.ExecuteReader();
            IList<DiagnosisFullData> results = Get<PropertyToColumnMapper<DiagnosisFullData>>().Map(reader);
            reader.Close();
            return results;
        }
    }
}
