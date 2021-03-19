using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanOrders")]
    public class OrderProduct
    {
        [DynamoDBHashKey]
        public String PK { get; set; }
        [DynamoDBRangeKey]
        public String SK { get; set; }
        [DynamoDBProperty]
        public String EntityType { get; set; }
        [DynamoDBProperty]
        public String ProductName { get; set; }
        [DynamoDBProperty]
        public Int64 Quantity { get; set; }
        [DynamoDBProperty]
        public Decimal Price { get; set; }
        [DynamoDBProperty]
        public String ImageLink { get; set; }
        [DynamoDBIgnore]
        public DateTime CreatedDate { get; set; }
        [DynamoDBIgnore]
        public String Status { get; set; }
        [DynamoDBIgnore]
        public String CustomerID { get; set; }
        [DynamoDBIgnore]
        public String Address { get; set; }
        [DynamoDBIgnore]
        public String Email { get; set; }
        [DynamoDBIgnore]
        public String FirstName { get; set; }
        [DynamoDBIgnore]
        public String LastName { get; set; }
        [DynamoDBIgnore]
        public String Username { get; set; }
    }
}
