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
    public class OrderController : Helper
    {
        

        /*Gets a Order by it's Id*/

        [HttpGet("api/order/[action]")]
        public async Task<Order> GetOrderById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                var data = await context.LoadAsync<Order>(myInput.PK, myInput.SK);
                return data;
            }
            
        }

      

        /*Create Order */

        [HttpPost("/api/order/[action]")]
        public async Task<StatusCodeResult> LoadOrder([FromBody] Order myOrder)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table order = Table.LoadTable(CreateContext(), "IanNathanOrders");
                try
                {
                    await order.PutItemAsync(UnwrapOrder(myOrder));
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        //delete order

        [HttpDelete("api/order/[action]")]
        public async Task<StatusCodeResult> DeleteOrder([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                try
                {
                    await context.DeleteAsync<Order>(myInput.PK, myInput.SK);
                    return StatusCode(204);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/order/[action]")]
        public async Task<StatusCodeResult> UpdateOrder([FromBody] Order myInput)
        {
            using (AmazonDynamoDBClient context = CreateContext())
            {
                try
                {
                    Table orders = Table.LoadTable(CreateContext(), "IanNathanOrders");
                    Expression expr = new Expression();
                    expr.ExpressionStatement = "PK = :PK and SK = :SK";
                    expr.ExpressionAttributeValues[":PK"] = myInput.PK;
                    expr.ExpressionAttributeValues[":SK"] = myInput.SK;

                    UpdateItemOperationConfig config = new UpdateItemOperationConfig
                    {
                        ConditionalExpression = expr
                    };

                    Document updatedOrder = await orders.UpdateItemAsync(UnwrapOrder(myInput), config);
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }

            }

        }

        [HttpGet("/api/order/[action]")]
        public async Task<List<Order>> GetOrderByCustId([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Expression expr = new Expression();
                expr.ExpressionStatement = "Username = :Username";
                expr.ExpressionAttributeValues[":Username"] = myInput.Username;

                

                QueryOperationConfig config = new QueryOperationConfig()
                {
                    KeyExpression = expr,
                    IndexName = "GSI1",
                    BackwardSearch = true, //scans backward saw on the doc unsure if needed
                    AttributesToGet = new List<string> {"Username", "Address", "SK", "EntityType", "CreatedDate", "Status" },
                    Select = SelectValues.SpecificAttributes,
                    Limit = 1

                   
                };

                var associatedProducts = await context.FromQueryAsync<Order>(config).GetNextSetAsync();

                return associatedProducts;
            }

        }

        [HttpGet("/api/order/[action]")]
        public async Task<List<Order>> GetAllOrderByCustId([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Expression expr = new Expression();
                expr.ExpressionStatement = "Username = :Username and begins_with(SK, :prefix)";
                expr.ExpressionAttributeValues[":Username"] = myInput.Username;
                expr.ExpressionAttributeValues[":prefix"] = "o#";



                QueryOperationConfig config = new QueryOperationConfig()
                {
                    KeyExpression = expr,
                    IndexName = "GSI1",
                    AttributesToGet = new List<string> { "Username", "Address", "SK", "EntityType", "CreatedDate", "Status" },
                    Select = SelectValues.SpecificAttributes

                };

                var associatedProducts = await context.FromQueryAsync<Order>(config).GetNextSetAsync();

                return associatedProducts;
            }


        }







        
    }

   





    }

   

