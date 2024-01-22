using System.ComponentModel.DataAnnotations;

namespace EmployeeMgtSystemAPI.Models
{
    public class JobRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();   
    }
}
