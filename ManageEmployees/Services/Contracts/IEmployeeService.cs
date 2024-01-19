using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;

namespace ManageEmployees.Services.Contracts
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        Task<CreateEmployee> CreateEmployeeAsync(CreateEmployee employee);
        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadEmployee>> GetEmployees();
        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId);
        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="newEmployee">The new employee.</param>
        /// <returns></returns>
        Task UpdateEmployeeAsync(int employeeId, UpdateEmployee newEmployee);
        /// <summary>
        /// Deletes the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task DeleteEmployeeAsync(int employeeId);
        /// <summary>
        /// Adds the employee to department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<EmployeeDepartment> AddEmployeeToDepartmentAsync(int employeeId, int departmentId);
        /// <summary>
        /// Deletes the employee to department.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task DeleteEmployeeInDepartment(int employeeId, int departmentId);


    }
}
