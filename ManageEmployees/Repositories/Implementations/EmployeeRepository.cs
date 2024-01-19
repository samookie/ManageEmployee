using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    /// <summary>
    /// La classe repository de l'employée
    /// </summary>
    /// <seealso cref="ManageEmployees.Repositories.Contracts.IEmployeeRepository" />
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly ManageEmployeeDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EmployeeRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }
        
        /// <summary>
        /// Gets the employee by identifier asynchronous t.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _dbContext.Employees
                .Include(e => e.Attendances)
                .Include(e => e.EmployeeDepartments)
                .Include(e => e.LeaveRequests)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employeeToCreate">The employee to create.</param>
        /// <returns></returns>
        public async Task<Employee> CreateEmployeeAsync(Employee employeeToCreate)
        {
            await _dbContext.Employees.AddAsync(employeeToCreate);
            await _dbContext.SaveChangesAsync();
            return employeeToCreate;
        }
        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeToUpdate">The employee to update.</param>
        public async Task UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _dbContext.Employees.Update(employeeToUpdate);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the employee by identifier asynchronous t.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        public async Task DeleteEmployeeByIdAsync(int employeeId)
        {
            var employee = await GetEmployeeByIdAsync(employeeId);
            _dbContext.Attendances.RemoveRange(employee.Attendances);
            _dbContext.EmployeeDepartments.RemoveRange(employee.EmployeeDepartments);
            _dbContext.LeaveRequests.RemoveRange(employee.LeaveRequests);
            _dbContext.Employees.Remove(employee);

            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Gets the employee byemail asynchronous.
        /// </summary>
        /// <param name="employeeEmail">The employee email.</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByemailAsync(string employeeEmail)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == employeeEmail);
        }
        /// <summary>
        /// Adds the employee to department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<EmployeeDepartment> AddEmployeeToDepartmentAsync(int employeeId, int departmentId)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(d => d.EmployeeId == employeeId);
            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

            var employeeDepartment = new EmployeeDepartment { EmployeeId = employeeId, DepartmentId = departmentId };

            _dbContext.EmployeeDepartments.Add(employeeDepartment);
            await _dbContext.SaveChangesAsync();

            return employeeDepartment;

        }
        /// <summary>
        /// Gets the employee department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<EmployeeDepartment> GetEmployeeDepartmentAsync(int employeeId, int departmentId)
        {
            return await _dbContext.EmployeeDepartments.FirstOrDefaultAsync(ed => ed.EmployeeId == employeeId && ed.DepartmentId == departmentId);
        }
        /// <summary>
        /// Deletes the employee from department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        public async Task DeleteEmployeeFromDepartmentAsync(int employeeId, int departmentId)
        {
            var employeeDepartment = await GetEmployeeDepartmentAsync(employeeId, departmentId);

            _dbContext.EmployeeDepartments.Remove(employeeDepartment);
            await _dbContext.SaveChangesAsync();
        }

    }

}
