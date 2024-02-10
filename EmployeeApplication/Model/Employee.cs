namespace EmployeeApplication.Model
{
    public enum Roles
    {
        Employee,
        Admin,
        Manager
      
    }
    public class Employee
    {
       
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }
        public int ManagerId {  get; set; }

        public Roles Role { get; set; }=Roles.Employee;
        public bool IsManager { get; set; } = false;



    }
}
