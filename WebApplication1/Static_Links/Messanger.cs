using System.Text;
using WebApplication1.Interfaces;
using WebApplication1.JsonResponce;

namespace WebApplication1.Static_Links
{
    public class Messanger : IMessanger
    {
        public async Task<jsonresponce> PostRequestAsync(string url, string json)
        {
            HttpClient client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage responce = await client.SendAsync(requestMessage);

            var OutPutPesponce = new jsonresponce {
                StatusCode=(int)responce.StatusCode,
                 Content=await responce.Content.ReadAsStringAsync(),
                 Headers=responce.Headers.ToDictionary(a=>a.Key, a => string.Join(";", a.Value))
            };

            return OutPutPesponce;
        }
    }
}
