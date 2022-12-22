using Company.Common.Dtos.Role;

namespace Company.Common.Dtos.Employee
{
    public class EmployeeDtoResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public bool TradeUnion { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<RoleDtoResponse> Roles { get; set; }
    }
}
