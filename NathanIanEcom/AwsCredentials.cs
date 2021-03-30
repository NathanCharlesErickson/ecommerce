using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
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
            return new ImmutableCredentials(Startup.DynamoDbApiKey, Startup.DynamoDbSecret, null);
        }
    }

}
