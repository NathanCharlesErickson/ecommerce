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
        public String CreatedDate { get; set; }
        [DynamoDBProperty]
        public String Status { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey]
        public String Username { get; set; }
        [DynamoDBProperty]
        public String Address { get; set; }
    }
}
