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
        public async void loadCustomer([FromBody] Customer myCust)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table customers = Table.LoadTable(createContext(), "IanNathanCustomers");
                await customers.PutItemAsync(unwrapCustomer(myCust));
            }

        }

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
        public async void loadProduct([FromBody] Product myProd)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table products = Table.LoadTable(createContext(), "IanNathanProducts");
                await products.PutItemAsync(unwrapProduct(myProd));
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
        public async void loadOrder([FromBody] Order myOrder)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table customers = Table.LoadTable(createContext(), "IanNathanOrders");
                await customers.PutItemAsync(unwrapOrder(myOrder));
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
        public async void loadOrderProduct([FromBody] OrderProduct myOrderProd)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                Table customers = Table.LoadTable(createContext(), "IanNathanOrders");
                await customers.PutItemAsync(unwrapOrderProduct(myOrderProd));
            }

        }

    }
}
