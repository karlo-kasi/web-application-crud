namespace WebApplication_scuffolding_reverse.Models
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string? Title { get; set; }

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? SalesPerson { get; set; }

        public string? EmailAddress { get; set; }

        public string? Phone { get; set; }

        public virtual List<CustomerAddressDto> CustomerAddresses { get; set; } = new();

        public virtual List<SalesOrderHeaderDto> SalesOrderHeaders { get; set; } = new();
    }
}
