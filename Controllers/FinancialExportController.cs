using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;  
using CsvHelper;
using System.Globalization;

namespace ScientiaMobilis.Controllers
{
    public class FinancialExportController : Controller
    {
        private readonly IDataService _dataService;

        public FinancialExportController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult FinancialExport()
        {
            return View("/Views/FinancialExport.cshtml");
        }
    

    [HttpPost]
        public IActionResult ProcessExport(int year, int quarter, string format)
        {
            try
            {
                var (startDate, endDate) = GetDateRange(year, quarter);

                var exportData = _dataService.GetFinancialData(startDate, endDate);

                return format switch
                {
                    "excel" => GenerateExcel(exportData, year, quarter),
                    "csv" => GenerateCsv(exportData),
                    _ => BadRequest("Invalid export format")
                };
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        private (DateTime startDate, DateTime endDate) GetDateRange(int year, int quarter)
        {
            return quarter switch
            {
                1 => (new DateTime(year, 1, 1), new DateTime(year, 3, 31)),
                2 => (new DateTime(year, 4, 1), new DateTime(year, 6, 30)),
                3 => (new DateTime(year, 7, 1), new DateTime(year, 9, 30)),
                4 => (new DateTime(year, 10, 1), new DateTime(year, 12, 31)),
                _ => (new DateTime(year, 1, 1), new DateTime(year, 12, 31))
            };
        }

        private IActionResult GenerateExcel(List<Models.FinancialData> data, int year, int quarter)
        {
            using (var package = new ExcelPackage()) {
                var worksheet = package.Workbook.Worksheets.Add("Financial Data Export");

                worksheet.Cells[1, 1].Value = "Date";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "Amount";
                worksheet.Cells[1, 4].Value = "Category";

                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = data[i].date.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 2].Value = data[i].description;
                    worksheet.Cells[i + 2, 3].Value = data[i].amount;
                    worksheet.Cells[i + 2, 4].Value = data[i].category;
                }

                var excelBytes = package.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                      $"FinancialData_{year}_Q{quarter}.xlsx");

            }
        }

        private IActionResult GenerateCsv(List<Models.FinancialData> data)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
                writer.Flush();

                return File(memoryStream.ToArray(), "text/csv", $"FinancialData_{DateTime.Now:yyyyMMdd}.csv");
            }
        }
    }
    }
