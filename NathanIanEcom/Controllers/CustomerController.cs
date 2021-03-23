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
    public class CustomerController : ControllerBase
    {
        public AmazonDynamoDBClient createContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

        //CRUD\


        /*Gets a Customer by it's Id*/

        [HttpGet("api/customer/[action]")]
        public async Task<Customer> getCustomerById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<Customer>(myInput.Username);
                return data;
            }

        }

        /*Create a Customer*/

        [HttpPost("/api/customer/[action]")]
        public async Task<StatusCodeResult> loadCustomer([FromBody] Customer myCustomer)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table customers = Table.LoadTable(createContext(), "IanNathanCustomers");
                try
                {
                    await customers.PutItemAsync(unwrapCustomer(myCustomer));
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpDelete("api/customer/[action]")]
        public async Task<StatusCodeResult> deleteCustomer([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                try
                {
                    await context.DeleteAsync<Customer>(myInput.Username);
                    return StatusCode(204);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/customer/[action]")]
        public async Task<StatusCodeResult> updateCustomer([FromBody] Customer myInput)
        {
            using (AmazonDynamoDBClient context = createContext())
            {
                try
                {
                    Table customers = Table.LoadTable(createContext(), "IanNathanCustomers");
                    Expression expr = new Expression();
                    expr.ExpressionStatement = ":Username = Username";
                    expr.ExpressionAttributeValues[":Username"] = myInput.Username;

                    UpdateItemOperationConfig config = new UpdateItemOperationConfig
                    {
                        ConditionalExpression = expr
                    };

                    Document updatedCustomer = await customers.UpdateItemAsync(unwrapCustomer(myInput), config);
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }

            }

        }

        private Document unwrapCustomer(Customer customer)
        {
            Document custDoc = new Document();
            custDoc["Username"] = customer.Username;
            custDoc["CustomerID"] = customer.CustomerID;
            custDoc["Address"] = customer.Address;
            custDoc["Email"] = customer.Email;
            custDoc["FirstName"] = customer.FirstName;
            custDoc["LastName"] = customer.LastName;
            custDoc["PassHash"] = customer.PassHash;
            return custDoc;
        }


    }
}
