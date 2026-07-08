using RoadCompany.Models;
using RoadCompany.UI.UC;
using RoadCompany.UI.Views;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RoadCompany.UI.Pages
{
    public partial class OrganizationStructurePg : Page
    {
        public OrganizationStructurePg()
        {
            InitializeComponent();
            DataContext = this;
            try
            {
                LoadOrganizationalStructure();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to the database: {ex.Message}");
            }
        }
        private void LoadOrganizationalStructure()
        {
            subdivisionContainer.Children.Clear();
            departmentContainer.Children.Clear();
            subdepartmentContainer.Children.Clear();

            using (var db = new RoadEntities())
            {
                var subdivisions = db.Subdivision.ToList();
                var depatrments = db.Department.ToList();
                var subdepartments = db.SubDepartment.ToList();

                foreach (var subdivision in subdivisions)
                {
                    var structuralVw = new StructureVw(subdivision.Name, subdivision.Id);
                    structuralVw.Click += StructuralVw_Click;
                    subdivisionContainer.Children.Add(structuralVw);
                }

                foreach (var department in depatrments)
                {
                    var structuralVw = new StructureVw(department.DepartmentName, department.Id);
                    structuralVw.Click += StructuralVw_Click1;
                    departmentContainer.Children.Add(structuralVw);
                }

                foreach (var subdepartment in subdepartments)
                {
                    var structuralVw = new StructureVw(subdepartment.Name, subdepartment.Id);
                    structuralVw.Click += StructuralVw_Click2;
                    subdepartmentContainer.Children.Add(structuralVw);
                }
            }
        }
        private void StructuralVw_Click(object sender, EventArgs e)
        {
            if (sender is StructureVw structuralVw)
            {
                int structuralId = structuralVw.structuralId;
                LoadEmployeesForSubdivision(structuralId);
            }
        }

        private void StructuralVw_Click1(object sender, EventArgs e)
        {
            if (sender is StructureVw structuralVw)
            {
                int structuralId = structuralVw.structuralId;
                LoadEmployeesForDepartment(structuralId);
            }
        }
        private void StructuralVw_Click2(object sender, EventArgs e)
        {
            if (sender is StructureVw structuralVw)
            {
                int structuralId = structuralVw.structuralId;
                LoadEmployeesForSubDepartment(structuralId);
            }
        }

        private void LoadEmployeesForSubdivision(int structuralId)
        {
            employeeContainer.Children.Clear();
            using (var db = new RoadEntities())
            {
                var employees = (from emp in db.Employee
                                 join p in db.Person on emp.PersonId equals p.Id
                                 join pos in db.Position on emp.JobId equals pos.Id
                                 join parlor in db.Parlor on emp.ParlorId equals parlor.Id
                                 join empdep in db.EmployeeDepartment on emp.Id equals empdep.EmployeeId
                                 join dep in db.Department on empdep.DepartmentId equals dep.Id
                                 join subdep in db.SubdDepartment on dep.Id equals subdep.DepartmentId
                                 join sub in db.Subdivision on subdep.SubdivisionId equals sub.Id
                                 where subdep.SubdivisionId == structuralId
                                 select new
                                 {
                                     emp.Id,
                                     p.FullName,
                                     sub.Name,
                                     pos.PositionName,
                                     emp.WorkPhone,
                                     parlor.ParlorName,
                                     emp.Email,
                                 }).ToList();

                foreach (var employee in employees)
                {
                    var employeeVw = new EmployeeVw(employee.Id, employee.FullName, employee.Name, employee.PositionName, employee.WorkPhone, employee.ParlorName, employee.Email);
                    employeeVw.Click += EmployeeCardBtn_Click;
                    employeeContainer.Children.Add(employeeVw);
                }
            }
        }
        private void EmployeeCardBtn_Click(object sender, EventArgs e)
        {
            if (sender is EmployeeVw selectedEmployee)
            {
                EmployeeCard detailsWindow = new EmployeeCard(selectedEmployee);
                detailsWindow.Show();
            }
        }
        private void LoadEmployeesForDepartment(int structuralId)
        {
            employeeContainer.Children.Clear();
            using (var db = new RoadEntities())
            {
                var employees = (from emp in db.Employee
                                 join p in db.Person on emp.PersonId equals p.Id
                                 join pos in db.Position on emp.JobId equals pos.Id
                                 join parlor in db.Parlor on emp.ParlorId equals parlor.Id
                                 join empdep in db.EmployeeDepartment on emp.Id equals empdep.EmployeeId
                                 join dep in db.Department on empdep.DepartmentId equals dep.Id
                                 join subdep in db.SubdDepartment on dep.Id equals subdep.DepartmentId
                                 join sub in db.Subdivision on subdep.SubdivisionId equals sub.Id
                                 where empdep.DepartmentId == structuralId
                                 select new
                                 {
                                     emp.Id,
                                     p.FullName,
                                     dep.DepartmentName,
                                     pos.PositionName,
                                     emp.WorkPhone,
                                     parlor.ParlorName,
                                     emp.Email,
                                 }).ToList();

                foreach (var employee in employees)
                {
                    var employeeVw = new EmployeeVw(employee.Id, employee.FullName, employee.DepartmentName, employee.PositionName, employee.WorkPhone, employee.ParlorName, employee.Email);
                    employeeVw.Click += EmployeeCardBtn_Click;
                    employeeContainer.Children.Add(employeeVw);
                }
            }
        }
        private void LoadEmployeesForSubDepartment(int structuralId)
        {
            employeeContainer.Children.Clear();

            using (var db = new RoadEntities())
            {
                var employees = (from emp in db.Employee
                                 join p in db.Person on emp.PersonId equals p.Id
                                 join pos in db.Position on emp.JobId equals pos.Id
                                 join parlor in db.Parlor on emp.ParlorId equals parlor.Id
                                 join empdep in db.EmployeeDepartment on emp.Id equals empdep.EmployeeId
                                 join dep in db.Department on empdep.DepartmentId equals dep.Id
                                 join subdep in db.SubdDepartment on dep.Id equals subdep.DepartmentId
                                 join sub in db.Subdivision on subdep.SubdivisionId equals sub.Id
                                 join subddep in db.SubDepartment on empdep.SubDepartmentId equals subddep.Id
                                 where empdep.SubDepartmentId == structuralId
                                 select new
                                 {
                                     emp.Id,
                                     p.FullName,
                                     subddep.Name,
                                     pos.PositionName,
                                     emp.WorkPhone,
                                     parlor.ParlorName,
                                     emp.Email,
                                 }).ToList();

                foreach (var employee in employees)
                {
                    var employeeVw = new EmployeeVw(employee.Id, employee.FullName, employee.Name, employee.PositionName, employee.WorkPhone, employee.ParlorName, employee.Email);
                    employeeVw.Click += EmployeeCardBtn_Click;
                    employeeContainer.Children.Add(employeeVw);
                }
            }
        }
        private void CompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employeeContainer.Children.Clear();
                using (var db = new RoadEntities())
                {
                    var employees = (from emp in db.Employee
                                     join p in db.Person on emp.PersonId equals p.Id
                                     join pos in db.Position on emp.JobId equals pos.Id
                                     join parlor in db.Parlor on emp.ParlorId equals parlor.Id
                                     join empdep in db.EmployeeDepartment on emp.Id equals empdep.EmployeeId
                                     join dep in db.Department on empdep.DepartmentId equals dep.Id
                                     join subdep in db.SubdDepartment on dep.Id equals subdep.DepartmentId
                                     join sub in db.Subdivision on subdep.SubdivisionId equals sub.Id
                                     select new
                                     {
                                         emp.Id,
                                         p.FullName,
                                         sub.Name,
                                         pos.PositionName,
                                         emp.WorkPhone,
                                         parlor.ParlorName,
                                         emp.Email,
                                     }).ToList();

                    foreach (var employee in employees)
                    {
                        var employeeVw = new EmployeeVw(employee.Id, employee.FullName, employee.Name, employee.PositionName, employee.WorkPhone, employee.ParlorName, employee.Email);
                        employeeVw.Click += EmployeeCardBtn_Click;
                        employeeContainer.Children.Add(employeeVw);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to the database: {ex.Message}");
            }
        }

        private void CreateEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeCard detailsWindow = new EmployeeCard(null);
            detailsWindow.Show();
        }
    }
}
