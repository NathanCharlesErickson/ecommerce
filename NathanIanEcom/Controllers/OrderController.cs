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
    public class OrderController : ControllerBase
    {

        public AmazonDynamoDBClient createContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

        //CRUD 


        /*Gets a Order by it's Id*/

        [HttpGet("api/order/[action]")]
        public async Task<Order> getOrderById([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<Order>(myInput.PK, myInput.SK);
                return data;
            }
            
        }

      

        /*Create Order */

        [HttpPost("/api/order/[action]")]
        public async Task<StatusCodeResult> loadOrder([FromBody] Order myOrder)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table order = Table.LoadTable(createContext(), "IanNathanOrders");
                try
                {
                    await order.PutItemAsync(unwarpOrder(myOrder));
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
        public async Task<StatusCodeResult> deleteOrder([FromBody] QueryOptions myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
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
        public async Task<StatusCodeResult> updateOrder([FromBody] Order myInput)
        {
            using (AmazonDynamoDBClient context = createContext())
            {
                var data = getOrderById(new QueryOptions { PK = myInput.PK, SK = myInput.SK});
                //Make sure the Order exists or else AsyncPutItem would create a new item. Not sure if this is needed. 
                if (data == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    try
                    {
                        Dictionary<string, AttributeValue> myDic = orderDictionary(myInput);
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





        /* gets all products by Order Id */
        [HttpGet("/api/order/[action]")]
        public async void getAllProdByOrderId([FromBody] QueryOptions myInput)
        {
            AmazonDynamoDBClient client = createContext();
            DynamoDBContext context = new DynamoDBContext(client);

            var conditions = new List<QueryCondition>();
            conditions.Add(new QueryCondition("PK", QueryOperator.Equal, myInput.PK));
            conditions.Add(new QueryCondition("SK", QueryOperator.BeginsWith, myInput.SK));

            List<Order> myLIst = await context.QueryAsync<Order>(conditions).GetRemainingAsync();
            



        }

       

       

        private Document unwarpOrder(Order myOrder)
        {
            Document myDoc = new Document();

            myDoc["PK"] = myOrder.PK;
            myDoc["SK"] = myOrder.SK;
            myDoc["EntityType"] = myOrder.EntityType;
            myDoc["CreatedDate"] = myOrder.CreatedDate;
            myDoc["Status"] = myOrder.Status;
            myDoc["CustomerID"] = myOrder.CustomerID;
            myDoc["Address"] = myOrder.Address;

            return myDoc;
        }

        private Dictionary<string, AttributeValue> orderDictionary(Order myOrder)
        {
            Dictionary<string, AttributeValue> orderDic = new Dictionary<string, AttributeValue>();
            orderDic["PK"] = new AttributeValue { S = myOrder.PK };
            orderDic["SK"] = new AttributeValue { S = myOrder.SK };
            orderDic["EntityType"] = new AttributeValue { S = myOrder.EntityType };
            orderDic["CreatedDate"] = new AttributeValue { S = myOrder.CreatedDate };
            orderDic["Status"] = new AttributeValue { S = myOrder.Status };
            orderDic["CustomerID"] = new AttributeValue { S = myOrder.CustomerID };
            orderDic["Address"] = new AttributeValue { S = myOrder.Address };
            return orderDic;
        }
    }

   





    }

   

