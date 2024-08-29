using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FluentTest.Scheduled.Utils
{
    internal static class ConstUtil
    {
        /// <summary>
        /// json类型声明
        /// </summary>
        public static readonly MediaTypeHeaderValue JsonMediaType = new MediaTypeHeaderValue("application/json");

        /// <summary>
        /// json序列化设置
        /// </summary>
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public const string MethodKey = "method";
        public const string UrlKey = "url";
        public const string MethodPostKey = "post";
        public const string MethodGetKey = "get";
    }
}
