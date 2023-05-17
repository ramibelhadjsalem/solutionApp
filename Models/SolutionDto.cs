using System.ComponentModel.DataAnnotations;

namespace solutionApp.Models
{
    public class SolutionDto
    {
        [Required]
        [MinLength(5)]
        public string title { get; set; }
        [Required]
        [MinLength(30)]
        public string description { get; set; }
    }
}
