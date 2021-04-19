using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    public class PagedResult
    {
        public List<Product>? ProductPage { get; set; }
        public List<Customer>? CustomerPage { get; set; }
        public List<Order>? OrderPage { get; set; }
        public List<OrderCustomer>? OrderCustomerPage { get; set; }
        public List<OrderProduct>? OrderProductPage { get; set; }
        public string? PaginationToken { get; set; }
    }
}
