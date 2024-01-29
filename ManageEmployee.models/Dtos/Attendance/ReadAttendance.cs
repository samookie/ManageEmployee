namespace ManageEmployees.Dtos.Attendance
{
    public class ReadAttendance
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ArrivingDate { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
