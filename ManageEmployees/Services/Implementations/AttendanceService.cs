using AutoMapper;
using ManageEmployees.Dtos.Attendance;
using ManageEmployees.Dtos.Department;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;

//
namespace ManageEmployees.Services.Implementations
{

    /// <summary>
    /// La classe service des présences des employées
    /// </summary>
    /// <seealso cref="ManageEmployees.Services.Contracts.IAttendanceService" />
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendaceService"/> class.
        /// </summary>
        /// <param name="attendanceRepository">The attendance repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        public AttendanceService(IAttendanceRepository attendanceRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Creates the attendance.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        public async Task<ReadAttendance> CreateAttendanceAsync(CreateAttendance attendance)
        {
            var attendanceCreate = new Attendance()
            {
                EmployeeId = attendance.EmployeeId,
                ArrivingDate = attendance.ArrivingDate,
                DepartureDate = attendance.DepartureDate,
            };

            var attendanceCreated = await _attendanceRepository.CreateAttendanceAsync(attendanceCreate);

            if (await _attendanceRepository.GetAttendanceByEmployeeAndDateAsync(attendanceCreated.EmployeeId, DateTime.Today ) >  4)
            {
                throw new Exception($"Echec de Création d'une présence  : Vous n'avez pas le droit de faire plus de 4 présences par jours !");
            }

            return new ReadAttendance()
            {
                Id = attendanceCreated.AttendanceId,
                EmployeeId = attendanceCreated.EmployeeId,
                ArrivingDate = attendanceCreated.ArrivingDate,
                DepartureDate = (DateTime)attendanceCreated.DepartureDate,
            };

        }

        /// <summary>
        /// Gets the attendance by identifier.
        /// </summary>
        /// <param name="attendanceId">The attendance identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'une présence car il n'existe pas : {attendanceId}</exception>
        public async Task<ReadAttendance> GetAttendanceByIdAsync(int attendanceId)
        {
            var attendance = await _attendanceRepository.GetAttendanceAsync(attendanceId);

            if (attendance == null)
                throw new Exception($"Echec de recupération des informations d'une présence car il n'existe pas : {attendanceId}");

            return new ReadAttendance()
            {
                Id = attendance.AttendanceId,
                EmployeeId = attendance.EmployeeId,
                ArrivingDate = attendance.ArrivingDate,
                DepartureDate = (DateTime)attendance.DepartureDate,
            };
        }

        /// <summary>
        /// Gets the attendances by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'une présence car il n'existe pas de présence avec l'id de cet employée: {employeeId}</exception>
        public async Task<List<ReadAttendance>> GetAttendancesByEmployeeId(int employeeId)
        {
            var attendances = await _attendanceRepository.GetAttendancesByEmployeeId(employeeId);

            if (attendances == null)
                throw new Exception($"Echec de recupération des informations d'une présence car il n'existe pas de présence avec l'id de cet employée: {employeeId}");

            List<ReadAttendance> readAttendance = new List<ReadAttendance>();

            foreach (var attendance in attendances)
            {
                readAttendance.Add(new ReadAttendance()
                {
                    Id = attendance.AttendanceId,
                    EmployeeId = attendance.EmployeeId,
                    ArrivingDate = attendance.ArrivingDate,
                    DepartureDate = (DateTime)attendance.DepartureDate,
                });
            }

            return readAttendance;

        }

    }
}
