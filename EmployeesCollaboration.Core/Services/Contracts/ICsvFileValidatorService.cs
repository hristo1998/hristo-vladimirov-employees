using EmployeesCollaboration.Core.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeesCollaboration.Core.Services
{
    public interface ICsvFileValidatorService
    {
        Task<(bool IsValid, List<string> Errors, List<CsvRecord> Records)> ValidateFileAsync(IFormFile file);
    }
}
