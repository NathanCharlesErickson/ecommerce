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
    public class Loader : Helper
    {

  
        [HttpPost("api/loader/[action]")]
        public async void LoadCustomers([FromBody] Customer myCust)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table customers = Table.LoadTable(CreateContext(), "IanNathanCustomers");
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
                    batchWrite.AddDocumentToPut(UnwrapCustomer(tempCust));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void LoadProducts([FromBody] Product myProd)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table products = Table.LoadTable(CreateContext(), "IanNathanProducts");
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
                    batchWrite.AddDocumentToPut(UnwrapProduct(tempProd));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void LoadOrderCustomer([FromBody] OrderCustomer myOrderCust)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table customers = Table.LoadTable(CreateContext(), "IanNathanOrders");
                await customers.PutItemAsync(UnwrapOrderCustomer(myOrderCust));
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void LoadOrders([FromBody] Order myOrder)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table products = Table.LoadTable(CreateContext(), "IanNathanOrders");
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
                    batchWrite.AddDocumentToPut(UnwrapOrder(tempOrder));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void LoadOrderCustomers([FromBody] OrderCustomer myOrderCust)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Table products = Table.LoadTable(CreateContext(), "IanNathanOrders");
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
                    batchWrite.AddDocumentToPut(UnwrapOrderCustomer(tempOrderCust));
                }

                await batchWrite.ExecuteAsync();
            }

        }

        [HttpPost("api/loader/[action]")]
        public async void LoadOrderProducts([FromBody] OrderProduct myOrderProd)
        {
            using (var context = new DynamoDBContext(CreateContext()))
            {
                Random rand = new Random();
                Table products = Table.LoadTable(CreateContext(), "IanNathanOrders");
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
                    batchWrite.AddDocumentToPut(UnwrapOrderProduct(tempOrderProd));
                }

                await batchWrite.ExecuteAsync();
            }

        }

    }
}
