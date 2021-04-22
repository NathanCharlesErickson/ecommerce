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
    public class CustomerController : Helper 
    {
        
        //CRUD\


        /*Gets a Customer by it's Id*/

        [HttpPost("api/customer/[action]")]
        public async Task<Customer> getCustomerById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                var data = await context.LoadAsync<Customer>(myInput.Username);
                return data;
            }

        }

        /*Create a Customer*/

        [HttpPost("/api/customer/[action]")]
        public async Task<IActionResult> loadCustomer([FromBody] Customer myCustomer)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table customers = Table.LoadTable(CreateContext(), "IanNathanCustomers");
                try
                {
                    await customers.PutItemAsync(UnwrapCustomer(myCustomer));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpDelete("api/customer/[action]")]
        public async Task<IActionResult> deleteCustomer([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                try
                {
                    await context.DeleteAsync<Customer>(myInput.Username);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/customer/[action]")]
        public async Task<IActionResult> updateCustomer([FromBody] Customer myInput)
        {
            using (AmazonDynamoDBClient context = CreateContext())
            {
                try
                {
                    Table customers = Table.LoadTable(CreateContext(), "IanNathanCustomers");
                    Expression expr = new Expression();
                    expr.ExpressionStatement = ":Username = Username";
                    expr.ExpressionAttributeValues[":Username"] = myInput.Username;

                    UpdateItemOperationConfig config = new UpdateItemOperationConfig
                    {
                        ConditionalExpression = expr
                    };

                    Document updatedCustomer = await customers.UpdateItemAsync(UnwrapCustomer(myInput), config);
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
