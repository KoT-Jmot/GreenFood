using System.Text;
using WebApplication1.Interfaces;

namespace WebApplication1.Static_Links
{
    public class Message : IMessage
    {
        public async Task<string> PostRequestAsync(string url, string json)
        {
            HttpClient client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage responce = await client.SendAsync(requestMessage);

            return await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
