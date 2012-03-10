using System;
using EFCFModel;
using Support.Tasks;

namespace Support
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = Console.WindowHeight*2;
            Console.WindowWidth = Console.WindowWidth*2;
            Console.BufferHeight = Console.BufferHeight*2;
            Console.BufferWidth = Console.BufferWidth*2;
            SupportManager sp = SupportManager.Instance(new EFHealthContext());
            sp.AddIncludePath(@"\..\..\..\..\Materials\");
            sp.AddIncludePath(@"\..\..\..\..\Health\Support\Scripts\");

            sp.AddTask("Вставка диагнозов", new ICDParserTask("ICD10RUS.xls"));
            sp.AddTask("Вставка тестовых данных", new TestDataTask());
            sp.ProcessAvailableTasks();
        }
    }
}