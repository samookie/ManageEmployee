using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;
using ManageEmployees.Services.Contracts;
using ManageEmployees.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEmployees.Controllers
{
    /// <summary>
    /// Controller pour la partie Congé
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        /// <summary>
        /// The leave request service
        /// </summary>
        private readonly ILeaveRequestService _leaveRequestService;
        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestController"/> class.
        /// </summary>
        /// <param name="leaveRequestService">The leave request service.</param>
        public LeaveRequestController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        /// <summary>
        /// Adds the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequest">The leave request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateLeaveRequest>> AddLeaveRequestAsync([FromBody] CreateLeaveRequest leaveRequest)
        {
            if (leaveRequest == null || leaveRequest.EmployeeId == 0 || default(DateTime) == leaveRequest.RequestDate || default(DateTime) == leaveRequest.StartDate
                || default(DateTime) == leaveRequest.EndDate)
            {
                return BadRequest("Echec de création d'un congé : les informations sont null ou vides");
            }
            else if (leaveRequest.EmployeeId < 1000)
                return BadRequest("Echec de création d'une congé  : l'id de l'employée doit être supérieur à 1000");

            try
            {
                var leaveRequestCreated = await _leaveRequestService.CreateLeaveRequestAsync(leaveRequest);
                return Ok(leaveRequestCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Updates the leave request asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="leaveRequest">The leave request.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLeaveRequestAsync(int id, [FromBody] UpdateLeaveRequest leaveRequest)
        {
            if (leaveRequest == null)
            {
                return BadRequest("Echec de créaction d'un congé : les informations sont null ou vides");
            }
            else if (id < 1)
                return BadRequest("Echec de création d'une congé  : l'id du congé doit être supérieur à 1");

            try
            {
                await _leaveRequestService.UpdateLeaveRequestAsync(id,leaveRequest);
                return Ok("La mise à jour du status à bien été pris en compte");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets the leave request asynchronous by identifier.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        [HttpGet("{leaveRequestId}")]
        public async Task<ActionResult<ReadLeaveRequest>> GetLeaveRequestAsyncById(int leaveRequestId)
        {
            if (leaveRequestId <1)
            {
                return BadRequest("Echec de la récupération du congé : l'id ne dois pas être inférieur à 1");
            }
            try
            {
                return Ok(await _leaveRequestService.GetLeaveRequestByIdAsync(leaveRequestId));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        /// <summary>
        /// Gets the leave request by employee identifier.
        /// </summary>
        /// <param name="idEmployee">The identifier employee.</param>
        /// <returns></returns>
        [HttpGet("/leaveRequests/{idEmployee}")]
        public async Task<ActionResult> GetLeaveRequestByEmployeeId(int idEmployee)
        {
            if (idEmployee < 1000)
            {
                return BadRequest("Echec de la recherche des congés : l'id doit être supérieur à 1000");
            }

            try
            {
                return Ok(await _leaveRequestService.GetLeaveRequestByEmployeeId(idEmployee));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }





    }
}
