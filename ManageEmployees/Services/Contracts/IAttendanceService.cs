using ManageEmployees.Dtos.Employee;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;

namespace ManageEmployees.Services.Contracts
{
    public interface IAttendanceService
    {
        /// <summary>
        /// Creates the attendance.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance);
        /// <summary>
        /// Gets the attendance by identifier asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId);

        /// <summary>
        /// Gets the attendances by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<List<ReadAttendance>> GetAttendancesByEmployeeId(int employeeId);

    }
}
