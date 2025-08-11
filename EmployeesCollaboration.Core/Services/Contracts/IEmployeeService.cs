using EmployeesCollaboration.Core.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeesCollaboration.Core.Services
{
    public interface IEmployeeService
    {
        (List<PairWorkResult> perProject, List<PairTotalResult> totals, PairTotalResult bestPair)
        ProcessCsv(IFormFile file);
    }
}
