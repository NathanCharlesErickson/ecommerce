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
    public class OrderCustomerController : ControllerBase
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


        /*Gets a Order's Customer by it's Id*/

        [HttpGet("api/orderCustomer/[action]")]
        public async Task<OrderCustomer> getOrderCustomerById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<OrderCustomer>(myInput.PK, myInput.SK);
                return data;
            }

        }

        /*Create a Order's Customer*/

        [HttpPost("/api/orderCustomer/[action]")]
        public async Task<StatusCodeResult> loadOrderCustomer([FromBody] OrderCustomer myOrder)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table order = Table.LoadTable(createContext(), "IanNathanOrders");
                try
                {
                    await order.PutItemAsync(unwarpOrderCustomer(myOrder));
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpDelete("api/orderCustomer/[action]")]
        public async Task<StatusCodeResult> deleteOrderCustomer([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                try
                {
                    await context.DeleteAsync<OrderCustomer>(myInput.PK, myInput.SK);
                    return StatusCode(204);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/orderCustomer/[action]")]
        public async Task<StatusCodeResult> updateOrderCustomer([FromBody] OrderCustomer myInput)
        {
            using (AmazonDynamoDBClient context = createContext())
            {
                var data = getOrderCustomerById(new QueryOptions { PK = myInput.PK, SK = myInput.SK });
                //Make sure the Order exists or else AsyncPutItem would create a new item. Not sure if this is needed. 
                if (data == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    try
                    {
                        Dictionary<string, AttributeValue> myDic = orderCustomerDictionary(myInput);
                        PutItemRequest myConfg = new PutItemRequest
                        {
                            TableName = "IanNathanOrders",
                            Item = myDic
                        };
                        await context.PutItemAsync(myConfg);
                        return StatusCode(204);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500);
                    }
                }


            }

        }

        private Document unwarpOrderCustomer(OrderCustomer myOrder)
        {
            Document doc = new Document();

            doc["PK"] = myOrder.PK;
            doc["SK"] = myOrder.SK;
            doc["EntityType"] = myOrder.EntityType;
            doc["Address"] = myOrder.Address;
            doc["Email"] = myOrder.Email;
            doc["FirstName"] = myOrder.FirstName;
            doc["LastName"] = myOrder.LastName;
            doc["Username"] = myOrder.Username;

            return doc;
        }

        private Dictionary<string, AttributeValue> orderCustomerDictionary(OrderCustomer myOrder)
        {
            Dictionary<string, AttributeValue> orderDic = new Dictionary<string, AttributeValue>();
            orderDic["PK"] = new AttributeValue { S = myOrder.PK };
            orderDic["SK"] = new AttributeValue { S = myOrder.SK };
            orderDic["EntityType"] = new AttributeValue { S = myOrder.EntityType };
            orderDic["Address"] = new AttributeValue { S = myOrder.Address };
            orderDic["Email"] = new AttributeValue { S = myOrder.Email };
            orderDic["FirstName"] = new AttributeValue { S = myOrder.FirstName };
            orderDic["LastName"] = new AttributeValue { S = myOrder.LastName };
            orderDic["Username"] = new AttributeValue { S = myOrder.Username };

            return orderDic;
        }


    }
}
