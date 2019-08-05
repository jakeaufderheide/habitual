using System.Net.Http;

namespace Habitual.Storage.DB.Base
{
    public class DBClient
    {
        private static volatile HttpClient client;

        public static HttpClient GetInstance()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.MaxResponseContentBufferSize = 4096000;
            }

            return client;
        }
    }
}
