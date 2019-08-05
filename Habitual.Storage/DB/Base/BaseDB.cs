using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Habitual.Storage.DB.Base
{
    public class BaseDB
    {
        HttpClient client;
        private string mobileWebService = string.Empty;

        public BaseDB()
        {
            mobileWebService = "http://habitualwebapi.azurewebsites.net/";

            this.client = DBClient.GetInstance();
        }

        public async Task<string> GetDataAsync(string uriSuffix)
        {
            var uri = new Uri(string.Format("{0}/{1}", this.mobileWebService, uriSuffix));
            string content = string.Empty;
            try
            {
                var response = await client.GetAsync(uri);
                content = await HandleResponse(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return content;
        }

        public async Task<string> PostDataAsync(string uriSuffix, string jsonData)
        {
            var uri = new Uri(string.Format("{0}/{1}", this.mobileWebService, uriSuffix));
            string result = string.Empty;

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(uri, content);
                result = await HandleResponse(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<string> DeleteDataAsync(string uriSuffix)
        {
            var uri = new Uri(string.Format("{0}/{1}", this.mobileWebService, uriSuffix));
            string content = string.Empty;
            try
            {
                var response = await client.DeleteAsync(uri);
                content = await HandleResponse(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return content;
        }

        private async Task<string> HandleResponse(HttpResponseMessage response)
        {
            string content = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Unauthorized user");
            }
            else
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                throw new Exception(jsonError);
            }
            return content;
        }
    }
}
