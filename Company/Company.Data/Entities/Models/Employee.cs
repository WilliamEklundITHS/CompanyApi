using System.Text.Json.Serialization;

namespace Company.Data.Entities.Models
{
    public class Employee
    {
        public Employee()
        {

            Roles = new HashSet<Role>();
        }

        [JsonIgnore]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public bool TradeUnion { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }

        public virtual ICollection<Role> Roles { get; set; }


    }
}
