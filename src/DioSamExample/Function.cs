using System;
using System.Collections;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.S3;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DioSamExample
{
    public class Function
    {

        IAmazonS3 S3Client { get; set; }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            S3Client = new AmazonS3Client();
        }

        /// <summary>
        /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
        /// </summary>
        /// <param name="s3Client"></param>
        public Function(IAmazonS3 s3Client)
        {
            this.S3Client = s3Client;
        }

        /// <summary>
        /// Simple example of S3 access using C#
        /// </summary>
        ///
        public async Task<string> FunctionHandler(IDictionary input, ILambdaContext context)
        {
            var bucketName = "dio-sam-example";
            var objectKey = "test.txt";
            try
            {
                var response = await this.S3Client.GetObjectMetadataAsync(bucketName, objectKey);
                var test = await this.S3Client.GetObjectAsync(bucketName, objectKey);
                context.Logger.LogLine($"Call returned {response.HttpStatusCode}, with a last modified of {response.LastModified} and a size of {test.ContentLength}");
                return response.Headers.ContentType;
            }
            catch(Exception e)
            {
                context.Logger.LogLine($"Error getting object {objectKey} from bucket {bucketName}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
                throw;
            }
        }
    }
}
