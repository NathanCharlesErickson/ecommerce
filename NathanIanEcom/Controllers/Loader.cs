using Amazon.DynamoDBv2;
using Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NathanIanEcom.Models;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace NathanIanEcom.Controllers
{
    public class Loader : ControllerBase
    {

        public AmazonDynamoDBClient createContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

        #region Unwrapping functions
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

        private Document unwrapProduct(Product product)
        {
            Document custDoc = new Document();
            custDoc["ProductID"] = product.ProductID;
            custDoc["Category"] = product.Category;
            custDoc["Description"] = product.Description;
            custDoc["ImageLink"] = product.ImageLink;
            custDoc["Name"] = product.Name;
            custDoc["Price"] = product.Price;
            return custDoc;
        }

        private Document unwrapOrder(Order order)
        {
            Document custDoc = new Document();
            custDoc["PK"] = order.PK;
            custDoc["SK"] = order.SK;
            custDoc["EntityType"] = order.EntityType;
            custDoc["CreatedDate"] = order.CreatedDate;
            custDoc["Status"] = order.Status;
            custDoc["CustomerID"] = order.CustomerID;
            custDoc["Address"] = order.Address;
            return custDoc;
        }

        private Document unwrapOrderCustomer(OrderCustomer orderCust)
        {
            Document custDoc = new Document();
            custDoc["PK"] = orderCust.PK;
            custDoc["SK"] = orderCust.SK;
            custDoc["EntityType"] = orderCust.EntityType;
            custDoc["Address"] = orderCust.Address;
            custDoc["Email"] = orderCust.Email;
            custDoc["FirstName"] = orderCust.FirstName;
            custDoc["LastName"] = orderCust.LastName;
            custDoc["Username"] = orderCust.Username;
            return custDoc;
        }

        private Document unwrapOrderProduct(OrderProduct orderProd)
        {
            Document custDoc = new Document();
            custDoc["PK"] = orderProd.PK;
            custDoc["SK"] = orderProd.SK;
            custDoc["EntityType"] = orderProd.EntityType;
            custDoc["ProductName"] = orderProd.ProductName;
            custDoc["ImageLink"] = orderProd.ImageLink;
            custDoc["Quantity"] = orderProd.Quantity;
            custDoc["Price"] = orderProd.Price;
            return custDoc;
        }

        #endregion

        [HttpPost("api/loader/[action]")]
        public async void loadCustomers([FromBody] Customer myCust)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table customers = Table.LoadTable(createContext(), "IanNathanCustomers");
                var batchWrite = customers.CreateBatchWrite();

                for (int i = 0; i < 100; i++)
                {
                    Customer tempCust = new Customer();
                    tempCust.Username = myCust.Username + i;
                    tempCust.CustomerID = myCust.CustomerID + i;
                    tempCust.Address = myCust.Address + i;
                    tempCust.Email = myCust.Email + i;
                    tempCust.FirstName = myCust.FirstName + i;
                    tempCust.LastName = myCust.LastName + i;
                    tempCust.PassHash = myCust.PassHash + i;
                    batchWrite.AddDocumentToPut(unwrapCustomer(tempCust));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void loadProducts([FromBody] Product myProd)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table products = Table.LoadTable(createContext(), "IanNathanProducts");
                var batchWrite = products.CreateBatchWrite();

                for (int i = 0; i < 100; i++)
                {
                    Product tempProd = new Product();
                    tempProd.ProductID = myProd.ProductID + i;
                    tempProd.Category = myProd.Category + i;
                    tempProd.Description = myProd.Description + i;
                    tempProd.ImageLink = myProd.ImageLink + i;
                    tempProd.Name = myProd.Name + i;
                    tempProd.Price = myProd.Price + i;
                    batchWrite.AddDocumentToPut(unwrapProduct(tempProd));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void loadOrderCustomer([FromBody] OrderCustomer myOrderCust)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table customers = Table.LoadTable(createContext(), "IanNathanOrders");
                await customers.PutItemAsync(unwrapOrderCustomer(myOrderCust));
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void loadOrders([FromBody] Order myOrder)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table products = Table.LoadTable(createContext(), "IanNathanOrders");
                var batchWrite = products.CreateBatchWrite();

                for (int i = 0; i < 100; i++)
                {
                    Order tempOrder = new Order();
                    tempOrder.PK = myOrder.PK + i;
                    tempOrder.SK = myOrder.SK + i;
                    tempOrder.EntityType = myOrder.EntityType;
                    tempOrder.CreatedDate = DateTime.Now.ToString();
                    tempOrder.Status = myOrder.Status;
                    tempOrder.CustomerID = myOrder.CustomerID + i;
                    tempOrder.Address = myOrder.Address + i;
                    batchWrite.AddDocumentToPut(unwrapOrder(tempOrder));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void loadOrderCustomers([FromBody] OrderCustomer myOrderCust)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table products = Table.LoadTable(createContext(), "IanNathanOrders");
                var batchWrite = products.CreateBatchWrite();

                for (int i = 0; i < 100; i++)
                {
                    OrderCustomer tempOrderCust = new OrderCustomer();
                    tempOrderCust.PK = myOrderCust.PK + i;
                    tempOrderCust.SK = myOrderCust.SK + i;
                    tempOrderCust.EntityType = myOrderCust.EntityType;
                    tempOrderCust.Address = myOrderCust.Address + i;
                    tempOrderCust.Email = myOrderCust.Email + i;
                    tempOrderCust.FirstName = myOrderCust.FirstName + i;
                    tempOrderCust.LastName = myOrderCust.LastName + i;
                    tempOrderCust.Username = myOrderCust.Username + i;
                    batchWrite.AddDocumentToPut(unwrapOrderCustomer(tempOrderCust));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void loadOrderProducts([FromBody] OrderProduct myOrderProd)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Random rand = new Random();
                Table products = Table.LoadTable(createContext(), "IanNathanOrders");
                var batchWrite = products.CreateBatchWrite();

                for (int i = 0; i < 200; i++)
                {
                    OrderProduct tempOrderProd = new OrderProduct();
                    tempOrderProd.PK = myOrderProd.PK + (i/2);
                    tempOrderProd.SK = myOrderProd.SK + i;
                    tempOrderProd.EntityType = myOrderProd.EntityType;
                    tempOrderProd.ProductName = myOrderProd.ProductName + i;
                    tempOrderProd.Quantity = rand.Next(1, 100).ToString();
                    tempOrderProd.Price = rand.Next(0, 99) + "." + rand.Next(0, 9) + rand.Next(0, 9);
                    tempOrderProd.ImageLink = myOrderProd.ImageLink + i;
                    batchWrite.AddDocumentToPut(unwrapOrderProduct(tempOrderProd));
                }

                await batchWrite.ExecuteAsync();
            }

        }

    }
}
