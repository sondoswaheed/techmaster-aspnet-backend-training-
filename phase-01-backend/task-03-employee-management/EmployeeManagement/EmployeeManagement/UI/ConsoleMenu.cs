using EmployeeManagement.Enums;
using EmployeeManagement.Models;
using EmployeeManagement.Services;

namespace EmployeeManagement.UI
{
    internal class ConsoleMenu
    {
        private readonly EmployeeService _employeeService;

        public ConsoleMenu(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void ShowMenu()
        {
            while (true)
            {

                Console.WriteLine("==============================");
                Console.WriteLine("   EMPLOYEE MANAGEMENT SYSTEM");
                Console.WriteLine("==============================");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Deactivate Employee");
                Console.WriteLine("4. View All Employees");
                Console.WriteLine("5. Search Employee");
                Console.WriteLine("6. Filter Employees");
                Console.WriteLine("7. Sort Employees");
                Console.WriteLine("8. Employee Reports");
                Console.WriteLine("0. Exit");
                Console.WriteLine("==============================");

                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;

                    case "2":
                        UpdateEmployee();
                        break;

                    case "3":
                        DeactivateEmployee();
                        break;

                    case "4":
                        ViewAllEmployees();
                        break;

                    case "5":
                        SearchEmployee();
                        break;

                    case "6":
                        FilterEmployees();
                        break;

                    case "7":
                        SortEmployees();
                        break;

                    case "8":
                        ShowReports();
                        break;

                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }


        private void AddEmployee()
        {
            Console.WriteLine("========== Add Employee ==========");

            Console.Write("Full Name: ");
            string fullname = Console.ReadLine() ?? "";

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.WriteLine("Select Department:");
            Console.WriteLine("1. HR");
            Console.WriteLine("2. IT");
            Console.WriteLine("3. Finance");
            Console.WriteLine("4. Marketing");
            Console.WriteLine("5. Sales");
            Console.WriteLine("6. Operations");
            Console.WriteLine("7. Support");


            Console.Write("Choose Department: ");
            string departmentChoice = Console.ReadLine() ?? "";

            Department department;

            switch (departmentChoice)
            {
                case "1":
                    department = Department.HR;
                    break;

                case "2":
                    department = Department.IT;
                    break;

                case "3":
                    department = Department.Finance;
                    break;

                case "4":
                    department = Department.Marketing;
                    break;

                case "5":
                    department = Department.Sales;
                    break;

                case "6":
                    department = Department.Operations;
                    break;

                case "7":
                    department = Department.Support;
                    break;

                default:
                    Console.WriteLine("Invalid department.");
                    return;
            }

            Console.Write("Position: ");
            string position = Console.ReadLine() ?? "";

            Console.Write("Salary: ");
            decimal salary;

            if (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid salary.");
                return;
            }

            Console.Write("Hire Date (yyyy-MM-dd): ");
            DateTime hireDate;

            if (!DateTime.TryParse(Console.ReadLine(), out hireDate))
            {
                Console.WriteLine("Invalid hire date.");
                return;
            }

             _employeeService.AddEmployee(
                fullname,
                email,
                department,
                position,
                salary,
                hireDate,
                out string message
            );

            Console.WriteLine(message);
        }
        private void ViewAllEmployees()
        {

            Console.WriteLine("========== All Employees ==========");

            List<Employee> employees = _employeeService.GetAllEmployees();

            DisplayEmployees(employees);
        }

        private void SearchEmployee()
        {

            Console.WriteLine("========== Search Employee ==========");

            Console.Write("Enter name or email: ");
            string keyword = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Search keyword is required.");
                return;
            }

            List<Employee> employees = _employeeService.SearchEmployees(keyword);


            DisplayEmployees(employees);
        }

        private void UpdateEmployee()
        {

            Console.WriteLine("========== Update Employee ==========");

            Console.Write("Enter Employee ID: ");
           

            string employeeId = Console.ReadLine() ?? "";

            Employee? employee = _employeeService.GetEmployeeById(employeeId);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Current Employee Data");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"ID: {employee.EmployeeId}");
            Console.WriteLine($"Name: {employee.FullName}");
            Console.WriteLine($"Email: {employee.Email}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Position: {employee.Position}");
            Console.WriteLine($"Salary: {employee.Salary}");
            Console.WriteLine($"Hire Date: {employee.HireDate:yyyy-MM-dd}");
            Console.WriteLine($"Status: {(employee.IsActive ? "Active" : "Inactive")}");
            Console.WriteLine("--------------------------------");

            Console.WriteLine();
            Console.WriteLine("What do you want to update?");
            Console.WriteLine("1. Full Name");
            Console.WriteLine("2. Email");
            Console.WriteLine("3. Department");
            Console.WriteLine("4. Position");
            Console.WriteLine("5. Salary");
            Console.WriteLine("6. Hire Date");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int fieldChoice))
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            if (fieldChoice == 0)
            {
                return;
            }

            string newValue;

            if (fieldChoice == 3)
            {
                Console.WriteLine();
                Console.WriteLine("1. HR");
                Console.WriteLine("2. IT");
                Console.WriteLine("3. Finance");
                Console.WriteLine("4. Marketing");
                Console.WriteLine("5. Sales");
                Console.WriteLine("6. Operations");
                Console.WriteLine("7. Support");

                Console.Write("Choose new department: ");
                newValue = Console.ReadLine() ?? "";
            }
            else
            {
                Console.Write("Enter new value: ");
                newValue = Console.ReadLine() ?? "";
            }

