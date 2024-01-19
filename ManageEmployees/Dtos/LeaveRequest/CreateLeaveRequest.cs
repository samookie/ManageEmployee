namespace ManageEmployees.Dtos.LeaveRequest
{
    public class CreateLeaveRequest
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        /// <value>
        /// The request date.
        /// </value>
        public DateTime RequestDate { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Gets or sets the leave request status identifier.
        /// </summary>
        /// <value>
        /// The leave request status identifier.
        /// </value>
        public int LeaveRequestStatusId { get; set; } = 1;

    }
}
