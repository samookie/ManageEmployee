using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    /// <summary>
    /// Controller pour la partie Département
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// The departement service
        /// </summary>
        private readonly IDepartementService _departementService;
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentsController"/> class.
        /// </summary>
        /// <param name="departementService">The departement service.</param>
        public DepartmentController(IDepartementService departmentService)
        {
            _departementService = departmentService;
        }

        /// <summary>
        /// Methode POST permettant d'ajouter un Département
        /// </summary>
        /// <param name="department">  Les paramètres sont : le nom, l'addresse et la description du département </param>
        /// <returns></returns>
        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult<ReadDepartment>> Post([FromBody] UpdateDepartment department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de création d'un departement : les informations sont null ou vides");
            }

            try
            {
                var departmentCreated = await _departementService.CreateDepartmentAsync(department);
                return Ok(departmentCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        // PUT api/<DepartmentsController>/id        
        /// <summary>
        /// Puts the department.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id,[FromBody] UpdateDepartment department)
        {
            if (department == null || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address) || string.IsNullOrWhiteSpace(department.Description))
            {
                return BadRequest("Echec de la mise à jour d'un departement : les informations sont null ou vides");
            }

            try
            {
                await _departementService.UpdateDepartmentAsync(id, department);
                return Ok("La mise à jour a un succès !");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            return Ok(await _departementService.GetDepartments());
        }

        /// <summary>
        /// Gets the department by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDepartment>> GetDepartmentByID(int id)
        {
            
            if (id <1) 
            {
                return BadRequest("Echec de récupération d'un departement : l'id ne dois pas être inférieur à 1");
            }
            try
            { 
                return Ok(await _departementService.GetDepartmentByIdAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {

            if (id<1)
            {
                return BadRequest("Echec de récupération d'un departement : l'id ne dois pas être inférieur à 1");
            }
            try
            {
                await _departementService.DeleteDepartmentById(id);

                return Ok( $"La suppression a bien été effectuée sur le département {id} !");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
