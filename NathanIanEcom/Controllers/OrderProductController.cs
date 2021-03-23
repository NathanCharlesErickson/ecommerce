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
                    await order.PutItemAsync(unwrapOrderProduct(myOrder));
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

                    Document updatedOrderProduct = await orders.UpdateItemAsync(unwrapOrderProduct(myInput), config);
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return StatusCode(500);
                }

            }

        }

        [HttpGet("/api/orderProduct/[action]")]
        public async Task<List<OrderProduct>> getAllProdByOrderId([FromBody] QueryOptions myInput)
        {

            using (var context = new DynamoDBContext(createContext()))
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

        private Document unwrapOrderProduct(OrderProduct myOrder)
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

        private OrderProduct wrapOrderProduct(Document item)
        {
            OrderProduct myProd = new OrderProduct();
            myProd.PK = item["PK"];
            myProd.SK = item["SK"];
            myProd.EntityType = item["EntityType"];
            myProd.ProductName = item["ProductName"];
            myProd.Quantity = item["Quantity"];
            myProd.Price = item["Price"];
            myProd.ImageLink = item["ImageLink"];

            return myProd;

        }

    }
}
