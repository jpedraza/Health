using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Support.ExcelParser
{
    internal class ICDParser
    {
        private readonly Dictionary<string, string> _props;
        private readonly string _excelConnectionString;
        private readonly string _sqlConnectionString;

        public ICDParser(string filePath)
        {
            _props = new Dictionary<string, string>
                         {
                             {"Provider", "Microsoft.Jet.OLEDB.4.0"},
                             {"Data Source", filePath},
                             {"Extended Properties", "Excel 8.0"}
                         };

            var stringBuilder = new StringBuilder();
            foreach (var prop in _props)
            {
                stringBuilder.AppendFormat("{0}={1};", prop.Key, prop.Value);
            }
            _excelConnectionString = stringBuilder.ToString();
            _sqlConnectionString =
                "Data Source=.;Initial Catalog=Health.MsSqlDatabase;User Id=user;Password=zzz;";
        }

        private class DiagnosisClass
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public void Parse()
        {
            using (var connection = new OleDbConnection(_excelConnectionString))
            {
                using (var adapter = new OleDbDataAdapter("select * from [Лист1$]", connection))
                {
                    var dataTable = new DataTable("ICD");
                    adapter.Fill(dataTable);
                    DataRow[] rows = dataTable.Select();
                    const string insert_format =
                        "insert into [dbo].[DiagnosisClass] (Name, Code, Parent) values ('{0}', '{1}', {2})";
                    var inserted = new List<DiagnosisClass>();
                    using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                    {
                        sqlConnection.Open();
                        SqlCommand preCommand = sqlConnection.CreateCommand();
                        preCommand.CommandText = "delete from [dbo].[Diagnosis]";
                        preCommand.ExecuteNonQuery();
                        preCommand.CommandText = "delete from [dbo].[DiagnosisClass]";
                        preCommand.ExecuteNonQuery();
                        preCommand.CommandText = "DBCC CHECKIDENT([Diagnosis], RESEED, 0)";
                        preCommand.ExecuteNonQuery();
                        preCommand.CommandText = "DBCC CHECKIDENT([DiagnosisClass], RESEED, 0)";
                        preCommand.ExecuteNonQuery();
                        foreach (DataRow row in rows)
                        {
                            if (row[1].ToString() == String.Empty || row[3].ToString() == String.Empty) break;
                            SqlCommand insertCommand = sqlConnection.CreateCommand();
                            if (inserted.Where(i => i.Name == row[0].ToString() && i.Code == row[1].ToString()).Count() == 0)
                            {
                                insertCommand.CommandText = String.Format(insert_format, row[1], row[0], "NULL");
                                insertCommand.ExecuteNonQuery();
                                Console.WriteLine("Insert diagnosis class: {0}", row[1]);
                                inserted.Add(new DiagnosisClass { Name = row[0].ToString(), Code = row[1].ToString() });
                            }
                            if (inserted.Where(i => i.Name == row[2].ToString() && i.Code == row[3].ToString()).Count() == 0)
                            {
                                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                                sqlCommand.CommandText =
                                    String.Format(
                                        "select DiagnosisClassId from [dbo].[DiagnosisClass] where Code = '{0}'", row[0]);
                                uint parent = Convert.ToUInt32(sqlCommand.ExecuteScalar());
                                insertCommand.CommandText = String.Format(insert_format, row[3], row[2].ToString().Replace("(", "").Replace(")", ""), parent);
                                insertCommand.ExecuteNonQuery();
                                Console.WriteLine("Insert diagnosis class: {0}", row[2]);
                                inserted.Add(new DiagnosisClass{ Name = row[2].ToString(), Code = row[3].ToString()});
                            }
                            SqlCommand selectCommand = sqlConnection.CreateCommand();
                            selectCommand.CommandText = String.Format(
                                "select DiagnosisClassId from [dbo].[DiagnosisClass] where Code = '{0}'", row[2].ToString().Replace("(", "").Replace(")", ""));
                            var diagnosisClassid = Convert.ToUInt32(selectCommand.ExecuteScalar());
                            insertCommand.CommandText = String.Format(
                                "insert into [dbo].[Diagnosis] (Name, Code, DiagnosisClassId) values ('{0}', '{1}', {2})",
                                row[5].ToString().Replace("'", ""), row[4].ToString().Replace("'", ""), diagnosisClassid);
                            insertCommand.ExecuteNonQuery();
                            Console.WriteLine("Insert diagnosis: {0}", row[5]);
                        }
                    }
                }
            }
            
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                sqlConnection.Open();
                SqlCommand preCommand = sqlConnection.CreateCommand();
                string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                string exDir = directoryName.Replace(@"file:\", "");
                string fileDir = exDir + @"\..\..\..\..\Health\Health.MsSqlDatabase\Scripts\Post-Deployment\SetDiagnosis.sql";
                FileInfo file = new FileInfo(fileDir);
                string script = file.OpenText().ReadToEnd();
                preCommand.CommandText = script;
                preCommand.ExecuteNonQuery();
            }
        }
    }
}
