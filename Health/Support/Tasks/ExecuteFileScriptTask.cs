using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Support.Tasks
{
    internal class ExecuteFileScriptTask : ITask
    {
        private readonly string _fileName;
        internal ExecuteFileScriptTask(string fileName)
        {
            _fileName = fileName;
        }

        public void Process(SqlConnection connection)
        {
            SqlCommand preCommand = connection.CreateCommand();
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string exDir = directoryName.Replace(@"file:\", "");
            string fileDir = String.Empty;
            SupportManager sp = SupportManager.Instance();
            foreach (string path in sp.IncludePaths)
            {
                if (File.Exists(path + _fileName))
                {
                    fileDir = path + _fileName;
                }
            }
            FileInfo file = new FileInfo(fileDir);
            string script = file.OpenText().ReadToEnd();
            preCommand.CommandText = script;
            preCommand.ExecuteNonQuery();
        }
    }
}
