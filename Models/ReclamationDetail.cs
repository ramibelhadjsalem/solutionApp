using solutionApp.Data.Entities;

namespace solutionApp.Models
{
    public class ReclamationDetail
    {
        public Reclamation  reclamation { get; set; }
        public bool isOwner { get; set; } = false;
        public bool canEdit { get; set; } = false;
        public bool canTake { get; set; } = false;
        public bool canAddSolution { get; set; } = false;
    }
}
