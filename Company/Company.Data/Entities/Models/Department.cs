using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Company.Data.Entities.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}

