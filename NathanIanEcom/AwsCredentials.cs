using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom
{
    public class AwsCredentials : AWSCredentials
    {

        public AmazonDynamoDBClient context {get; set;}

        public override ImmutableCredentials GetCredentials()
        {
             AWSCred info = new AWSCred();

            JObject myObject = JObject.Parse("myCred.json");

            JToken myToken = myObject["AWSCred"];

            info.apikey = (String)myToken["apikey"];
            info.secret = (String)myToken["secret"]; 

            return new ImmutableCredentials(info.apikey, info.secret, null);


        }
    }

   
    public class AWSCred
    {
        public String apikey { get; set; }
        public String secret { get; set; }

    } 
}
