using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Dtos.LeaveRequest;
using Microsoft.AspNetCore.Mvc;

namespace ManageEmployees.Services.Contracts
{
    /// <summary>
    /// Classe de l'interface du service des congés
    /// </summary>
    public interface ILeaveRequestService
    {
        /// <summary>
        /// Adds the attendance.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaveRequest);
        /// <summary>
        /// Gets the leave request by identifier asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId);
        /// <summary>
        /// Updates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <param name="newLeaveRequest">The new leave request.</param>
        /// <returns></returns>
        Task UpdateLeaveRequestAsync(int leaveRequestId, UpdateLeaveRequest newLeaveRequest);
        /// <summary>
        /// Gets the leave request by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<List<ReadLeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId);
    }
}
