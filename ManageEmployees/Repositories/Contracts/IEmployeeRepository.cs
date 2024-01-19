using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    /// <summary>
    /// L'interface pour le repository du salarié
    /// </summary>
    public interface IEmployeeRepository
    {

        // GET

        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<Employee>> GetEmployeesAsync();
        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="EmployeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByIdAsync(int EmployeeId);
        /// <summary>
        /// Gets the employee byemail asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByemailAsync(string email);
        /// <summary>
        /// Gets the employee department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<EmployeeDepartment> GetEmployeeDepartmentAsync(int employeeId, int departmentId);

        // POST

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employeeToCreate">The employee to create.</param>
        /// <returns></returns>
        Task<Employee> CreateEmployeeAsync(Employee employeeToCreate);
        /// <summary>
        /// Adds the employee to department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<EmployeeDepartment> AddEmployeeToDepartmentAsync(int employeeId, int departmentId);

        // PUT

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeToUpdate">The employee to update.</param>
        /// <returns></returns>
        Task UpdateEmployeeAsync(Employee employeeToUpdate);

        // DELETE 
           
        /// <summary>
        /// Deletes the employee from department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task DeleteEmployeeFromDepartmentAsync(int employeeId, int departmentId);
        /// <summary>
        /// Deletes the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task DeleteEmployeeByIdAsync(int employeeId);

    }


}
