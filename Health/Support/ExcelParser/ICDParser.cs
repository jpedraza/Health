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
    internal class ICDParser : ITask
    {
        private Dictionary<string, string> _props;
        private string _excelConnectionString;
        private readonly string _xlsFileName;

        public ICDParser(string xlsFileName)
        {
            _xlsFileName = xlsFileName;
        }

        private class DiagnosisClass
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        private void Parse(SqlConnection sqlConnection)
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
                    foreach (DataRow row in rows)
                    {
                        var stringBuilder = new StringBuilder();
                        if (row[1].ToString() == String.Empty || row[3].ToString() == String.Empty) break;
                        SqlCommand insertCommand = sqlConnection.CreateCommand();
                        if (inserted.Where(i => i.Name == row[0].ToString() && i.Code == row[1].ToString()).Count() == 0)
                        {
                            insertCommand.CommandText = String.Format(insert_format, row[1], row[0], "NULL");
                            insertCommand.ExecuteNonQuery();
                            //Console.WriteLine("Insert diagnosis class: {0}", row[1]);
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
                            //Console.WriteLine("Insert diagnosis class: {0}", row[2]);
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
                        //Console.WriteLine("Insert diagnosis: {0}", row[5]);
                    }
                }
            }
        }

        private void FindFile()
        {
            SupportManager sp = SupportManager.Instance();
            string currentPath = String.Empty;
            foreach (string path in sp.IncludePaths)
            {
                if (File.Exists(path + _xlsFileName))
                {
                    currentPath = path + _xlsFileName;
                    break;
                }
            }
            _props = new Dictionary<string, string>
                         {
                             {"Provider", "Microsoft.Jet.OLEDB.4.0"},
                             {"Data Source", currentPath},
                             {"Extended Properties", "Excel 8.0"}
                         };

            var stringBuilder = new StringBuilder();
            foreach (var prop in _props)
            {
                stringBuilder.AppendFormat("{0}={1};", prop.Key, prop.Value);
            }
            _excelConnectionString = stringBuilder.ToString();
        }

        public void Process(SqlConnection connection)
        {
            FindFile();
            Parse(connection);
        }
    }
}
