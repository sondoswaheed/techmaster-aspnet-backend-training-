using EmployeeManagement.Enums;
using System.Runtime.InteropServices;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Department Department { get; set; }

        public string Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; }

        public String? PhoneNumber { get; set; }
        public String? ManagerName { get; set; }
    }
}