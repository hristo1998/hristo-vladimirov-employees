using EmployeesCollaboration.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesCollaboration.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICsvFileValidatorService validator;

        public ApiController(IEmployeeService employeeService, ICsvFileValidatorService validator)
        {
            this.employeeService = employeeService;
            this.validator = validator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var (isValid, errors, records) = await validator.ValidateFileAsync(file);

            if (!isValid)
            {
                return BadRequest(new { Success = false, Errors = errors });
            }

            var result = employeeService.ProcessCsv(file);

            return Ok(new
            {
                pairs = result.pairs,
                bestPair = result.bestPair
            });
        }
    }
}
