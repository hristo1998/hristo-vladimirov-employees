using EmployeesCollaboration.Core.Models;

namespace EmployeesCollaboration.Web.ViewModels
{
    public class CollaborationResultViewModel
    {
        public List<PairWorkResult> Pairs { get; set; }
        public PairWorkResult BestPair { get; set; }
    }
}
