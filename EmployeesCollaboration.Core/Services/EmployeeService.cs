using CsvHelper;
using CsvHelper.Configuration;
using EmployeesCollaboration.Core.Models;
using EmployeesCollaboration.Core.Utils;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace EmployeesCollaboration.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public (List<PairWorkResult> perProject, List<PairTotalResult> totals, PairTotalResult bestPair)
        ProcessCsv(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim
            });

            var records = new List<EmployeeProject>();

            while (csv.Read())
            {
                var empId = csv.GetField<int>(0);
                var projId = csv.GetField<int>(1);
                var dateFrom = DateParser.Parse(csv.GetField(2));
                var dateTo = DateParser.Parse(csv.GetField(3));

                records.Add(new EmployeeProject
                {
                    EmpId = empId,
                    ProjectId = projId,
                    DateFrom = dateFrom,
                    DateTo = dateTo
                });
            }

            return CalculatePairs(records);
        }

        private (List<PairWorkResult> perProject, List<PairTotalResult> totals, PairTotalResult bestPair)
            CalculatePairs(List<EmployeeProject> records)
        {
            var perProject = new List<PairWorkResult>();
            var totalsDict = new Dictionary<(int, int), int>();

            foreach (var group in records.GroupBy(r => r.ProjectId))
            {
                var list = group.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        var e1 = list[i];
                        var e2 = list[j];

                        var overlapStart = e1.DateFrom > e2.DateFrom ? e1.DateFrom : e2.DateFrom;
                        var overlapEnd = e1.DateTo < e2.DateTo ? e1.DateTo : e2.DateTo;

                        if (overlapStart <= overlapEnd)
                        {
                            var days = (overlapEnd - overlapStart).Days + 1;
                            perProject.Add(new PairWorkResult
                            {
                                EmpId1 = Math.Min(e1.EmpId, e2.EmpId),
                                EmpId2 = Math.Max(e1.EmpId, e2.EmpId),
                                ProjectId = e1.ProjectId,
                                DaysWorked = days
                            });

                            var key = (Math.Min(e1.EmpId, e2.EmpId), Math.Max(e1.EmpId, e2.EmpId));
                            if (!totalsDict.ContainsKey(key))
                                totalsDict[key] = 0;

                            totalsDict[key] += days;
                        }
                    }
                }
            }

            var totals = totalsDict
                .Select(kv => new PairTotalResult
                {
                    EmpId1 = kv.Key.Item1,
                    EmpId2 = kv.Key.Item2,
                    TotalDays = kv.Value
                })
                .OrderByDescending(t => t.TotalDays)
                .ToList();

            var bestPair = totals.FirstOrDefault();

            return (perProject, totals, bestPair!);
        }
    }
}
