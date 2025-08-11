namespace EmployeesCollaboration.Core.Consts
{
    public static class Constants
    {
        public static string[] DateFormats =
        [
            "yyyy-MM-dd",       // 2025-08-11
            "MM/dd/yyyy",       // 08/11/2025
            "dd/MM/yyyy",       // 11/08/2025
            "dd-MM-yyyy",       // 11-08-2025
            "MM-dd-yyyy",       // 08-11-2025
            "yyyy/MM/dd",       // 2025/08/11
            "yyyy.MM.dd",       // 2025.08.11
            "dd.MM.yyyy",       // 11.08.2025
            "dd MMM yyyy",      // 11 Aug 2025
            "MMM dd, yyyy",     // Aug 11, 2025
            "MMMM dd, yyyy",    // August 11, 2025
            "dd MMMM yyyy",     // 11 August 2025
            "yyyyMMdd",         // 20250811
            "ddMMyyyy",         // 11082025
            "MMMyy",            // Aug25
            "MMM-yyyy",         // Aug-2025
            "MM/yyyy",          // 08/2025
            "yyyy/MM",          // 2025/08
            "yyyy-MM-ddTHH:mm:ss",  // 2025-08-11T14:30:00
            "MM/dd/yyyy HH:mm",     // 08/11/2025 14:30
            "dd/MM/yyyy HH:mm",     // 11/08/2025 14:30
            "yyyy-MM-dd HH:mm:ss",  // 2025-08-11 14:30:00
            "yyyy-MM-ddTHH:mm:ssZ", // 2025-08-11T14:30:00Z (UTC)
            "o",                    // 2025-08-11T14:30:00.0000000Z (Round-trip)
            "s"                     // 2025-08-11T14:30:00 (Sortable)
        ];

        public static string CsvFileFormat = ".csv";

        public static string DefaultDateFormat = "yyyy-MM-dd";
    }
}
