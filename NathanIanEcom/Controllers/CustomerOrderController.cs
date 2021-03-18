using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using NathanIanEcom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Controllers
{
    public class CustomerOrderController : ControllerBase
    {

        public AmazonDynamoDBClient createContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

        /*Gets a Product by it's Id*/

        [HttpGet("api/order/[action]")]
        public async Task<CustomerOrders> getOrderById([FromBody] PKSKModel myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<CustomerOrders>(myInput.PK, myInput.SK);
                return data;
            }
            
        }

        /*get products by Order ID*/
        [HttpGet("api/order/[action]")]
        public async Task<CustomerOrders> getProductByOrderId([FromBody] PKSKModel myInput)
        {
            using (var context = new DynamoDBContext(createContext()))
            {
                var data = await context.LoadAsync<CustomerOrders>(myInput.PK, myInput.SK);
                return data;
            }

        }

        /* gets all products by Order Id */
        [HttpGet("/api/order/[action]")]
        public void getAllProdByOrderId([FromBody] PKSKModel myInput)
        {

           
            AmazonDynamoDBClient client = createContext();
            string tableName = "IanNathanCustomersOrders";
            Table ThreadTable = Table.LoadTable(client, tableName);

            QueryFilter filter = new QueryFilter("PK", QueryOperator.Equal, myInput.PK);
            filter.AddCondition("SK", QueryOperator.BeginsWith, myInput.SK);

            Search mySearch = ThreadTable.Query(filter);
            String test = mySearch.Count.ToString();


        }

        /*get invoice by Order ID*/
        [HttpGet("api/order/[action]")]
        public void getInvoiceByOrder([FromBody] PKSKModel myInput)
        {
            AmazonDynamoDBClient client = createContext();
            string tableName = "IanNathanCustomersOrders";
            Table ThreadTable = Table.LoadTable(client, tableName);

            QueryFilter filter = new QueryFilter("PK", QueryOperator.Equal, myInput.PK);
            filter.AddCondition("SK", QueryOperator.BeginsWith, myInput.SK);

            Search mySearch = ThreadTable.Query(filter);
            String test = mySearch.Count.ToString();


        }

        /*get order by CUST ID GSI*/
        [HttpGet("api/order/[action]")]
        public void getOrderByCustId([FromBody] CustIdModel myInput)
        {
            AmazonDynamoDBClient client = createContext();
            string tableName = "IanNathanCustomerOrders";
            Table ThreadTable = Table.LoadTable(client, tableName);

            QueryFilter filter = new QueryFilter("CustomerID", QueryOperator.Equal, myInput.CustomerID);
            filter.AddCondition("SK", QueryOperator.Equal, myInput.SK); //not sure how to get latest SK
            
            Search mySearch = ThreadTable.Query(filter);
            String test = mySearch.Count.ToString();


        }

        /*get all orders by given customer id*/
        [HttpGet("api/order/[action]")]
        public void getAllOrder([FromBody] CustIdModel myInput)
        {
            AmazonDynamoDBClient client = createContext();
            string tableName = "IanNathanCustomersOrders";
            Table ThreadTable = Table.LoadTable(client, tableName);

            QueryFilter filter = new QueryFilter("CustomerID", QueryOperator.Equal, myInput.CustomerID);
            filter.AddCondition("SK", QueryOperator.BeginsWith, myInput.SK); 

            Search mySearch = ThreadTable.Query(filter);
            String test = mySearch.Count.ToString();


        }


    }

   





    }

   

