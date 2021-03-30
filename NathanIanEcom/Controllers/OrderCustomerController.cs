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
    public class OrderCustomerController : Helper
    {
       


        /*Gets a Order's Customer by it's Id*/

        [HttpGet("api/orderCustomer/[action]")]
        public async Task<OrderCustomer> GetOrderCustomerById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                var data = await context.LoadAsync<OrderCustomer>(myInput.PK, myInput.SK);
                return data;
            }

        }

        /*Create a Order's Customer*/

        [HttpPost("/api/orderCustomer/[action]")]
        public async Task<IActionResult> LoadOrderCustomer([FromBody] OrderCustomer myOrder)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table order = Table.LoadTable(CreateContext(), "IanNathanOrders");
                try
                {
                    await order.PutItemAsync(UnwrapOrderCustomer(myOrder));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpDelete("api/orderCustomer/[action]")]
        public async Task<IActionResult> DeleteOrderCustomer([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                try
                {
                    await context.DeleteAsync<OrderCustomer>(myInput.PK, myInput.SK);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/orderCustomer/[action]")]
        public async Task<IActionResult> UpdateOrderCustomer([FromBody] OrderCustomer myInput)
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

                    Document updatedOrderCustomer = await orders.UpdateItemAsync(UnwrapOrderCustomer(myInput), config);
                    return Ok();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }

            }

        }

        


    }
}
