using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
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
        public void getAllProd()
        {
            AmazonDynamoDBClient client = createContext();
            string tableName = "IanNathanProducts";
            Table ThreadTable = Table.LoadTable(client, tableName);

            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("ProductID", ScanOperator.BeginsWith, "p#");

            ScanOperationConfig config = new ScanOperationConfig()
            {
                AttributesToGet = new List<string> { "ProdctID", "Description", "ImageLink" , "Name", "Price"},
                Filter = scanFilter
            };

            Search search = ThreadTable.Scan(config);

            String test = search.Count.ToString();

            
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


