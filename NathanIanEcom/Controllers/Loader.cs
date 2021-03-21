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

                for(int i = 0; i < 100; i++)
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

    }
}
