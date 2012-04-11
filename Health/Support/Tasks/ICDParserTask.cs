using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using Model.Entities;

namespace Support.Tasks
{
    internal class ICDParserTask : ITask
    {
        private readonly string _xlsFileName;
        private string _excelConnectionString;
        private Dictionary<string, string> _props;

        public ICDParserTask(string xlsFileName)
        {
            _xlsFileName = xlsFileName;
        }

        private void Parse(DbContext context)
        {
            using (var connection = new OleDbConnection(_excelConnectionString))
            {
                using (var adapter = new OleDbDataAdapter("select * from [Лист1$]", connection))
                {
                    var dataTable = new DataTable("ICD");
                    adapter.Fill(dataTable);
                    DataRow[] rows = dataTable.Select();
                    DbSet diagnosisClassSet = context.Set(typeof (DiagnosisClass));
                    DbSet diagnosisSet = context.Set(typeof (Diagnosis));
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                    foreach (DataRow row in rows)
                    {
                        string code1 = row[0].ToString();
                        string name1 = row[1].ToString();
                        string code2 = row[2].ToString().Replace("(", "").Replace(")", "");
                        string name2 = row[3].ToString();
                        string code3 = row[4].ToString().Replace("'", "");
                        string name3 = row[5].ToString().Replace("'", "");
                        if (name1 == String.Empty || name2 == String.Empty) break;
                        if (
                            !diagnosisClassSet.Local.Cast<DiagnosisClass>().Any(i => i.Name == name1 && i.Code == code1) &&
                            !diagnosisClassSet.Cast<DiagnosisClass>().Any(i => i.Name == name1 && i.Code == code1))
                        {
                            var dc = new DiagnosisClass
                                         {
                                             Name = row[1].ToString(),
                                             Code = row[0].ToString()
                                         };
                            diagnosisClassSet.Add(dc);
                            context.SaveChanges();
                        }
                        if (
                            !diagnosisClassSet.Local.Cast<DiagnosisClass>().Any(i => i.Name == name2 && i.Code == code2) &&
                            !diagnosisClassSet.Cast<DiagnosisClass>().Any(i => i.Name == name2 && i.Code == code2))
                        {
                            DiagnosisClass p =
                                diagnosisClassSet.Cast<DiagnosisClass>().FirstOrDefault(e => e.Code == code1);
                            var dc = new DiagnosisClass
                                         {
                                             Name = name2,
                                             Code = code2,
                                             ParentDiagnosisClass = p
                                         };
                            diagnosisClassSet.Add(dc);
                            context.SaveChanges();
                        }
                        DiagnosisClass dcc =
                            diagnosisClassSet.Cast<DiagnosisClass>().FirstOrDefault(e => e.Code == code2);
                        var d = new Diagnosis
                                    {
                                        Name = name3,
                                        Code = code3,
                                        DiagnosisClass = dcc
                                    };
                        diagnosisSet.Add(d);
                        if (diagnosisSet.Local.Count > 50)
                            context.SaveChanges();
                    }
                    context.SaveChanges();
                    context.Configuration.AutoDetectChangesEnabled = true;
                    context.Configuration.ValidateOnSaveEnabled = true;
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

        #region Implementation of ITask

        public void Process(DbContext context)
        {
            FindFile();
            Parse(context);
        }

        #endregion
    }
}