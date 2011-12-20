using System.Data.SqlClient;
using System.Linq;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;
using PrototypeHM.Doctor;

namespace PrototypeHM.Patient
{
    internal class PatientRepository : Repository
    {
        public PatientRepository(IDIKernel diKernel) : base(diKernel)
        {
        }

        public PatientFullData GetPatientFullDataById(PatientForDoctor patientFor)
        {
            SqlCommand command = CreateQuery(string.Format("exec [dbo].[GetPatientFullDataById] {0}", patientFor.Id));
            SqlDataReader reader = command.ExecuteReader();
            PatientFullData patient = Get<PropertyToColumnMapper<PatientFullData>>().Map(reader).FirstOrDefault();
            reader.Close();
            return patient;
        }
    }
}
