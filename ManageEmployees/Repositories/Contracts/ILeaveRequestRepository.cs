using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{
    public interface ILeaveRequestRepository
    {
        /// <summary>
        /// Creates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestToCreate">The leave request to create.</param>
        /// <returns></returns>
        Task<LeaveRequest> CreateLeaveRequestAsync(LeaveRequest leaveRequestToCreate);
        /// <summary>
        /// Gets the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        Task<LeaveRequest> GetLeaveRequestAsync(int leaveRequestId);
        /// <summary>
        /// Updates the leave request status asynchronous.
        /// </summary>
        /// <param name="leaveRequest">The leave request.</param>
        /// <returns></returns>
        Task UpdateLeaveRequestStatusAsync(LeaveRequest leaveRequest);
        /// <summary>
        /// Gets the leave request by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<List<LeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId);
    }
}
