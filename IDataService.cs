using ScientiaMobilis.Models;

public interface IDataService
{
    List<FinancialData> GetFinancialData(DateTime startDate, DateTime endDate);
}

public class DataService : IDataService
{
    public List<FinancialData> GetFinancialData(DateTime startDate, DateTime endDate)
    {
        // Replace with your actual data access
        return new List<FinancialData>
        {
            new FinancialData
            {
                date = DateTime.Now.AddDays(-2),
                description = "Office Supplies",
                amount = 125.50m,
                category = "Expenses"
            },
            new FinancialData
            {
                date = DateTime.Now.AddDays(-1),
                description = "Client Payment",
                amount = 2500.00m,
                category = "Income"
            }
        };
    }
}