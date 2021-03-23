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
    public class ProductController : Helper
    {

        /* gets all products */
        [HttpGet("/api/product/[action]")]
        public async Task<List<Product>> GetAllProd()
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                //Create a List of ScanConditions (can be left blank, as here, to not filter results whatsoever)
                var conditions = new List<ScanCondition>();

                //Run asynchronous Scan on an object, which implicitly picks-up on the item's associated table (outlined in the model)
                var allDocs = await context.ScanAsync<Product>(conditions).GetRemainingAsync();

                //Sort function on the list, should be cleaned-up. Since our ID use-case is prevalent, possibly should make a helper function
                allDocs.Sort(delegate (Product p1, Product p2) {
                    return Int64.Parse(p1.ProductID.Substring(2))
                    .CompareTo(Int64.Parse(p2.ProductID.Substring(2)));
                });

                return allDocs;
            }

        }

        /* gets product by It's ID */

        [HttpGet("api/product/[action]")]
        public async Task<Product> GetProductById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                var data = await context.LoadAsync<Product>(myInput.ProductID);
                return data;
            }

        }
        /*Create, Delete, Update Product */


        /* Create Product */
        [HttpPost("/api/product/[action]")]
        public async Task<StatusCodeResult> LoadProduct([FromBody] Product myProd)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table products = Table.LoadTable(CreateContext(), "IanNathanProducts");
                try
                {
                    await products.PutItemAsync(UnwrapProduct(myProd));
                    return StatusCode(201);
                } catch (Exception ex)
                {
                   return StatusCode(500);
                }
                
            }

        }

        /*delete a product by PK SK */
        [HttpDelete("api/product/[action]")]
        public async Task<StatusCodeResult> DeleteProduct([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
               try
                {
                    await context.DeleteAsync<Product>(myInput.ProductID);
                    return StatusCode(204);
                } catch(Exception ex)
                {
                    return StatusCode(500);
                }
                   
            }

        }

        [HttpPut("api/product/[action]")]
        public async Task<StatusCodeResult> UpdateProduct([FromBody] Product myInput)
        {
            using (AmazonDynamoDBClient context = CreateContext())
            {
                try
                {
                    Table products = Table.LoadTable(CreateContext(), "IanNathanProducts");
                    Expression expr = new Expression();
                    expr.ExpressionStatement = "ProductID = :productIDVal";
                    expr.ExpressionAttributeValues[":productIDVal"] = myInput.ProductID;

                    UpdateItemOperationConfig config = new UpdateItemOperationConfig
                    {
                        ConditionalExpression = expr
                    };

                    Document updatedProduct = await products.UpdateItemAsync(UnwrapProduct(myInput), config);
                    return StatusCode(200);
                } catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }
                
            }

        }

       








    }


}


