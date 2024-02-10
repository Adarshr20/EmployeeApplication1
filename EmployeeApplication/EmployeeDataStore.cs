using EmployeeApplication.Model;

namespace EmployeeApplication
{
    public class EmployeeDataStore
    {
        ICollection<Employee> _employees;
        ICollection<LeaveRequest> _leaveRequests;
        private int empCount;
        private int leaveCount;
        public EmployeeDataStore()
        {

            _employees = new List<Employee>();
            _leaveRequests  = new List<LeaveRequest>();
            _employees.Add(new Employee { EmployeeId = 1, Name = "John Doe", ManagerId = 101, Password = "123" ,Role=Roles.Admin});
            _employees.Add(new Employee { EmployeeId = 2, Name = "Jane Doe", ManagerId = 2, Password = "123",Role=Roles.Manager });
            _employees.Add(new Employee { EmployeeId = 3, Name = "Bob Smith", ManagerId = 1, Password = "123", Role = Roles.Employee });
            _employees.Add(new Employee { EmployeeId = 4, Name = "Alice Johnson", ManagerId = 2, Password = "123", Role = Roles.Employee });
            _employees.Add(new Employee { EmployeeId = 5, Name = "Eve Davis", ManagerId = 2, Password = "123", Role = Roles.Employee });
            

            empCount = _employees.Count;
            leaveCount = _leaveRequests.Count;
        }
       
         
        public Employee AddEmployee(Employee employee)
        {
            empCount += 1;
            employee.EmployeeId = empCount;
            _employees.Add(employee);
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            Employee obj = _employees.FirstOrDefault(e => e.EmployeeId == id);
            return obj;
        }
        public ICollection<Employee> GetAllEmployee(int id)
        {
            var employeesWithSameManager = _employees.Where(e => e.ManagerId == id).ToList();
            return employeesWithSameManager;
        }

        public Employee Login(string name, string password)
        {
            Employee employee = _employees?.FirstOrDefault(e => e.Name == name&&e.Password==password);
            return employee;
        }

        public ICollection<LeaveRequest> ApplyLeave(int empId,int mgnId)
        { LeaveRequest obj=new LeaveRequest();
            leaveCount += 1;
            obj.LeaveId = leaveCount;
            obj.EmpId=empId;
            obj.MngId = mgnId;
            _leaveRequests.Add(obj);
         var lvreq=_leaveRequests.ToList();
            return lvreq;

        }

        public LeaveRequest ApproveLeave(int leaveId,int mngId)
        {
            LeaveRequest employeeLeave = _leaveRequests.FirstOrDefault(e => e.LeaveId == leaveId && e.MngId == mngId);
            employeeLeave.EmpLeaveStatus=LeaveStatus.Approved;

            return employeeLeave;
        }

    }
}
