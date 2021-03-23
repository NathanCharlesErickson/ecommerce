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
                    await order.PutItemAsync(unwrapOrder(myOrder));
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
                try
                {
                    Table orders = Table.LoadTable(createContext(), "IanNathanOrders");
                    Expression expr = new Expression();
                    expr.ExpressionStatement = "PK = :PK and SK = :SK";
                    expr.ExpressionAttributeValues[":PK"] = myInput.PK;
                    expr.ExpressionAttributeValues[":SK"] = myInput.SK;

                    UpdateItemOperationConfig config = new UpdateItemOperationConfig
                    {
                        ConditionalExpression = expr
                    };

                    Document updatedOrder = await orders.UpdateItemAsync(unwrapOrder(myInput), config);
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }

            }

        }

        //Currently a work in progress
        [HttpGet("/api/order/[action]")]
        public async Task<List<OrderProduct>> getOrderByCustId([FromBody] QueryOptions myInput)
        {
            AmazonDynamoDBClient client = createContext();
            DynamoDBContext context = new DynamoDBContext(client);

            Dictionary<string, Condition> myDic = new Dictionary<string, Condition>();

            ComparisonOperator myOp = new ComparisonOperator("EQ");



            myDic.Add("CustomerID", new Condition { AttributeValueList = { new AttributeValue { S = myInput.CustomerID } }, ComparisonOperator = myOp  });

            QueryRequest config = new QueryRequest()
            {
                TableName = "IanNathanOrders",
                IndexName = "GSI1",
                ScanIndexForward = false,
                QueryFilter = myDic,


            };

            var data = await client.QueryAsync(config);
            

            return null;
        }







        private Document unwrapOrder(Order myOrder)
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

   

