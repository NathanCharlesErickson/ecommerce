using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanCustomers")]
    public class Customer
    {
        [DynamoDBHashKey]
        public String Username { get; set; }
        [DynamoDBRangeKey]
        public String CustomerID { get; set; }
        [DynamoDBProperty]
        public String Address { get; set; }
        [DynamoDBProperty]
        public String Email { get; set; }
        [DynamoDBProperty]
        public String FirstName { get; set; }
        [DynamoDBProperty]
        public String LastName { get; set; }
        [DynamoDBProperty]
        public String PassHash { get; set; }
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
