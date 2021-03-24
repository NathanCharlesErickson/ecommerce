using Amazon;
using Amazon.DynamoDBv2;
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
    public class Helper : ControllerBase
    {
        public AmazonDynamoDBClient CreateContext()
        {
            AmazonDynamoDBConfig myConfig = new AmazonDynamoDBConfig();
            myConfig.RegionEndpoint = RegionEndpoint.USWest2;

            var awsCred = new AwsCredentials();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCred, myConfig);
            return client;
        }

       protected Document UnwrapCustomer(Customer customer)
        {
            Document custDoc = new Document();
            custDoc["Username"] = customer.Username;
            custDoc["Address"] = customer.Address;
            custDoc["Email"] = customer.Email;
            custDoc["FirstName"] = customer.FirstName;
            custDoc["LastName"] = customer.LastName;
            custDoc["PassHash"] = customer.PassHash;
            return custDoc;
        }


        protected Document UnwrapProduct(Product product)
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

        protected Document UnwrapOrder(Order order)
        {
            Document custDoc = new Document();
            custDoc["PK"] = order.PK;
            custDoc["SK"] = order.SK;
            custDoc["EntityType"] = order.EntityType;
            custDoc["CreatedDate"] = order.CreatedDate;
            custDoc["Status"] = order.Status;
            custDoc["Username"] = order.Username;
            custDoc["Address"] = order.Address;
            return custDoc;
        }

        protected Document UnwrapOrderCustomer(OrderCustomer orderCust)
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

        protected Document UnwrapOrderProduct(OrderProduct orderProd)
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

     

        protected Dictionary<string, AttributeValue> OrderDictionary(Order myOrder)
        {
            Dictionary<string, AttributeValue> orderDic = new Dictionary<string, AttributeValue>();
            orderDic["PK"] = new AttributeValue { S = myOrder.PK };
            orderDic["SK"] = new AttributeValue { S = myOrder.SK };
            orderDic["EntityType"] = new AttributeValue { S = myOrder.EntityType };
            orderDic["CreatedDate"] = new AttributeValue { S = myOrder.CreatedDate };
            orderDic["Status"] = new AttributeValue { S = myOrder.Status };
            orderDic["Username"] = new AttributeValue { S = myOrder.Username };
            orderDic["Address"] = new AttributeValue { S = myOrder.Address };
            return orderDic;
        }

        
        protected Dictionary<string, AttributeValue> OrderCustomerDictionary(OrderCustomer myOrder)
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

       

        protected Dictionary<string, AttributeValue> OrderProductDictionary(OrderProduct myOrder)
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

        protected OrderProduct WrapOrderProduct(Document item)
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

       
        protected Dictionary<string, AttributeValue> ProductDictionary(Product product)
        {
            Dictionary<string, AttributeValue> prodDic = new Dictionary<string, AttributeValue>();
            prodDic["ProductID"] = new AttributeValue { S = product.ProductID };
            prodDic["Category"] = new AttributeValue { S = product.Category };
            prodDic["Description"] = new AttributeValue { S = product.Description };
            prodDic["ImageLink"] = new AttributeValue { S = product.ImageLink };
            prodDic["Name"] = new AttributeValue { S = product.Name };
            prodDic["Price"] = new AttributeValue { S = product.Price };
            return prodDic;
        }




    }
}
