using WebApplication1.JsonResponce;


namespace WebApplication1.Interfaces
{
    public interface IMessanger
    {
        Task<jsonresponce> PostRequestAsync(string url, string json);
    }
}
