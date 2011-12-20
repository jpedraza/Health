using System;
using System.IO;
using System.Reflection;
using Support.ExcelParser;
using System.Data.SqlClient;
using System.Collections.Generic;
using Support.Tasks;

namespace Support
{
    class Program
    {
        static void Main(string[] args)
        {
            SupportManager sp = SupportManager.Instance();
            sp.ConnectionString = "Data Source=.;Initial Catalog=Health.MsSqlDatabase;User Id=user;Password=zzz;";
            sp.AddIncludePath(@"\..\..\..\..\Materials\");
            sp.AddIncludePath(@"\..\..\..\..\Health\Support\Scripts\");
            sp.AddIncludePath(@"\..\..\..\..\Health\Health.MsSqlDatabase\Scripts\Post-Deployment\");

            sp.AddTask("Очистка базы", new ExecuteFileScriptTask("ClearDatabase.sql"));
            sp.AddTask("Вставка диагнозов", new ICDParser("ICD10RUS.xls"));
            sp.AddTask("Вставка тестовых данных", new ExecuteFileScriptTask("TestData.sql"));
            sp.ProcessAvailableTasks();
        }
    }
}
