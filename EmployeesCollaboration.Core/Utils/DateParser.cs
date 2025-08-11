using EmployeesCollaboration.Core.Consts;
using System.Globalization;

namespace EmployeesCollaboration.Core.Utils
{
    public static class DateParser
    {
        private static readonly string[] Formats = Constants.DateFormats;

        public static DateTime Parse(string? input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Trim().ToUpper() == "NULL")
                return DateTime.Today;

            if (DateTime.TryParseExact(input.Trim(), Formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var date))
                return date;

            if (DateTime.TryParse(input, out date))
                return date;

            throw new FormatException($"Invalid date format: {input}");
        }
    }
}
