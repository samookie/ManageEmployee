using ManageEmployees.Dtos.Department;

namespace ManageEmployees.Services.Contracts
{
    public interface IDepartementService
    {
        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department);
        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department);
        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        Task<List<ReadDepartment>> GetDepartments();
        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<ReadDepartment> GetDepartmentByIdAsync(int departmentId);
        /// <summary>
        /// Deletes the department by identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task DeleteDepartmentById(int departmentId);
    }
}
