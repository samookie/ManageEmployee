using AutoMapper;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

//
namespace ManageEmployees.Services.Implementations
{

    /// <summary>
    /// La classe service du salarié
    /// </summary>
    /// <seealso cref="ManageEmployees.Services.Contracts.IEmployeeService" />
    public class EmployeeService : IEmployeeService
    {
       
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IDepartementRepository _departementRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="mapper">The mapper.</param>
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IDepartementRepository departementRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _departementRepository = departementRepository;
        }

        /// <summary>
        /// Creates the employee asynchronous.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de création d'un département : Il existe déjà un département avec ce nom {employee.Email}</exception>
        public async Task<CreateEmployee> CreateEmployeeAsync(CreateEmployee employee)
        {
            var verifNameEmployee = await _employeeRepository.GetEmployeeByemailAsync(employee.Email);
            if (verifNameEmployee != null) 
            {
                throw new Exception($"Echec de création d'un département : Il existe déjà un département avec ce nom {employee.Email}");
            }
            var employeeToCreate = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.Birthday,
                Email = employee.Email,
                PhoneNumber= employee.PhoneNumber,
                Position = employee.Position
            };

            var employeeCreated = await _employeeRepository.CreateEmployeeAsync(employeeToCreate);

            return new CreateEmployee()
            {

                FirstName = employeeCreated.FirstName,
                LastName = employeeCreated.LastName,
                Birthday = employeeCreated.BirthDate,
                Email = employeeCreated.Email,
                PhoneNumber = employeeCreated.PhoneNumber,
                Position = employeeCreated.Position

            };


        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReadEmployee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            List<ReadEmployee> resultEmployees = new List<ReadEmployee>();

            return employees.Select(employee => _mapper.Map<ReadEmployee>(employee)).ToList(); 
        }
        /// <summary>
        /// Gets the employee by identifier asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'un employée car il n'existe pas : {employeeId}</exception>
        public async Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId)
        {

            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
                throw new Exception($"Echec de recupération des informations d'un employée car il n'existe pas : {employeeId}");

            return _mapper.Map<ReadEmployee>(employee);
        }

        /// <summary>
        /// Updates the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="newEmployee">The new employee.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour d'un employée : Il n'existe aucun employée avec cet identifiant : {employeeId}
        /// or
        /// Echec de mise à jour d'un employee : Il existe déjà un employee avec ce nom {employee.Email}
        /// </exception>
        public async Task UpdateEmployeeAsync(int employeeId, UpdateEmployee newEmployee)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de mise à jour d'un employée : Il n'existe aucun employée avec cet identifiant : {employeeId}");

            var verifNameEmployee = await _employeeRepository.GetEmployeeByemailAsync(newEmployee.Email);
            if (verifNameEmployee is not null && employeeId != verifNameEmployee.EmployeeId)
            {
                throw new Exception($"Echec de mise à jour d'un employee : Il existe déjà un employee avec ce nom {employee.Email}");
            }

            employee.FirstName = newEmployee.FirstName;
            employee.LastName = newEmployee.LastName;
            employee.BirthDate = newEmployee.Birthday;
            employee.Email = newEmployee.Email;
            employee.PhoneNumber = newEmployee.PhoneNumber;
            employee.Position = newEmployee.Position;

            await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        /// <summary>
        /// Deletes the employee asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de suppression d'un employée : Il n'existe aucun employée avec cet identifiant : {employeeId}
        /// or
        /// Echec de suppression car ce departement est lié à des employés
        /// </exception>
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de suppression d'un employée : Il n'existe aucun employée avec cet identifiant : {employeeId}");

            
            await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }

        /// <summary>
        /// Adds the employee to department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Echec de recherche de cet employée : Il n'existe aucun employée avec cet identifiant : {employeeId}
        /// or
        /// Echec de recherche de ce département : Il n'existe aucun département avec cet identifiant : {departmentId}
        /// or
        /// Echec de l'ajout de l'employée {employeeId} dans le département {departmentId}
        /// or
        /// Echec d'ajout du salarié dans le département : Ce salarié {employeeId} est déjà dans le département : {departmentId}
        /// </exception>
        public async Task<EmployeeDepartment> AddEmployeeToDepartmentAsync(int employeeId,int departmentId)
        {
            var verifEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de recherche de cet employée : Il n'existe aucun employée avec cet identifiant : {employeeId}");
            var verifDepartment = await _departementRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception($"Echec de recherche de ce département : Il n'existe aucun département avec cet identifiant : {departmentId}");

            if (verifEmployee == null && verifDepartment ==  null) 
            {
                throw new Exception($"Echec de l'ajout de l'employée {employeeId} dans le département {departmentId}");
            }

            var verifEmployeeInDepartment = await _employeeRepository.GetEmployeeDepartmentAsync(verifEmployee.EmployeeId, verifDepartment.DepartmentId);

            if (verifEmployeeInDepartment != null)
            {
                throw new Exception($"Echec d'ajout du salarié dans le département : Ce salarié {employeeId} est déjà dans le département : {departmentId}");
            }

            var addEmployee = await _employeeRepository.AddEmployeeToDepartmentAsync(verifEmployee.EmployeeId, verifDepartment.DepartmentId);
            return addEmployee;
        }

        /// <summary>
        /// Deletes the employee to department.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <exception cref="System.Exception">
        /// Echec de recherche de cet employée : Il n'existe aucun employée avec cet identifiant : {employeeId}
        /// or
        /// Echec de recherche de ce département : Il n'existe aucun département avec cet identifiant : {departmentId}
        /// or
        /// Echec de suppression de l'employée {employeeId} dans le département {departmentId}
        /// or
        /// Echec de la suppression du salarié dans le département : Ce salarié {employeeId} n'est pas dans le département : {departmentId}
        /// </exception>
        public async Task DeleteEmployeeInDepartment(int employeeId, int departmentId)
        {
            var verifEmployee = await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception($"Echec de recherche de cet employée : Il n'existe aucun employée avec cet identifiant : {employeeId}");
            var verifDepartment = await _departementRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception($"Echec de recherche de ce département : Il n'existe aucun département avec cet identifiant : {departmentId}");

            if (verifEmployee == null && verifDepartment == null)
            {
                throw new Exception($"Echec de suppression de l'employée {employeeId} dans le département {departmentId}");
            }

            var supprEmployee = await _employeeRepository.GetEmployeeDepartmentAsync(verifEmployee.EmployeeId, verifDepartment.DepartmentId);

            if (supprEmployee == null)
            {
                throw new Exception($"Echec de la suppression du salarié dans le département : Ce salarié {employeeId} n'est pas dans le département : {departmentId}");

            }

            await _employeeRepository.DeleteEmployeeFromDepartmentAsync(employeeId, departmentId);

        }

    }
}
