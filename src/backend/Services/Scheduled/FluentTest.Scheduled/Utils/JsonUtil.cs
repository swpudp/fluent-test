using Quartz;
using System.Text.Json.Nodes;

namespace FluentTest.Scheduled.Utils
{
    internal static class JsonUtil
    {
        /// <summary>
        /// 创建json对象
        /// </summary>
        /// <param name="jobDataMap">job数据</param>
        /// <param name="ignoreKeys">忽略的key列表</param>
        /// <returns>json对象</returns>
        public static JsonObject BuildJsonObject(this JobDataMap jobDataMap, params string[] ignoreKeys)
        {
            JsonObject json = new JsonObject();
            foreach (string key in jobDataMap.Keys.Where(x => !ignoreKeys.Contains(x, StringComparer.OrdinalIgnoreCase)))
            {
                json.Add(key, jobDataMap.GetString(key));
            }
            return json;
        }
    }
}
