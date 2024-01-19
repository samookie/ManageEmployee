using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Entities;
using ManageEmployees.Infrastructures.Database;
using ManageEmployees.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployees.Repositories.Implementations
{
    /// <summary>
    /// La classe du repository des présences de l'employée
    /// </summary>
    /// <seealso cref="ManageEmployees.Repositories.Contracts.IAttendanceRepository" />
    public class AttendanceRepository : IAttendanceRepository
    {
        /// <summary>
        /// The database context
        /// </summary>
        private readonly ManageEmployeeDbContext _dbContext;
        /// <summary>
        /// Initializes a new instance of the <see cref="AttendanceRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public AttendanceRepository(ManageEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Creates the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceToCreate">The attendance to create.</param>
        /// <returns></returns>
        public async Task<Attendance> CreateAttendanceAsync(Attendance attendanceToCreate)
        {
            await _dbContext.Attendances.AddAsync(attendanceToCreate);
            await _dbContext.SaveChangesAsync();
            return attendanceToCreate;
        }

        /// <summary>
        /// Gets the attendance asynchronous.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        public async Task<Attendance> GetAttendanceAsync(int attendanceId)
        {
            return await _dbContext.Attendances.FirstOrDefaultAsync(a => a.AttendanceId == attendanceId);
            
        }

        /// <summary>
        /// Gets the attendance by employee and date asynchronous.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public async Task<int> GetAttendanceByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            return await _dbContext.Attendances
                .Where(a => a.EmployeeId == employeeId && a.ArrivingDate.Date == date)
                .CountAsync();
        }

        /// <summary>
        /// Gets the attendances by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        public async Task<List<Attendance>> GetAttendancesByEmployeeId(int employeeId)
        {
            return await _dbContext.Attendances
            .Where(a => a.EmployeeId == employeeId)
            .ToListAsync();
        }




    }
}
