using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMgtSystemAPI.Models
{
    public class Employee: IdentityUser
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; } = "Employed";
        [ForeignKey(nameof(JobRole))]
        public int? JobRoleId { get; set; }
        
        public JobRole JobRole { get; set; }

    }
}
