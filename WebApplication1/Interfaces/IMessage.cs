using WebApplication1.Static_Links;

namespace WebApplication1.Interfaces
{
    public interface IMessage
    {
        Task<string> PostRequestAsync(string url, string json);
    }
}
