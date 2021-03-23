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
    public class OrderProductController : ControllerBase
    {

        public AmazonDynamoDBClient createContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

        /*Gets a Order's Product by it's Id*/

        [HttpGet("api/orderProduct/[action]")]
        public async Task<OrderProduct> getOrderProductById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<OrderProduct>(myInput.PK, myInput.SK);
                return data;
            }

        }

        /*Create a Order's Product*/

        [HttpPost("/api/orderProduct/[action]")]
        public async Task<StatusCodeResult> loadOrderProduct([FromBody] OrderProduct myOrder)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table order = Table.LoadTable(createContext(), "IanNathanOrders");
                try
                {
                    await order.PutItemAsync(unwarpOrderProduct(myOrder));
                    return StatusCode(201);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpDelete("api/orderProduct/[action]")]
        public async Task<StatusCodeResult> deleteOrderPrduct([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                try
                {
                    await context.DeleteAsync<OrderProduct>(myInput.PK, myInput.SK);
                    return StatusCode(204);
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }

            }

        }

        [HttpPut("api/orderProduct/[action]")]
        public async Task<StatusCodeResult> updateOrderProduct([FromBody] OrderProduct myInput)
        {
            using (AmazonDynamoDBClient context = createContext())
            {
                var data = getOrderProductById(new QueryOptions { PK = myInput.PK, SK = myInput.SK });
                //Make sure the Order exists or else AsyncPutItem would create a new item. Not sure if this is needed. 
                if (data == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    try
                    {
                        Dictionary<string, AttributeValue> myDic = orderProductDictionary(myInput);
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

        private Document unwarpOrderProduct(OrderProduct myOrder)
        {
            Document doc = new Document();

            doc["PK"] = myOrder.PK;
            doc["SK"] = myOrder.SK;
            doc["EntityType"] = myOrder.EntityType;
            doc["ProductName"] = myOrder.ProductName;
            doc["Quantity"] = myOrder.Quantity;
            doc["Price"] = myOrder.Price;
            doc["ImageLink"] = myOrder.ImageLink;

            return doc;
        }

        private Dictionary<string, AttributeValue> orderProductDictionary(OrderProduct myOrder)
        {
            Dictionary<string, AttributeValue> orderDic = new Dictionary<string, AttributeValue>();
            orderDic["PK"] = new AttributeValue { S = myOrder.PK };
            orderDic["SK"] = new AttributeValue { S = myOrder.SK };
            orderDic["EntityType"] = new AttributeValue { S = myOrder.EntityType };
            orderDic["ProductName"] = new AttributeValue { S = myOrder.ProductName };
            orderDic["Quantity"] = new AttributeValue { S = myOrder.Quantity };
            orderDic["Price"] = new AttributeValue { S = myOrder.Price };
            orderDic["ImageLink"] = new AttributeValue { S = myOrder.ImageLink };

            return orderDic;
        }

    }
}
