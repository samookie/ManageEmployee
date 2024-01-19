using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    /// <summary>
    /// Controller pour la partie Attendance
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        /// <summary>
        /// The attendance Service
        /// </summary>
        private readonly IAttendanceService _attendanceService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceController"/> class.
        /// </summary>
        /// <param name="attendanceService">The attendance service.</param>
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        /// <summary>
        /// Adds the attendance.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateAttendance>> AddAttendance([FromBody] CreateAttendance attendance)
        {


            if (attendance == null || attendance.EmployeeId == 0 || default(DateTime) == attendance.ArrivingDate || default(DateTime) == attendance.DepartureDate)
            {
                return BadRequest("Echec de création d'une présence : les informations sont null ou vides");
            }
            else if (attendance.EmployeeId < 1000)
                return BadRequest("Echec de création d'une présence : l'id doit être supérieur à 1000");

            try
            {
                var attendanceCreated = await _attendanceService.CreateAttendanceAsync(attendance);
                return Ok(attendanceCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        [HttpGet("{attendanceId}")]
        public async Task<IActionResult> GetAttendanceByIdAsync(int attendanceId)
        {
            if (attendanceId < 1)
            {
                return BadRequest("Echec de la récupération de la présence : l'id ne dois pas être inférieur à 1");
            }
            try
            {
                return Ok(await _attendanceService.GetAttendanceByIdAsync(attendanceId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Gets the attendance by employee identifier.
        /// </summary>
        /// <param name="idEmployee">The identifier employee.</param>
        /// <returns></returns>
        [HttpGet("/attendances/{idEmployee}")]
        public async Task<ActionResult> GetAttendanceByEmployeeId(int idEmployee)
        {
            if ( idEmployee < 1000)
            {
               return BadRequest("Echec de la recherche d'une présence : l'id doit être supérieur à 1000");
            }

            try
            {
                return Ok(await _attendanceService.GetAttendancesByEmployeeId(idEmployee));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }


    }
}
