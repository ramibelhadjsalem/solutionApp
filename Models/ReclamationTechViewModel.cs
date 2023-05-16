using solution.Data.Entities;
using solutionApp.Data.Entities;

namespace solutionApp.Models
{
    public class ReclamationTechViewModel
    {
        public Filter filter { get; set; }
        public IList<Reclamation> reclamations { get; set; } =new List<Reclamation>();
        public IList<AppUser> users { get; set; }

        public List<string> reclamationStatus = new List<string>() { "NotProcessed", "InProcess", "Resolved", "NoSolution" };
    }
    public class Filter
    {
        public int? UserId { get; set; } = null;
        public string status { get; set; } 
    }
}
