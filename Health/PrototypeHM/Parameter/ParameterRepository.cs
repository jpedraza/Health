using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using PrototypeHM.DB;
using PrototypeHM.DB.DI;
using PrototypeHM.DB.Mappers;
using System;
using System.Text;

namespace PrototypeHM.Parameter {
    internal class ParameterRepository : Repository {

        public ParameterRepository(IDIKernel diKernel)
            : base(diKernel)
        {
        }

        public IList<ParameterDetail> GetAll()
        {
            var db = DIKernel.Get<DB.DB>();
            SqlCommand command = CreateQuery(GetQueryText("GetAllParameterShowData"));
            SqlDataReader reader = command.ExecuteReader();

            IList<ParameterDetail> parameters = Get<PropertyToColumnMapper<ParameterDetail>>().Map(reader);
            reader.Close();
            return parameters;
        }

        internal ParameterDetail Detail(ParameterDetail data)
        {

            SqlCommand command = CreateQuery(GetQueryText("GetParameterById",
                new Dictionary<string, string> { {"parameterId", data.ParameterId.ToString()}}));

            SqlDataReader reader = command.ExecuteReader();

            var _data = Get<PropertyToColumnMapper<ParameterBaseData>>().Map(reader)[0];

            if (_data != null)
            {
                SqlCommand command2 = CreateQuery(GetQueryText("GetAllMetadataForParameter",
                    new Dictionary<string, string> { { "parameterId", _data.ParameterId.ToString() } }));

                var metaData = Get<PropertyToColumnMapper<MetadataForParameter>>().Map(reader);

                return new ParameterDetail { ParameterId = _data.ParameterId,
                Id = _data.ParameterId,
                Name = _data.Name,
                DefaultValue = _data.DefaultValue,
                Metadata = metaData
                };
            }
            return new ParameterDetail();


        }

        internal QueryStatus Save(ParameterDetail data)
        {
            return new QueryStatus();
        }

        internal QueryStatus Update(ParameterDetail data)
        {
            return new QueryStatus();
        }

        internal QueryStatus SaveMetadata(MetadataForParameter data)
        {
            return new QueryStatus() { Status = 1};
        }

        internal QueryStatus SaveValueType(ValueTypeOfMetadata data)
        {
            var @queryStatus = new QueryStatus();
            var @parameters = this.CheckWrittingPropertys(data.Name);
            if (@parameters == null)
            {
                @queryStatus.Status = 0;
                @queryStatus.StatusMessage = "Проверьте пожалуйста данные";
            }
            else
            {
                var c = CreateQuery("EXEC [dbo].[NewValueTypes] @name");
                c.Parameters.Add("name", SqlDbType.NVarChar);
                c.Parameters[0].Value = parameters[0];
                var reader = c.ExecuteReader();
                queryStatus = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
                reader.Close();
            }

            return @queryStatus;
        }

        public IList<ValueTypeOfMetadata> GetAllValueTypes()
        {
            var c = CreateQuery("EXEC [dbo].[GetAllValueTypes]");
            var reader = c.ExecuteReader();
            var list =
                Get<PropertyToColumnMapper<ValueTypeOfMetadata>>().Map
                    (reader);
            reader.Close();
            return list;
        }

        internal QueryStatus DeleteValueTypeById(ValueTypeOfMetadata deleteData)
        {
            var c = CreateQuery("EXEC [dbo].[DeleteValueTypes] @deleteValueTypeId");
            c.Parameters.Add("deleteValueTypeId", SqlDbType.Int);
            c.Parameters[0].Value = deleteData.ValueTypeId;
            var reader = c.ExecuteReader();
            var queryStatus = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();
            return queryStatus;
        }

        internal QueryStatus UpdateValueTypeById(ValueTypeOfMetadata updateData)
        {
            var c = CreateQuery("EXEC [dbo].[UpdateValueTypes] @ValueTypeId, @Name");
            c.Parameters.Add("ValueTypeId", SqlDbType.Int);
            c.Parameters[0].Value = updateData.ValueTypeId;

            c.Parameters.Add("Name", SqlDbType.NVarChar);
            c.Parameters[1].Value = updateData.Name;
            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();
            return qS;
        }

        internal ValueTypeOfMetadata DetailValueType(ValueTypeOfMetadata data)
        {
            if (data == null) throw new ArgumentNullException("data");

            var c = CreateQuery("EXEC [dbo].[GetValueTypeDataById] @ValueTypeId");
            c.Parameters.Add("ValueTypeId", SqlDbType.Int);
            c.Parameters[0].Value = data.ValueTypeId;

            var reader = c.ExecuteReader();
            var qS = Get<PropertyToColumnMapper<ValueTypeOfMetadata>>().Map(reader)[0];
            reader.Close();
            return qS;
        }
    }
}