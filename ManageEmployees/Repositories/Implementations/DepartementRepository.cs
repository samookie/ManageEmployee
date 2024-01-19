using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    /// <summary>
    /// La classe du repository de l'employé
    /// </summary>
    /// <seealso cref="ManageEmployees.Repositories.Contracts.IDepartementRepository" />
    public class DepartementRepository : IDepartementRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly ManageEmployeeDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartementRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public DepartementRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets the departments asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }
        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
        }
        /// <summary>
        /// Gets the department by identifier with include asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentByIdWithIncludeAsync(int departmentId)
        {
            return await _dbContext.Departments
                .Include(x => x.EmployeeDepartments)
                .FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
        }
        /// <summary>
        /// Gets the department by name asynchronous.
        /// </summary>
        /// <param name="departmentName">Name of the department.</param>
        /// <returns></returns>
        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(x => x.Name == departmentName);
        }
        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentToUpdate">The department to update.</param>
        public async Task UpdateDepartmentAsync(Department departmentToUpdate)
        {
            _dbContext.Departments.Update(departmentToUpdate);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="departmentToCreate">The department to create.</param>
        /// <returns></returns>
        public async Task<Department> CreateDepartmentAsync(Department departmentToCreate)
        {
            await _dbContext.Departments.AddAsync(departmentToCreate);
            await _dbContext.SaveChangesAsync();

            return departmentToCreate;
        }
        /// <summary>
        /// Deletes the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        public async Task<Department> DeleteDepartmentByIdAsync(int departmentId)
        {
            var departmentToDelete = await _dbContext.Departments.FindAsync(departmentId);
            _dbContext.EmployeeDepartments.RemoveRange(departmentToDelete.EmployeeDepartments);
            _dbContext.Departments.Remove(departmentToDelete);
            await _dbContext.SaveChangesAsync();
            return departmentToDelete;
        }
    }
}
