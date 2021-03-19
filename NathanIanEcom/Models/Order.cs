using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanOrders")]
    public class Order
    {
        [DynamoDBHashKey]
        public String PK { get; set; }
        [DynamoDBRangeKey]
        public String SK { get; set; }
        [DynamoDBProperty]
        public String EntityType { get; set; }
        [DynamoDBProperty]
        public DateTime CreatedDate { get; set; }
        [DynamoDBProperty]
        public String Status { get; set; }
        [DynamoDBProperty]
        public String CustomerID { get; set; }
        [DynamoDBProperty]
        public String Address { get; set; }
        [DynamoDBIgnore]
        public String Email { get; set; }
        [DynamoDBIgnore]
        public String FirstName { get; set; }
        [DynamoDBIgnore]
        public String LastName { get; set; }
        [DynamoDBIgnore]
        public String Username { get; set; }
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
