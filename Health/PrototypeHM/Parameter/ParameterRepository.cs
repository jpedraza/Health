using System.Collections.Generic;
using System.Data.SqlClient;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;

namespace PrototypeHM.Parameter {
    internal class ParameterRepository : Repository {

        public ParameterRepository(IDIKernel diKernel)
            : base(diKernel)
        {
        }

        public IList<ParameterFullData> GetAll() {
            var db = DIKernel.Get<DB.DB>();
            SqlCommand command = CreateQuery(GetQueryText("GetAllParameterShowData"));
            SqlDataReader reader = command.ExecuteReader();
        
            IList<ParameterFullData> parameters = Get<PropertyToColumnMapper<ParameterFullData>>().Map(reader);
            reader.Close();
            return parameters;
        }

        internal ParameterDetail Detail(ParameterFullData data) {
            SqlCommand command = CreateQuery(GetQueryText("GetAllMetadataForParameter",
                new Dictionary<string, string> { { "parameterId", data.ParameterId.ToString() } }));
            SqlDataReader reader = command.ExecuteReader();
            IList<MetadataForParameter> metadatas = Get<PropertyToColumnMapper<MetadataForParameter>>().Map(reader);
            reader.Close();
            return new ParameterDetail
                {
                    ParameterId = data.ParameterId,
                    Name = data.Name,
                    Metadata = metadatas
                };           
           
        }
    }
}