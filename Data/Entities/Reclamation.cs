using solution.Data.Entities;

namespace solutionApp.Data.Entities
{
    public class Reclamation:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ReclamationStatus Status { get; set; } = ReclamationStatus.NotProcessed;

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
    public enum ReclamationStatus
    {
        NotProcessed, InProcess, Resolved, NoSolution
    }
}