            bool success = _employeeService.UpdateEmployee(
                employeeId,
                fieldChoice,
                newValue,
                out string message
            );

            if (success)
            {
                Console.WriteLine($"Success: {message}");
            }
            else
            {
                Console.WriteLine($"Error: {message}");
            }
        }

        private void DeactivateEmployee()
        {

            Console.WriteLine("========== Deactivate Employee ==========");

            Console.Write("Enter Employee ID: ");

            string employeeId=Console.ReadLine() ?? "";

            bool success = _employeeService.DeactivateEmployee(
                employeeId,
                out string message
            );

            if (success)
            {
                Console.WriteLine($"Success: {message}");
            }
            else
            {
                Console.WriteLine($"Error: {message}");
            }
        }

        private void FilterEmployees()
        {

            Console.WriteLine("========== Filter Employees ==========");
            Console.WriteLine("1. By Department");
            Console.WriteLine("2. By Status");
            Console.WriteLine("3. By Department and Status");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Department? department = null;
            bool? isActive = null;

            switch (choice)
            {
                case 1:
                    department = ReadDepartment();
                    break;

                case 2:
                    isActive = ReadStatus();
                    break;

                case 3:
                    department = ReadDepartment();
                    isActive = ReadStatus();
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            List<Employee> employees =_employeeService.FilterEmployees(department, isActive);

            DisplayEmployees(employees);
        }

        private bool? ReadStatus()
        {
            Console.WriteLine();
            Console.WriteLine("1. Active");
            Console.WriteLine("2. Inactive");

            Console.Write("Choose Status: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid status.");
                return null;
            }

            switch (choice)
            {
                case 1:
                    return true;

                case 2:
                    return false;

                default:
                    Console.WriteLine("Invalid status.");
                    return null;
            }
        }

        private Department? ReadDepartment()
        {
            Console.WriteLine();
            Console.WriteLine("1. HR");
            Console.WriteLine("2. IT");
            Console.WriteLine("3. Finance");
            Console.WriteLine("4. Marketing");
            Console.WriteLine("5. Sales");
            Console.WriteLine("6. Operations");
            Console.WriteLine("7. Support");

            Console.Write("Choose Department: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid department.");
                return null;
            }

            switch (choice)
            {
                case 1:
                    return Department.HR;

                case 2:
                    return Department.IT;

                case 3:
                    return Department.Finance;

                case 4:
                    return Department.Marketing;

                case 5:
                    return Department.Sales;

                case 6:
                    return Department.Operations;
                case 7:
                    return Department.Support;

                default:
                    Console.WriteLine("Invalid department.");
                    return null;
            }
        }

        private void DisplayEmployees(List<Employee> employees)
        {
            Console.WriteLine();

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
                return;
            }

            foreach (Employee employee in employees)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"ID: {employee.EmployeeId}");
                Console.WriteLine($"Name: {employee.FullName}");
                Console.WriteLine($"Email: {employee.Email}");
                Console.WriteLine($"Department: {employee.Department}");
                Console.WriteLine($"Position: {employee.Position}");
                Console.WriteLine($"Salary: {employee.Salary}");
                Console.WriteLine($"Hire Date: {employee.HireDate:yyyy-MM-dd}");
                Console.WriteLine($"Status: {(employee.IsActive ? "Active" : "Inactive")}");
            }

            Console.WriteLine("--------------------------------");
        }

        private void SortEmployees()
        {

            Console.WriteLine("========== Sort Employees ==========");
            Console.WriteLine("1. Name A-Z");
            Console.WriteLine("2. Salary High-Low");
            Console.WriteLine("3. Salary Low-High");
            Console.WriteLine("4. Hire Date Oldest-Newest");
            Console.WriteLine("5. Hire Date Newest-Oldest");
            Console.WriteLine("0. Back");

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            if (choice == 0)
            {
                return;
            }

            if (choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            List<Employee> employees =
                _employeeService.SortEmployees(choice);

            DisplayEmployees(employees);
        }

        private void ShowReports()
        {

            Console.WriteLine("========== Employee Reports ==========");

            List<Employee> employees =_employeeService.GetAllEmployees();

            EmployeeReportService reportService =new EmployeeReportService(employees);

            Console.WriteLine($"Total Employees: {reportService.GetTotalEmployees()}");
            Console.WriteLine($"Active Employees: {reportService.GetActiveEmployees()}");
            Console.WriteLine($"Inactive Employees: {reportService.GetInactiveEmployees()}");
            Console.WriteLine($"Average Salary: {reportService.GetAverageSalary():F2}");
            Console.WriteLine($"Highest Salary: {reportService.GetHighestSalary():F2}");
            Console.WriteLine($"Lowest Salary: {reportService.GetLowestSalary():F2}");
            Console.WriteLine($"Total Salaries: {reportService.GetTotalSalary():F2}");

            Console.WriteLine();
            Console.WriteLine("Employees By Department:");

            foreach (Department department in Enum.GetValues<Department>())
            {
                int count =reportService.GetEmployeesByDepartment(department);

                Console.WriteLine($"{department}: {count}");
            }
        }
    }
}