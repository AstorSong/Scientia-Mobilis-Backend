namespace ScientiaMobilis.Models
{
    public class FinancialData
    {

        public DateTime date { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public string category
        {
            get; set;
        }
    }
}
