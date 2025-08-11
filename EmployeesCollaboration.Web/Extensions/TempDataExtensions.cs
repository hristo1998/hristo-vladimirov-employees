using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace EmployeesCollaboration.Web.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
        {
            if (value == null)
            {
                tempData.Remove(key);
                return;
            }

            tempData[key] = JsonSerializer.Serialize(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key)
        {
            if (!tempData.TryGetValue(key, out var obj) || obj is not string json)
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
