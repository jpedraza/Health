using System;
using System.IO;
using System.Reflection;
using Support.ExcelParser;
using System.Data.SqlClient;

namespace Support
{
    class Program
    {
        static void Main(string[] args)
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (directoryName != null)
            {
                string exDir = directoryName.Replace(@"file:\", "");
                string fileDir = exDir + @"\..\..\..\..\Materials\ICD10RUS.xls";
                Console.WriteLine(fileDir);
                var icdParser = new ICDParser(fileDir);
                icdParser.Parse();               
            }
           
            Console.ReadLine();
        }
    }
}
