using System.Collections;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DioSamExample
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(IDictionary input, ILambdaContext context)
        {
            var message = input["data"].ToString() ?? "Hello from Lambda!";
            string json = System.Text.Json.JsonSerializer.Serialize(message);
            context.Logger.LogLine($"Processed message: {json}");
            return message.ToUpper();
        }
    }
}
