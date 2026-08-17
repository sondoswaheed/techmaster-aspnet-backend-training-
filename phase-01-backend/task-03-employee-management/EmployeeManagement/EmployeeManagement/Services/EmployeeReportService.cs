using EmployeeManagement.Enums;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    internal class EmployeeReportService
    {
        private readonly List<Employee> _employees;

        public EmployeeReportService(List<Employee> employees)
        {
            _employees = employees;
        }

        public int GetTotalEmployees()
        {
            return _employees.Count;
        }

        public int GetActiveEmployees()
        {
            return _employees.Count(e => e.IsActive);
        }

        public int GetInactiveEmployees()
        {
            return _employees.Count(e => !e.IsActive);
        }

        public decimal GetAverageSalary()
        {
            if (_employees.Count == 0)
            {
                return 0;
            }

            return _employees.Average(e => e.Salary);
        }

        public decimal GetHighestSalary()
        {
            if (_employees.Count == 0)
            {
                return 0;
            }

            return _employees.Max(e => e.Salary);
        }

        public decimal GetLowestSalary()
        {
            if (_employees.Count == 0)
            {
                return 0;
            }

            return _employees.Min(e => e.Salary);
        }

        public decimal GetTotalSalary()
        {
            return _employees.Sum(e => e.Salary);
        }

        public int GetEmployeesByDepartment(Department department)
        {
            return _employees.Count(e => e.Department == department);
        }
    }
}