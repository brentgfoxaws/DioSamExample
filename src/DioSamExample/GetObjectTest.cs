using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;

namespace DioSamExample
{
    class GetObjectTest
    {
        private static IAmazonS3 client;

        private const string regionName = "us-west-2";
        private const string bucketName = "dio-sam-example";
        private const string keyName = "test.txt";

        public void RunTest()
        {
            client = new AmazonS3Client(RegionEndpoint.GetBySystemName(regionName));
            ReadObjectDataAsync().Wait();
        }

        static async Task ReadObjectDataAsync()
        {
            var request = new GetObjectRequest()
            {
                BucketName = bucketName,
                Key = keyName,
            };

            using (var response = await client.GetObjectAsync(request))
            {
                // Save the text to the /tmp folder of the container for processing.
                /*
                using (var bitmap = new Bitmap(response.ResponseStream))
                {
                    bitmap.Save(@"C:\Image.jpg");
                }
                */
            }
        }
    }
}
