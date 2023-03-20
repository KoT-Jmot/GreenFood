using WebApplication1.Interfaces;
using WebApplication1.Static_Links;

namespace WebApplication1.JsonResponce
{
    public class jsonresponce
    {
        public int StatusCode { get; set; }
        public Dictionary<string,string> Headers { get; set; }
        public string Content { get; set; }

    }
}
