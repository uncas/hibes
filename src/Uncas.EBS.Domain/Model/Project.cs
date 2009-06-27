using System.ComponentModel.DataAnnotations;
namespace Uncas.EBS.Domain.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; }
    }
}