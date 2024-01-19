using ManageEmployees.Dtos.Employee;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    /// <summary>
    /// Controller de la partir Employée
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// The employee service
        /// </summary>
        private readonly IEmployeeService _employeeService;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeService">The employee service.</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReadEmployee>> AddEmployee([FromBody] CreateEmployee employee)

        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.LastName)
                || default(DateTime) == employee.Birthday|| string.IsNullOrWhiteSpace(employee.Email) || string.IsNullOrWhiteSpace(employee.PhoneNumber)
                || string.IsNullOrWhiteSpace(employee.Position))
            {
                return BadRequest("Echec de créaction d'un employée : les informations sont null ou vides");
            }
            if (!employee.Email.Contains("@") && !employee.Email.Contains(".fr") || !employee.Email.Contains(".com"))
                return BadRequest("Echec de créaction d'un employée : vous devez entrer un email valide ('@' ou '.fr' / '.com').");
            if (employee.PhoneNumber.Length < 10)
                return BadRequest("Echec de la création d'un employée : vous devez entrer un vrai numéro (au moins 10 chiffres)");
            
            
            if (employee.Birthday.Year > 2008)
                return BadRequest("Echec de la création d'un employée : vous devez avoir minimum 16 ans");
            try
            {
                var employeeCreated = await _employeeService.CreateEmployeeAsync(employee);
                return Ok(employeeCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets the employee by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEmployee>> GetEmployeeByID(int id)
        {
            if (id < 1000)
            {
                return BadRequest("Echec de la récupération de l'employée : l'id ne dois pas être inférieur à 1000");
            }
            try
            {
                return Ok(await _employeeService.GetEmployeeByIdAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployee employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.FirstName) || string.IsNullOrWhiteSpace(employee.LastName)
               || employee.Birthday == null || string.IsNullOrWhiteSpace(employee.Email) || string.IsNullOrWhiteSpace(employee.PhoneNumber)
               || string.IsNullOrWhiteSpace(employee.Position))
            {
                return BadRequest("Echec de créaction d'un employée : les informations sont null ou vides");
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employee);
                return Ok("La mise à jour à été un succès");
            }
            catch (Exception ex) 
            { 
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        public async Task<ActionResult> DeleteEmployee(int employeeId)
        {
            if (employeeId < 1000)
            {
                return BadRequest("Echec de la récupération de l'employée : l'id ne dois pas être inférieur à 1000");

            }
            try
            {
                await _employeeService.DeleteEmployeeAsync(employeeId);

                return Ok($"La suppression a bien été effectuée sur l'employée {employeeId} !");
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Adds the employee to department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpPost("{employeeId}/departments/{departmentId}")]
        public async Task<IActionResult> AddEmployeeToDepartmentAsync(int employeeId, int departmentId)
        {
            if (employeeId < 1000)
            {
                return BadRequest("Echec de la récupération de l'employée : l'id ne dois pas être inférieur à 1000");

            }
            else if (departmentId < 0)
            {
                return BadRequest("Echec de la récupération du département : l'id ne dois pas être inférieur à 1");
            }

            try
            {
                var employeeDepartment = await _employeeService.AddEmployeeToDepartmentAsync(employeeId, departmentId);
                return Ok($"Vous avez bien ajouté l'employée {employeeId} au département {departmentId} !");

            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the employee department asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}/departments/{departmentId}")]
        public async Task<ActionResult> DeleteEmployeeDepartmentAsync(int employeeId, int departmentId)
        {
            if (employeeId < 1000)
            {
                return BadRequest("Echec de la récupération de l'employée : l'id ne dois pas être inférieur à 1000");

            }
            else if (departmentId < 0)
            {
                return BadRequest("Echec de la récupération du département : l'id ne dois pas être inférieur à 1");
            }

            try
            {
                await _employeeService.DeleteEmployeeInDepartment(employeeId, departmentId);
                return Ok($"Vous avez bien supprimé l'employée {employeeId} au département {departmentId} !");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            return Ok(await _employeeService.GetEmployees());
        }





    }
}
