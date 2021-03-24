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

}
