using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;
using PrototypeHM.Parameter;

namespace PrototypeHM.Doctor
{
    internal class DoctorRepository : Repository
    {
        public DoctorRepository(IDIKernel diKernel) : base(diKernel)
        {
        }

        internal IList<DoctorFullData> GetAll()
        {
            SqlCommand command = CreateQuery(GetQueryText("GetAllDoctorShowData"));
            SqlDataReader reader = command.ExecuteReader();
            IList<DoctorFullData> doctors = Get<PropertyToColumnMapper<DoctorFullData>>().Map(reader);
            reader.Close();
            return doctors;
        }

        internal QueryStatus Delete(DoctorFullData data)
        {
            SqlCommand command =
                CreateQuery(GetQueryText("DeleteDoctor",
                                         new Dictionary<string, string> {{"doctorId", data.Id.ToString()}}));
            SqlDataReader reader = command.ExecuteReader();
            IList<QueryStatus> results = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader);
            reader.Close();
            return results.FirstOrDefault();
        }

        internal DoctorDetail Detail(DoctorFullData data)
        {
            SqlCommand command =
                CreateQuery(GetQueryText("GetAllPatientsForDoctor",
                                         new Dictionary<string, string> {{"doctorId", data.Id.ToString()}}));
            SqlDataReader reader = command.ExecuteReader();
            IList<PatientForDoctor> patients = Get<PropertyToColumnMapper<PatientForDoctor>>().Map(reader);
            reader.Close();
            var detailData = new DoctorDetail
                                 {
                                     Id = data.Id,
                                     FirstName = data.FirstName,
                                     LastName = data.LastName,
                                     ThirdName = data.ThirdName,
                                     Login = data.Login,
                                     Password = data.Password,
                                     Role = data.Role,
                                     Specialty = data.Specialty,
                                     Token = data.Token,
                                     Birthday = data.Birthday,
                                     Patients = patients
                                 };
            return detailData;
        }

        internal DoctorFullData GetDoctorFullData(int doctorId)
        {
            var c = CreateQuery("[dbo].[GetDoctorShowData] @doctorId");
            c.Parameters.Add("ParameterId", SqlDbType.Int);
            c.Parameters[0].Value = doctorId;

            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<DoctorFullData>>().Map(reader)[0];
            reader.Close();
            return qS;
        }

        internal QueryStatus Save(DoctorDetail data)
        {
            return new QueryStatus();
        }
        
    }
}
