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
            /*Не абыть про валидацию*/

            /*
             *byte[] bytes = Encoding.UTF8.GetBytes(1231231.ToString());
             *
             * string s = Encoding.UTF8.GetString(new byte[] { 0, 2 });
             */
            if (data.IsValid())
            {
                try
                {
                    SqlCommand c = CreateQuery("EXEC NewParameter @nameParameter, @defaultValue");
                    c.Parameters.Add(new SqlParameter("nameParameter", SqlDbType.NVarChar));
                    c.Parameters[0].Value = data.Name;
                    c.Parameters.Add(new SqlParameter("defaultValue", SqlDbType.VarBinary));
                    c.Parameters[1].Value = Encoding.UTF8.GetBytes(data.DefaultValue);

                    SqlDataReader reader = c.ExecuteReader();
                    reader.Close();

                    return new QueryStatus { Status = 1, StatusMessage = "Успешно записано" };
                }
                catch (Exception exp)
                {
                    return new QueryStatus { Status = 0, StatusMessage = exp.Message };
                }
            }

            return new QueryStatus { Status = 0, StatusMessage = "Запись невозможна" };

            /*
             try
                            {
                                SqlCommand c = CreateQuery("EXEC NewParameter @nameParameter, @defaultValue");
                                c.Parameters.Add(new SqlParameter("nameParameter", SqlDbType.NVarChar));
                                c.Parameters[0].Value = data.Name;
                                c.Parameters.Add(new SqlParameter("defaultValue", SqlDbType.VarBinary));
                                c.Parameters[1].Value = Encoding.UTF8.GetBytes(data.DefaultValue);

                                SqlDataReader reader = c.ExecuteReader();
                                reader.Close();

                                SqlCommand c2 = CreateQuery("EXEC NewParameterMetadata @ParameterId, @Key, @Value, @ValueTypeId");
                                c2.Parameters.Add(new SqlParameter("ParameterId", SqlDbType.Int) { Value = data.ParameterId });
                                c2.Parameters.Add(new SqlParameter("Key", SqlDbType.NVarChar) { Value = metadata.Key });
                                c2.Parameters.Add(new SqlParameter("Value", SqlDbType.VarBinary) { Value = Encoding.UTF8.GetBytes(metadata.Value.ToString()) });
                                c2.Parameters.Add(new SqlParameter("ValueTypeId", SqlDbType.Int) { Value = metadata.ValueType.ValueTypeId });

                                reader = c2.ExecuteReader();
                                reader.Close();
                            }
                            catch (Exception exp2)
                            {
                                return new QueryStatus { Status = 0, StatusMessage = exp2.Message };
                            }
                        }
             */
        }

        internal QueryStatus Update(ParameterDetail data)
        {
            return new QueryStatus();
        }
    }
}