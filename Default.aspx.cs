using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;


namespace mikkyredis
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private static StringBuilder strResult = new StringBuilder();
        private static RedisConnection _redisConnection;

        protected async void Button1_Click(object sender, EventArgs e)
        {
            strResult.Append("Logy:Button clicked\n");

            _redisConnection = await RedisConnection.InitializeAsync("mikkyredis.redis.cache.windows.net:6380,password=,ssl=True,abortConnect=False");

            strResult.Append("Logy:*Connected to Redis client*********\n");

            try
            {
                Task thread1 = Task.Run(() => RunRedisCommandsAsync());
                Task.WaitAll(thread1);
            }
            catch (Exception ex)
            {
                strResult.Append("Logy:Exception" + ex.ToString());
            }
            finally
            {
                _redisConnection.Dispose();
                strResult.Append("Logy:----DONE---");
            }
        }

        private static async Task RunRedisCommandsAsync()
        {
            strResult.Append($"{Environment.NewLine}: Cache command: PING\n");

            RedisResult pingResult = await _redisConnection.BasicRetryAsync(async (db) => await db.ExecuteAsync("PING"));
            strResult.Append($"Cache response: {pingResult}");

            string key = "Stock1";
            string value = "MSFT";

            RedisValue getMessageResult = await _redisConnection.BasicRetryAsync(async (db) => await db.StringGetAsync(key));

            strResult.Append($" SET> {getMessageResult} \n");

            bool stringSetResult = await _redisConnection.BasicRetryAsync(async (db) => await db.StringSetAsync(key, value));
            strResult.Append($" Did you find it in Cache? {stringSetResult} \n");

            getMessageResult = await _redisConnection.BasicRetryAsync(async (db) => await db.StringGetAsync(key));
            strResult.Append($" GET> {getMessageResult}\n");

            strResult.Append("\n UTC > " + System.DateTime.UtcNow.ToString());

            strResult.Append("\n Local > " + System.DateTime.Now.ToString());

            var writeTask = WriteToBlobAsync(strResult.ToString());
            writeTask.Wait();
        }


        public static async Task WriteToBlobAsync(string text)
        {
            try
            {
                string connectionString = "DefaultEndpointsProtocol=https;AccountName=maheshfilesharing;AccountKey=";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = serviceClient.GetContainerReference("test");

                
                CloudBlockBlob blob = container.GetBlockBlobReference("container-logs.txt");
                

                string contents = blob.DownloadTextAsync().Result;
                Console.WriteLine(contents);
                await blob.UploadTextAsync(text);
            }
            catch (Exception ere)
            {
                Debug.WriteLine(ere.ToString());
            }

        }
    }
}