using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using NathanIanEcom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Controllers
{
    public class ProductController : ControllerBase
    {

        public AmazonDynamoDBClient createContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

        /* gets all products */
        [HttpGet("/api/products/[action]")]
        public async Task<List<Product>> getAllProd()
        {
            //Create a DynamoDB context, which allows for interaction with the DataModel
            AmazonDynamoDBClient client = createContext();
            DynamoDBContext context = new DynamoDBContext(client);

            //Create a List of ScanConditions (can be left blank, as here, to not filter results whatsoever)
            var conditions = new List<ScanCondition>();

            //Run asynchronous Scan on an object, which implicitly picks-up on the item's associated table (outlined in the model)
            var allDocs = await context.ScanAsync<Product>(conditions).GetRemainingAsync();

            //Sort function on the list, should be cleaned-up. Since our ID use-case is prevalent, possibly should make a helper function
            allDocs.Sort(delegate(Product p1, Product p2) { 
                return Int64.Parse(p1.ProductID.Substring(2))
                .CompareTo(Int64.Parse(p2.ProductID.Substring(2))); 
            });

            return allDocs;
        }

        /* gets product by It's ID */

        [HttpGet("api/product/[action]")]
        public async Task<Product> getProductById([FromBody] ProductModel myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<Product>(myInput.ProductID);
                return data;
            }

        }

       

       



    }


}


