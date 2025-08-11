using EmployeesCollaboration.Core.Services;
using EmployeesCollaboration.Web.Extensions;
using EmployeesCollaboration.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesCollaboration.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly ICsvFileValidatorService validator;

        public HomeController(IEmployeeService employeeService, ICsvFileValidatorService validator)
        {
            this.employeeService = employeeService;
            this.validator = validator;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var (isValid, errors, records) = await validator.ValidateFileAsync(file);

            if (!isValid)
            {
                TempData.Put("Errors", errors);
                return RedirectToAction("Index");
            }

            try
            {
                var result = employeeService.ProcessCsv(file);
                var resultModel = new CollaborationResultViewModel
                {
                    Pairs = result.pairs,
                    BestPair = result.bestPair,
                };

                return View("Results", resultModel);
            }
            catch (Exception ex)
            {
                TempData.Put("Errors", errors);
                return RedirectToAction("Index");
            }
        }
    }
}
