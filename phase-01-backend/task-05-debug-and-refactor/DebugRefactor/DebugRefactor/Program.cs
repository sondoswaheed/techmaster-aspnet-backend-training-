using DebugRefactor.Services;
using DebugRefactor.UI;

namespace DebugRefactor
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                OrderCalculatorService calculator =new OrderCalculatorService();

                ConsoleMenu menu =new ConsoleMenu(calculator);

                menu.Start();
            }
        }
    }
