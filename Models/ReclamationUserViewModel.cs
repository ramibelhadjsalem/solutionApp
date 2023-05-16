using solutionApp.Data.Entities;

namespace solutionApp.Models
{
    public class ReclamationUserViewModel
    {
        public Filter filter { get; set; }
        public IList<Reclamation> reclamations { get; set; } = new List<Reclamation>();
    }
}
