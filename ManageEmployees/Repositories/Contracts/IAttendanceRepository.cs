using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;

namespace ManageEmployees.Repositories.Contracts
{

    public interface IAttendanceRepository 
    {
        /// <summary>
        /// Creates the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceToCreate">The attendance to create.</param>
        /// <returns></returns>
        Task<Attendance> CreateAttendanceAsync(Attendance attendanceToCreate);
        /// <summary>
        /// Gets the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        Task<Attendance> GetAttendanceAsync(int attendanceId);
        /// <summary>
        /// Gets the attendance by employee and date asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        Task<int> GetAttendanceByEmployeeAndDateAsync(int employeeId, DateTime date);
        /// <summary>
        /// Gets the attendances by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        Task<List<Attendance>> GetAttendancesByEmployeeId(int employeeId);
    }
}
