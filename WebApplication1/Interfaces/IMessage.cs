using WebApplication1.Static_Links;

namespace WebApplication1.Interfaces
{
    public interface IMessage
    {
        Message PostRequestAsync(string url, string json);
    }
}
