using EmployeeManagement.Services;
using EmployeeManagement.UI;

namespace EmployeeManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeService employeeService = new EmployeeService();
            employeeService.SeedEmployees();

            ConsoleMenu menu = new ConsoleMenu(employeeService);

            menu.ShowMenu();
        }
    }
}
