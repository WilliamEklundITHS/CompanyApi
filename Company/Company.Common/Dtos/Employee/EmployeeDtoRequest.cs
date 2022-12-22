namespace Company.Common.Dtos.Employee
{
    public class EmployeeDtoRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public bool TradeUnion { get; set; }
        public int DepartmentId { get; set; }
        //public ICollection<RoleDtoRequest>? Roles { get; set; }

    }
}
