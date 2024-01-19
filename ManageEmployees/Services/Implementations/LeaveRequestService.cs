using AutoMapper;
using ManageEmployees.Dtos.LeaveRequest;
using ManageEmployees.Entities;
using ManageEmployees.Repositories.Contracts;
using ManageEmployees.Repositories.Implementations;
using ManageEmployees.Services.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;


namespace ManageEmployees.Services.Implementations
{

    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeaveRequestService"/> class.
        /// </summary>
        /// <param name="leaveRequestRepository">The leave request repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequest">The leave request.</param>
        /// <returns></returns>
        public async Task<ReadLeaveRequest> CreateLeaveRequestAsync(CreateLeaveRequest leaveRequest)
        {
            var lrCreate = new LeaveRequest()
            {
                EmployeeId = leaveRequest.EmployeeId,
                RequestDate = leaveRequest.RequestDate,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId,
            };

            var lrCreated = await _leaveRequestRepository.CreateLeaveRequestAsync(lrCreate);

            return new ReadLeaveRequest()
            {
                Id = lrCreated.LeaveRequestId,
                EmployeeId = lrCreated.EmployeeId,
                RequestDate = lrCreated.RequestDate,
                StartDate = lrCreated.StartDate,
                EndDate = lrCreated.EndDate,
                LeaveRequestStatusId = lrCreated.LeaveRequestStatusId,

            };
        }

        /// <summary>
        /// Gets the leave request by identifier.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération de la demande de congé elle n'existe pas : {leaveRequestId}</exception>
        public async Task<ReadLeaveRequest> GetLeaveRequestByIdAsync(int leaveRequestId)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestAsync(leaveRequestId);

            if (leaveRequest == null)
                throw new Exception($"Echec de recupération de la demande de congé elle n'existe pas : {leaveRequestId}");

            return new ReadLeaveRequest()
            {
                Id = leaveRequest.LeaveRequestId,
                EmployeeId = leaveRequest.EmployeeId,
                RequestDate = leaveRequest.RequestDate,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId,
            };
        }

        /// <summary>
        /// Updates the leave request asynchronous.
        /// </summary>
        /// <param name="leaveRequestId">The leave request identifier.</param>
        /// <param name="newLeaveRequest">The new leave request.</param>
        /// <exception cref="System.Exception">
        /// Echec de mise à jour du congé : Il n'existe aucun congé avec cet identifiant : {leaveRequestId}
        /// or
        /// Echec de mise à jour du congé : Vous devez ou accepter (2) ou refuser (3) le congé !
        /// </exception>
        public async Task UpdateLeaveRequestAsync(int leaveRequestId, UpdateLeaveRequest newLeaveRequest)
        {
            var leaveRequest = await _leaveRequestRepository.GetLeaveRequestAsync(leaveRequestId);

            if (leaveRequest == null)
            {
                throw new Exception($"Echec de mise à jour du congé : Il n'existe aucun congé avec cet identifiant : {leaveRequestId}");

            }

            if (newLeaveRequest.LeaveRequestStatusId == 1)
            {
                throw new Exception($"Echec de mise à jour du congé : Vous devez ou accepter (2) ou refuser (3) le congé !");
            }
            else if (newLeaveRequest.LeaveRequestStatusId > 3)
            {
                throw new Exception($"Echec de mise à jour du congé : le status ne dois pas être supérieur à 3 !");
            }

            leaveRequest.LeaveRequestStatusId = newLeaveRequest.LeaveRequestStatusId;

            await _leaveRequestRepository.UpdateLeaveRequestStatusAsync(leaveRequest);

        }


        /// <summary>
        /// Gets the attendances by employee identifier.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Echec de recupération des informations d'une présence car il n'existe pas de présence avec l'id de cet employée: {employeeId}</exception>
        public async Task<List<ReadLeaveRequest>> GetLeaveRequestByEmployeeId(int employeeId)
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestByEmployeeId(employeeId);

            if (leaveRequests == null)
                throw new Exception($"Echec de recupération des informations des congés car il n'existe pas de congés avec l'id de cet employée: {employeeId}");

            List<ReadLeaveRequest> readLeaveRequest = new List<ReadLeaveRequest>();

            foreach (var leaveRequest in leaveRequests)
                readLeaveRequest.Add(new ReadLeaveRequest()
                {
                    Id = leaveRequest.LeaveRequestId,
                    EmployeeId = leaveRequest.EmployeeId,
                    RequestDate = leaveRequest.RequestDate,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    LeaveRequestStatusId = leaveRequest.LeaveRequestStatusId,
                });
            return readLeaveRequest;
        }

    }

}
