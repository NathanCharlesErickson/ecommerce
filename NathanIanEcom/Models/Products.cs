using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    [DynamoDBTable("IanNathanProducts")]

    public class Products
    {
        [DynamoDBHashKey]

        public String ProductID { get; set; }
        [DynamoDBProperty]

        public String Descirption { get; set; }
        [DynamoDBProperty]

        public String ImageLink { get; set; }
        [DynamoDBProperty]

        public String Name { get; set; }
        [DynamoDBProperty]

        public String Price { get; set; }
    }

    public class ProductModel
    {
        public String ProductID { get; set; }
    }

    

    
}
