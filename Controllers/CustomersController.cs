using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_scuffolding_reverse.Models;

namespace WebApplication_scuffolding_reverse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        public CustomersController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers
                .Include(c => c.CustomerAddresses)
                .Include(c => c.SalesOrderHeaders)
                .ToListAsync();
        }

        [HttpGet("dto")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersDto()
        {
            var customersDto = await _context.Customers
                .AsNoTracking()
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    Title = c.Title,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CompanyName = c.CompanyName,
                    SalesPerson = c.SalesPerson,
                    EmailAddress = c.EmailAddress,
                    Phone = c.Phone,
                    CustomerAddresses = c.CustomerAddresses.Select(ca => new CustomerAddressDto
                    {
                        AddressId = ca.AddressId,
                        AddressType = ca.AddressType,
                    }).ToList(),
                    SalesOrderHeaders = c.SalesOrderHeaders.Select(so => new SalesOrderHeaderDto
                    {
                        SalesOrderId = so.SalesOrderId,
                        OrderDate = so.OrderDate,
                        SalesOrderNumber = so.SalesOrderNumber,
                        PurchaseOrderNumber = so.PurchaseOrderNumber,
                        CustomerId = so.CustomerId,
                        ShipMethod = so.ShipMethod,
                        TotalDue = ((int)so.TotalDue),
                        Comment = so.Comment
                    }).ToList()
                }).ToListAsync();
            return customersDto;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
