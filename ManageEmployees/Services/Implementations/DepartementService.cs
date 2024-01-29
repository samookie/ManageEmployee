using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Services.Contracts;

namespace ManageEmployees.Services.Implementations
{
    /// <summary>
    /// La classe service du département
    /// </summary>
    /// <seealso cref="ManageEmployees.Services.Contracts.IDepartementService" />
    public class DepartementService : IDepartementService
    {
        /// <summary>
        /// The departement repository
        /// </summary>
        private readonly IDepartementRepository _departementRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartementService"/> class.
        /// </summary>
        /// <param name="departementRepository">The departement repository.</param>
        public DepartementService(IDepartementRepository departementRepository)
        {
            _departementRepository = departementRepository;
        }
        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadDepartment>> GetDepartments()
        {
            var departments = await _departementRepository.GetDepartmentsAsync();

            List<ReadDepartment> readDepartments = new List<ReadDepartment>();

            foreach (var department in departments)
            {
                readDepartments.Add(new ReadDepartment()
                {
                    Id = department.DepartmentId,
                    Name = department.Name,
                    Description = department.Description,
                    Address = department.Address,
                });
            }

            return readDepartments;
        }

        /// <summary>
        /// Gets the department by identifier asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un département car il n'existe pas : {departmentId}</exception>
        public async Task<ReadDepartment> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _departementRepository.GetDepartmentByIdAsync(departmentId);

            if (department is null)
                throw new Exception($"Echec de recupération des informations d'un département car il n'existe pas : {departmentId}");

            return new ReadDepartment()
            {
                Id = department.DepartmentId,
                Name = department.Name,
                Description = department.Description,
                Address = department.Address,
            };

        }
        /// <summary>
        /// Updates the department asynchronous.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="department">The department.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}
        /// </exception>
        public async Task UpdateDepartmentAsync(int departmentId, UpdateDepartment department)
        {
            var departmentGet = await _departementRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception($"Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}");

            var departmentGetName = await _departementRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGetName is not null && departmentId != departmentGetName.DepartmentId)
            {
                throw new Exception($"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            departmentGet.Name = department.Name;
            departmentGet.Description = department.Description;
            departmentGet.Address = department.Address;

            await _departementRepository.UpdateDepartmentAsync(departmentGet);

        }
        /// <summary>
        /// Deletes the department by identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}
        /// or
        /// Echec de suppression car ce departement est lié à des employés
        /// </exception>
        public async Task DeleteDepartmentById(int departmentId)
        {
            var departmentGet = await _departementRepository.GetDepartmentByIdWithIncludeAsync(departmentId)
              ?? throw new Exception($"Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}");

            await _departementRepository.DeleteDepartmentByIdAsync(departmentId);
        }
        /// <summary>
        /// Creates the department asynchronous.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}</exception>
        public async Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department)
        {
            var departmentGet = await _departementRepository.GetDepartmentByNameAsync(department.Name);
            if (departmentGet is not null)
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}");
            }

            var departementTocreate = new Department()
            {
                Name = department.Name,
                Description = department.Description,
                Address = department.Address,
            };

            var departmentCreated = await _departementRepository.CreateDepartmentAsync(departementTocreate);

            return new ReadDepartment()
            {
                Id = departmentCreated.DepartmentId,
                Name = departmentCreated.Name,
                Description = departmentCreated.Description,
                Address = departmentCreated.Address,
            };
        }
    }
}
