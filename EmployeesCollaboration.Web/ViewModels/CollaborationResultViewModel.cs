using EmployeesCollaboration.Core.Models;

namespace EmployeesCollaboration.Web.ViewModels
{
    public class CollaborationResultViewModel
    {
        public List<PairWorkResult> PerProject { get; set; }
        public List<PairTotalResult> Totals { get; set; }
        public PairTotalResult BestPair { get; set; }
    }
}
