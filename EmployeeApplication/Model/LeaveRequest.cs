namespace EmployeeApplication.Model
{

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
    public class LeaveRequest
    {
        public int LeaveId {  get; set; }
        public LeaveStatus EmpLeaveStatus { get; set; }=LeaveStatus.Pending;
        public int EmpId { get; set; }
        public int MngId {  get; set; }
    }
}
