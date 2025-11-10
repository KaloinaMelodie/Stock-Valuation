using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models
{
    public class MonthlyRevenue
    {
        public int Year { get; set; }
        public int Month { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2} MGA")]
        public double Revenue { get; set; }
    }

}
