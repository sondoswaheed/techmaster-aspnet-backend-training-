using EmployeeManagement.Enums;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{
    internal class EmployeeService
    {
        private readonly List<Employee> _employees = new();

        public bool AddEmployee(string fullname, string email, Department dept, string position, decimal salary, DateTime hiredate, out string message)
        {
            if (string.IsNullOrWhiteSpace(fullname))
            {
                message = "FullName is required";
                return false;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                message = "email is required";
                return false;
            }
            if (string.IsNullOrWhiteSpace(position))
            {
                message = "position is required";
                return false;
            }
            
            if (salary <= 0)
            {
                message = "Salary must be positive ";
                return false;
            }
            if (hiredate > DateTime.Now)
            {
                message = "Hire date can't be in the future.";
                return false;
            }
            string employeeId = $"EMP-{_employees.Count + 1:000}";


            Employee employee = new Employee
            {
                EmployeeId = employeeId,
                FullName=fullname,
                Email=email,
                HireDate=hiredate,
                IsActive=true,
                Salary=salary,
                Position=position,
                Department=dept
            };
            _employees.Add(employee);

            message = "Employee added successfully";
            return true;

        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public List<Employee> SearchEmployees(string keyword)
        {
            return _employees.Where(e => e.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            e.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Employee? GetEmployeeById( string employeeId)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }


        public bool UpdateEmployee(string employeeId,int fieldChoice,string newValue,out string message)
        {
            Employee? employee = GetEmployeeById(employeeId);

            if (employee == null)
            {
                message = "Employee not found.";
                return false;
            }

            switch (fieldChoice)
            {
                case 1:
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        message = "Full name is required.";
                        return false;
                    }

                    employee.FullName = newValue;
                    break;

                case 2:
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        message = "Email is required.";
                        return false;
                    }

                    employee.Email = newValue;
                    break;

                case 3:
                    if (!int.TryParse(newValue, out int departmentChoice) ||
                        departmentChoice < 1 ||
                        departmentChoice > 6)
                    {
                        message = "Invalid department.";
                        return false;
                    }

                    employee.Department = (Department)(departmentChoice - 1);
                    break;

                case 4:
                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        message = "Position is required.";
                        return false;
                    }

                    employee.Position = newValue;
                    break;

                case 5:
                    if (!decimal.TryParse(newValue, out decimal salary) || salary <= 0)
                    {
                        message = "Salary must be positive.";
                        return false;
                    }

                    employee.Salary = salary;
                    break;

                case 6:
                    if (!DateTime.TryParse(newValue, out DateTime hireDate))
                    {
                        message = "Invalid hire date.";
                        return false;
                    }

                    if (hireDate > DateTime.Now)
                    {
                        message = "Hire date can't be in the future.";
                        return false;
                    }

                    employee.HireDate = hireDate;
                    break;

                default:
                    message = "Invalid update option.";
                    return false;
            }

            message = "Employee updated successfully.";
            return true;
        }

        public bool DeactivateEmployee(string employeeId, out string message)
        {
            Employee? employee = _employees
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
            {
                message = "Employee not found.";
                return false;
            }

            if (!employee.IsActive)
            {
                message = "Employee is already inactive.";
                return false;
            }

            employee.IsActive = false;

            message = "Employee deactivated successfully.";
            return true;
        }

        public List<Employee> FilterEmployees(Department? department , bool? isActive)
        {
            IEnumerable<Employee> employees = _employees;

            if (department.HasValue)
            {
                employees=employees.Where(e=>e.Department == department.Value);
            }
            if (isActive.HasValue)
            {
                employees =employees.Where(e=>e.IsActive== isActive.HasValue);
            }
            return employees.ToList();
        }

        public List<Employee> SortEmployees(int sortChoice)
        {
            return sortChoice switch
            {
                1 => _employees.OrderBy(e => e.FullName).ToList(),

                2 => _employees.OrderByDescending(e => e.Salary).ToList(),

                3 => _employees.OrderBy(e => e.Salary).ToList(),

                4 => _employees.OrderBy(e => e.HireDate).ToList(),

                5 => _employees.OrderByDescending(e => e.HireDate).ToList(),

                _ => new List<Employee>()
            };
        }



        public void SeedEmployees()
        {
            _employees.AddRange(new List<Employee>
            {
                new Employee
                {
                    EmployeeId = "EMP-001",
                    FullName = "Mohamed Ayman",
                    Email = "mohamed@test.com",
                    Department = Department.IT,
                    Position = "Backend Developer",
                    Salary = 20000,
                    HireDate = new DateTime(2025, 1, 10),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-002",
                    FullName = "Sara Adel",
                    Email = "sara@test.com",
                    Department = Department.HR,
                    Position = "HR Specialist",
                    Salary = 12000,
                    HireDate = new DateTime(2024, 5, 15),
                     IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-003",
                    FullName = "Ahmed Tarek",
                    Email = "ahmed@test.com",
                    Department = Department.IT,
                    Position = "Junior Developer",
                    Salary = 9000,
                    HireDate = new DateTime(2026, 1, 1),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-004",
                    FullName = "Omar Samir",
                    Email = "omar@test.com",
                    Department = Department.Sales,
                    Position = "Sales Executive",
                    Salary = 11000,
                    HireDate = new DateTime(2023, 11, 20),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-005",
                     FullName = "Mariam Hassan",
                    Email = "mariam@test.com",
                    Department = Department.Finance,
                    Position = "Accountant",
                    Salary = 14000,
                    HireDate = new DateTime(2022, 9, 11),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-006",
                    FullName = "Khaled Ali",
                    Email = "khaled@test.com",
                    Department = Department.IT,
                    Position = "DevOps Trainee",
                    Salary = 10000,
                    HireDate = new DateTime(2026, 2, 1),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-007",
                    FullName = "Nour Emad",
                    Email = "nour@test.com",
                    Department = Department.Marketing,
                    Position = "Content Specialist",
                    Salary = 9500,
                    HireDate = new DateTime(2025, 7, 8),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-008",
                    FullName = "Youssef Nabil",
                    Email = "youssef@test.com",
                    Department = Department.Sales,
                    Position = "Sales Manager",
                    Salary = 18000,
                    HireDate = new DateTime(2021, 3, 17),
                    IsActive = false
                },

                new Employee
                {
                    EmployeeId = "EMP-009",
                    FullName = "Dina Farouk",
                    Email = "dina@test.com",
                    Department = Department.HR,
                    Position = "Recruiter",
                    Salary = 10500,
                    HireDate = new DateTime(2024, 2, 13),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-010",
                    FullName = "Hady Mahmoud",
                    Email = "hady@test.com",
                    Department = Department.IT,
                    Position = "QA Engineer",
                    Salary = 13000,
                    HireDate = new DateTime(2025, 10, 1),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-011",
                    FullName = "Salma Taha",
                    Email = "salma@test.com",
                    Department = Department.Finance,
                    Position = "Finance Manager",
                    Salary = 26000,
                    HireDate = new DateTime(2020, 12, 12),
                    IsActive = true
                },

                new Employee
                {
                    EmployeeId = "EMP-012",
                    FullName = "Ali Mostafa",
                    Email = "ali@test.com",
                    Department = Department.Support,
                    Position = "Support Agent",
                    Salary = 8000,
                    HireDate = new DateTime(2026, 3, 5),
                    IsActive = true
                }
            });
        }
    }
}
