using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Company.Data.Entities.Models
{
    public class Role
    {
        public Role()
        {
            Employee = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public ICollection<Employee> Employee { get; set; }
    }
}
