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
    public class OrderProductController : Helper
    {

      

        /*Gets a Order's Product by it's Id*/

        [HttpPost("api/orderProduct/[action]")]
        public async Task<OrderProduct> GetOrderProductById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                var data = await context.LoadAsync<OrderProduct>(myInput.PK, myInput.SK);
                return data;
            }

        }

        /*Create a Order's Product*/

        [HttpPost("/api/orderProduct/[action]")]
        public async Task<IActionResult> LoadOrderProduct([FromBody] OrderProduct myOrder)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table order = Table.LoadTable(CreateContext(), "IanNathanOrders");
                try
                {
                    await order.PutItemAsync(UnwrapOrderProduct(myOrder));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpDelete("api/orderProduct/[action]")]
        public async Task<IActionResult> DeleteOrderPrduct([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                try
                {
                    await context.DeleteAsync<OrderProduct>(myInput.PK, myInput.SK);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/orderProduct/[action]")]
        public async Task<IActionResult> updateOrderProduct([FromBody] OrderProduct myInput)
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

                    Document updatedOrderProduct = await orders.UpdateItemAsync(UnwrapOrderProduct(myInput), config);
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }

            }

        }

        [HttpPost("/api/orderProduct/[action]")]
        public async Task<List<OrderProduct>> getAllProdByOrderId([FromBody] QueryOptions myInput)
        {

            using (var context = new DynamoDBContext(CreateContext()))
            {
                Expression expr = new Expression();
                expr.ExpressionStatement = "PK = :PK and begins_with(SK, :prodPrefix)";
                expr.ExpressionAttributeValues[":PK"] = myInput.PK;
                expr.ExpressionAttributeValues[":prodPrefix"] = "p#";

                QueryOperationConfig config = new QueryOperationConfig()
                {
                    KeyExpression = expr
                 
                };

                var associatedProducts = await context.FromQueryAsync<OrderProduct>(config).GetRemainingAsync();

                return associatedProducts;
            }

        }

        [HttpPost("api/orderProduct/[action]")]
        public async void LoadOrderProducts([FromBody] QueryOptions queryOptions)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                if(queryOptions.OrderProducts != null)
                {
                    Table products = Table.LoadTable(CreateContext(), "IanNathanOrders");
                    var batchWrite = products.CreateBatchWrite();

                    foreach (OrderProduct orderProd in queryOptions.OrderProducts)
                    {
                        batchWrite.AddDocumentToPut(UnwrapOrderProduct(orderProd));
                    }

                    await batchWrite.ExecuteAsync();
                }
            }

        }

    }
}
