using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanCustomersOrders")]
    public class CustomerOrders
    {
        [DynamoDBHashKey]
        public String PK { get; set; }
        [DynamoDBRangeKey]
        public String SK { get; set; }
        [DynamoDBProperty]

        public String EntityType { get; set; }
        [DynamoDBProperty]

        public String Email { get; set; }
        [DynamoDBProperty]

        public String FirstName { get; set; }
        [DynamoDBProperty]

        public String LastName { get; set; }
        [DynamoDBProperty]

        public String Username { get; set; }
        [DynamoDBProperty]

        public String Address { get; set; }
        [DynamoDBProperty]

        public String CreateDate { get; set; }
        [DynamoDBProperty]

        public String CustomerId { get; set; }
        [DynamoDBProperty]

        public String ImageLink { get; set; }
        [DynamoDBProperty]

        public String PassHash { get; set; }
        [DynamoDBProperty]

        public String Price { get; set; }
        [DynamoDBProperty]

        public String ProductName { get; set; }
        [DynamoDBProperty]

        public Int64 Quantity { get; set; }
        [DynamoDBProperty]

        public String Status { get; set; }
    }

    public class PKSKModel
    {
        public String PK { get; set; }
        public String SK { get; set; }
    }

    public class CustIdModel
    {
        public String CustomerID { get; set; }
        public String SK { get; set; }
    }
}
