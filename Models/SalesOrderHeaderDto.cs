namespace WebApplication_scuffolding_reverse.Models
{
    public class SalesOrderHeaderDto
    {
        public int? SalesOrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string SalesOrderNumber { get; set; } = null!;
        public string? PurchaseOrderNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? ShipMethod { get; set; } = null!;
        public decimal? TotalDue { get; set; }
        public string? Comment { get; set; }
    }
}