using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanOrders")]

    public class OrderCustomer
    {
        [DynamoDBHashKey]
        public String PK { get; set; }
        [DynamoDBRangeKey]
        public String SK { get; set; }
        [DynamoDBProperty]
        public String EntityType { get; set; }
        [DynamoDBProperty]
        public String Address { get; set; }
        [DynamoDBProperty]
        public String Email { get; set; }
        [DynamoDBProperty]
        public String FirstName { get; set; }
        [DynamoDBProperty]
        public String LastName { get; set; }
        [DynamoDBProperty]
        public String Username { get; set; }
        [DynamoDBIgnore]
        public DateTime CreatedDate { get; set; }
        [DynamoDBIgnore]
        public String Status { get; set; }
        [DynamoDBIgnore]
        public String CustomerID { get; set; }
        [DynamoDBIgnore]
        public String ProductName { get; set; }
        [DynamoDBIgnore]
        public Int64 Quantity { get; set; }
        [DynamoDBIgnore]
        public Decimal Price { get; set; }
        [DynamoDBIgnore]
        public String ImageLink { get; set; }
    }
}
