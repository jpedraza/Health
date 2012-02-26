using System;
using System.Data;
using System.Data.Entity;
using System.IO;

namespace Support.Tasks
{
    internal class ExecuteFileScriptTask : ITask
    {
        private readonly string _fileName;

        internal ExecuteFileScriptTask(string fileName)
        {
            _fileName = fileName;
        }

        #region Implementation of ITask

        public void Process(DbContext context)
        {
            IDbCommand preCommand = context.Database.Connection.CreateCommand();
            string fileDir = String.Empty;
            SupportManager sp = SupportManager.Instance();
            foreach (string path in sp.IncludePaths)
            {
                if (File.Exists(path + _fileName))
                {
                    fileDir = path + _fileName;
                }
            }
            var file = new FileInfo(fileDir);
            string script = file.OpenText().ReadToEnd();
            preCommand.CommandText = script;
            preCommand.ExecuteNonQuery();
        }

        #endregion
    }
}