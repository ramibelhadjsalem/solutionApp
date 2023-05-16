namespace solutionApp.Data.Entities
{
    public class Solution :BaseEntity
    {
        public int ReclamationId { get; set; }
        public Reclamation Reclamation { get; set; }
        public SolutionStatus status { get; set; } = SolutionStatus.NotSeen;

        public string Title { get; set; }
        public string Description { get; set; }
    }
    public enum SolutionStatus
    {
        NotSeen,Working,NotWorking
    }
}
