using MediatR;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Loader;

namespace FluentTest.Scheduled.Utils
{
    internal static class TypeLoaderUtil
    {
        private static readonly IDictionary<string, Type> _handlerNameList = new ConcurrentDictionary<string, Type>();

        public static Type LoadRequestHandler(string name)
        {
            if (_handlerNameList.TryGetValue(name, out Type cacheType))
            {
                return cacheType;
            }
            foreach (Assembly assembly in AssemblyLoadContext.Default.Assemblies.Where(x => x.GetName().FullName.StartsWith("FluentTest")))
            {
                Type type = assembly.GetExportedTypes().Where(x => x.IsAssignableTo(typeof(IRequest))).FirstOrDefault(x => x.Name == name);
                if (type == null)
                {
                    continue;
                }
                _handlerNameList.Add(name, type);
                return type;
            }
            throw new Infrastructure.BusinessExpcetion("未找到执行器");
        }
    }
}
