namespace ManageEmployees.Dtos.Attendance
{
    public class CreateAttendance
    {
        public int EmployeeId { get; set; }

        public DateTime ArrivingDate { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
