namespace EmployeesCollaboration.Core.Consts
{
    public class ErrorMessageConstants
    {
        public const string NoFileUploaded = "No file uploaded.";

        public const string InvalidFileType = "Invalid file type. Only CSV files are allowed.";

        public const string EmptyFile = "Empty file uploaded.";

        public const string FileTooLarge = "File size exceeds limit of {0} MB.";

        public const string WrongColumnCount = "Line {0}: Expected 4 columns but found {1}.";

        public const string InvalidEmpId = "Line {0}: Invalid EmpID '{1}'. Must be a positive integer.";
        
        public const string InvalidProjectId = "Line {0}: Invalid ProjectID '{1}'. Must be a positive integer.";
        
        public const string InvalidDateFrom = "Line {0}: Invalid DateFrom '{1}'.";
        
        public const string InvalidDateTo = "Line {0}: Invalid DateTo '{1}'.";
        
        public const string DateFromAfterDateTo = "Line {0}: DateFrom '{1}' is after DateTo '{2}'.";
        
        public const string DateFromInFuture = "Line {0}: DateFrom '{1}' cannot be in the future.";
    }
}
