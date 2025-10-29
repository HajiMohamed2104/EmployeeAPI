using EmployeeApi.Models;
using System.Text;

namespace EmployeeApi.Services
{
    public class CsvExportService
    {
        public byte[] GenerateEmployeeCsv(List<Employee> employees)
        {
            if (employees == null || !employees.Any())
                return Array.Empty<byte>();

            var csvContent = new StringBuilder();
            
            // Add CSV header
            csvContent.AppendLine("Id,Name,Position");
            
            // Add employee data
            foreach (var employee in employees)
            {
                csvContent.AppendLine($"{employee.Id},{EscapeCsvField(employee.Name)},{EscapeCsvField(employee.Position)}");
            }
            
            return Encoding.UTF8.GetBytes(csvContent.ToString());
        }

        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field))
                return string.Empty;
                
            // Escape fields that contain commas, quotes, or newlines
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            {
                field = field.Replace("\"", "\"\"");
                return $"\"{field}\"";
            }
            
            return field;
        }
    }
}