using EmployeesCollaboration.Core.Consts;
using EmployeesCollaboration.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;

namespace EmployeesCollaboration.Core.Services
{
    public class CsvFileValidatorService : ICsvFileValidatorService
    {
        private readonly long _maxFileSizeBytes;
        private readonly string[] _allowedExtensions;
        private readonly string[] _supportedDateFormats;

        public CsvFileValidatorService(long maxFileSizeBytes = 5 * 1024 * 1024) // 5MB default
        {
            _maxFileSizeBytes = maxFileSizeBytes;
            _allowedExtensions = new[] { Constants.CsvFileFormat };
            _supportedDateFormats = Constants.DateFormats;
        }

        public async Task<(bool IsValid, List<string> Errors, List<CsvRecord> Records)> ValidateFileAsync(IFormFile file)
        {
            var errors = new List<string>();
            var records = new List<CsvRecord>();

            if (file == null)
            {
                errors.Add(ErrorMessageConstants.NoFileUploaded);
                return (false, errors, records);
            }

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(ext))
                errors.Add(ErrorMessageConstants.InvalidFileType);

            if (file.Length == 0)
                errors.Add(ErrorMessageConstants.EmptyFile);

            if (file.Length > _maxFileSizeBytes)
                errors.Add(string.Format(ErrorMessageConstants.FileTooLarge, _maxFileSizeBytes / (1024 * 1024)));

            if (errors.Count > 0)
                return (false, errors, records);

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            int lineNumber = 0;

            while (!reader.EndOfStream)
            {
                lineNumber++;
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = line.Split(',');

                if (columns.Length != 4)
                {
                    errors.Add(string.Format(ErrorMessageConstants.WrongColumnCount, lineNumber, columns.Length));
                    continue;
                }

                for (int i = 0; i < columns.Length; i++)
                    columns[i] = columns[i].Trim();

                if (!int.TryParse(columns[0], out int empId) || empId <= 0)
                {
                    errors.Add(string.Format(ErrorMessageConstants.InvalidEmpId, lineNumber, columns[0]));
                    continue;
                }

                if (!int.TryParse(columns[1], out int projectId) || projectId <= 0)
                {
                    errors.Add(string.Format(ErrorMessageConstants.InvalidProjectId, lineNumber, columns[1]));
                    continue;
                }

                if (!TryParseDate(columns[2], out DateTime dateFrom))
                {
                    errors.Add(string.Format(ErrorMessageConstants.InvalidDateFrom, lineNumber, columns[2]));
                    continue;
                }

                DateTime dateTo;
                if (string.Equals(columns[3], "NULL", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(columns[3]))
                {
                    dateTo = DateTime.Today;
                }
                else if (!TryParseDate(columns[3], out dateTo))
                {
                    errors.Add(string.Format(ErrorMessageConstants.InvalidDateTo, lineNumber, columns[3]));
                    continue;
                }

                if (dateFrom > DateTime.Today)
                {
                    errors.Add(string.Format(ErrorMessageConstants.DateFromInFuture, lineNumber, dateFrom.ToString(Constants.DefaultDateFormat)));
                    continue;
                }

                if (dateFrom > dateTo)
                {
                    errors.Add(
                        string.Format(
                            ErrorMessageConstants.DateFromAfterDateTo, 
                            lineNumber, 
                            dateFrom.ToString(Constants.DefaultDateFormat), 
                            dateTo.ToString(Constants.DefaultDateFormat)));

                    continue;
                }

                records.Add(new CsvRecord
                {
                    EmpId = empId,
                    ProjectId = projectId,
                    DateFrom = dateFrom,
                    DateTo = dateTo
                });
            }

            return (errors.Count == 0, errors, records);
        }

        private bool TryParseDate(string input, out DateTime date)
        {
            return DateTime.TryParseExact(input,
                _supportedDateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
        }
    }
}
