using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    /// <summary>
    /// L'interface pour le repository du département
    /// </summary>
    public interface IDepartementRepository
    {
        /// <summary>
        /// Gets the departments asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<Department>> GetDepartmentsAsync();
        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<Department> GetDepartmentByIdAsync(int departmentId);
        /// <summary>
        /// Gets the department by identifier with include asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<Department> GetDepartmentByIdWithIncludeAsync(int departmentId);
        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="departmentName">Name of the department.</param>
        /// <returns></returns>
        Task<Department> GetDepartmentByNameAsync(string departmentName);
        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentToUpdate">The department to update.</param>
        /// <returns></returns>
        Task UpdateDepartmentAsync(Department departmentToUpdate);
        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="departmentToCreate">The department to create.</param>
        /// <returns></returns>
        Task<Department> CreateDepartmentAsync(Department departmentToCreate);
        /// <summary>
        /// Deletes the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        Task<Department> DeleteDepartmentByIdAsync(int departmentId);
    }
}
