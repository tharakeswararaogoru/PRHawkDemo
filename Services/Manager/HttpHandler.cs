using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.Manager
{
    public class HttpHandler : IHttpHandler
    {
        public HttpClient Client
        {
            get
            {
                HttpClient client = new HttpClient();
                string header = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes("snkirklandinterview:81b0aabc0012acc8be87056afc9b8eab4cd07e01"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", $"BASIC " + header);
                client.DefaultRequestHeaders.Add("User-Agent", "PRHawk Test App");
                client.DefaultRequestHeaders.Add("Accept", "*/*");

                return client;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(Uri fullUri)
        {
            return await Client.GetAsync(fullUri);
        }
    }
}
