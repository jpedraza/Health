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

        public IList<ParameterBaseData> GetAllParameters()
        {
            var command = CreateQuery("EXEC [dbo].[GetAllParameterShowData]");
            
            var reader = command.ExecuteReader();
            var parameters = Get<PropertyToColumnMapper<ParameterBaseData>>().Map(reader);
            reader.Close();

            return parameters;
        }

        internal ParameterDetail Detail(ParameterDetail data)
        {
            var c = CreateQuery("[dbo].[GetParameterById] @ParameterId");
            c.Parameters.Add("ParameterId", SqlDbType.Int);
            c.Parameters[0].Value = data.ParameterId;

            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<ParameterDetail>>().Map(reader)[0];
            reader.Close();

            var c2 = CreateQuery("[dbo].[GetAllMetadataForParameterByParameterId] @ParameterId");
            c2.Parameters.Add("ParameterId", SqlDbType.Int);
            c2.Parameters[0].Value = data.ParameterId;

            var reader2 = c2.ExecuteReader();
            var qS2 = Get<PropertyToColumnMapper<MetadataForParameter>>().Map(reader2);
            reader2.Close();

            qS.Metadata = qS2;

            return qS;
        }

        internal QueryStatus SaveNewParameter(ParameterBaseData data)
        {
            var c0 = CreateQuery("[dbo].[NewParameter] @nameParameter, @defaultValue");
            c0.Parameters.Add("nameParameter", SqlDbType.NVarChar);
            c0.Parameters[0].Value = data.Name;

            c0.Parameters.Add("defaultValue", SqlDbType.VarBinary);
            c0.Parameters[1].Value = data.DefaultValue;

            var reader = c0.ExecuteReader();
            var qS = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();

            return qS;
        }

        internal QueryStatus UpdateParameter(ParameterDetail data)
        {
            var c0 = CreateQuery("[dbo].[UpdateParameter] @ParameterId, @Name, @DefaultValue");
            c0.Parameters.Add("ParameterId", SqlDbType.Int);
            c0.Parameters[0].Value = data.ParameterId;

            c0.Parameters.Add("Name", SqlDbType.NVarChar);
            c0.Parameters[1].Value = data.Name;

            c0.Parameters.Add("DefaultValue", SqlDbType.VarBinary);
            c0.Parameters[2].Value = data.DefaultValue;

            var reader = c0.ExecuteReader();
            var qS = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();

            
            return qS;
        }

        internal QueryStatus DeleteParameter (ParameterBaseData deleteParameter)
        {
            var c = CreateQuery("EXEC [dbo].[DeleteParameter] @Id");
            c.Parameters.Add("Id", SqlDbType.Int);
            c.Parameters[0].Value = deleteParameter.ParameterId;
            var reader = c.ExecuteReader();
            var queryStatus = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();
            return queryStatus;
        }

        internal QueryStatus SaveMetadata(MetadataForParameter data)
        {

            var c = CreateQuery("[dbo].[NewParameterMetadata] @ParameterId, @Key, @Value, @ValueTypeId");
            c.Parameters.Add("ParameterId", SqlDbType.Int);
            c.Parameters[0].Value = data.ParameterId;

            c.Parameters.Add("Key", SqlDbType.NVarChar);
            c.Parameters[1].Value = data.Key;

            c.Parameters.Add("Value", SqlDbType.VarBinary);
            c.Parameters[2].Value = data.Value;

            c.Parameters.Add("ValueTypeId", SqlDbType.NVarChar);
            c.Parameters[3].Value = data.ValueTypeId;

            var reader2 = c.ExecuteReader();
            var qS2 = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader2)[0];
            reader2.Close();


            return qS2;
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

        internal IList<MetadataForParameter> GetAllMetadataForParameters ()
        {
            var c = CreateQuery("EXEC [dbo].[GetAllMetadata]");
            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<MetadataForParameter>>().Map(reader);
            reader.Close();

           return qS;
        }

        internal QueryStatus UpdateMetadata(MetadataForParameter editMetadataForParameter)
        {
            var c = CreateQuery("EXEC [dbo].[UpdateParameterMetadata] @Id, @ParameterId, @Key, @Value, @ValueTypeId");
            
            c.Parameters.Add("Id", SqlDbType.Int);
            c.Parameters[0].Value = editMetadataForParameter.Id;

            c.Parameters.Add("ParameterId", SqlDbType.Int);
            c.Parameters[1].Value = editMetadataForParameter.ParameterId;

            c.Parameters.Add("Key", SqlDbType.NVarChar);
            c.Parameters[2].Value = editMetadataForParameter.Key;

            c.Parameters.Add("Value", SqlDbType.VarBinary);
            c.Parameters[3].Value = editMetadataForParameter.Value;

            c.Parameters.Add("ValueTypeId", SqlDbType.Int);
            c.Parameters[4].Value = editMetadataForParameter.ValueTypeId;

            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();
            return qS;
        }

        internal QueryStatus DeleteMetadata(MetadataForParameter deleteIdMetadataForParameter)
        {
            var c = CreateQuery("EXEC [dbo].[DeleteMetadata] @Id");
            c.Parameters.Add("Id", SqlDbType.Int);
            c.Parameters[0].Value = deleteIdMetadataForParameter.Id;

            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<QueryStatus>>().Map(reader)[0];
            reader.Close();
            return qS;
        }

        internal MetadataForParameter GetMetdadataById(MetadataForParameter detailMetadata)
        {
            var c = CreateQuery("EXEC [dbo].[GetMetadataById] @Id");
            c.Parameters.Add("Id", SqlDbType.Int);
            c.Parameters[0].Value = detailMetadata.Id;

            var reader = c.ExecuteReader();

            var qS = Get<PropertyToColumnMapper<MetadataForParameter>>().Map(reader)[0];
            reader.Close();
            return qS;
        }
    }
}