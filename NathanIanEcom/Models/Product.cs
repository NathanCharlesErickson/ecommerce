using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanProducts")]

    public class Product
    {
        [DynamoDBHashKey]
        public String ProductID { get; set; }
        [DynamoDBProperty]
        public String Description { get; set; }
        [DynamoDBProperty]
        public String ImageLink { get; set; }
        [DynamoDBProperty]
        public String Name { get; set; }
        [DynamoDBProperty]
        public String Price { get; set; }
        [DynamoDBProperty]
        public String Category { get; set; }
    }

  

    

    
}
