using EmployeesCollaboration.Core.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeesCollaboration.Core.Services
{
    public interface IEmployeeService
    {
        (List<PairWorkResult> pairs, PairWorkResult bestPair) ProcessCsv(IFormFile file);
    }
}
